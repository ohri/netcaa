using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace StatGrabber
{
    public class StatGrabber
    {
        public StatGrabber()
        {
        }

        public ArrayList GetGames( DateTime DateToGet )
        {
            // temporary limiting to confid 7 (big ten)
            // this will need to loop over the appropriate conferences
//            string page = WebPageToString( "http://scores.espn.go.com/ncb/scoreboard?confId=7&date=" + DateToString( DateToGet ) );
            string page = WebPageToString( "http://scores.espn.go.com/ncb/scoreboard?date=" + DateToString( DateToGet ) );

            ArrayList retval = new ArrayList();

            Regex boxstart = new Regex( @"/ncb/boxscore\?gameId=" );
            //Extract the address
            Match m = boxstart.Match( page );
            while( m.Success )
            {
                int sPos = m.Index;
                int ePos = 0;
                if( sPos > 0 )
                {
                    Regex end = new Regex( "\"" );
                    Match me = end.Match( page, sPos );
                    ePos = me.Index;
                    if( ePos > -1 )
                    {
                        string url = "http://espn.go.com" + page.Substring( sPos, ePos - sPos );
                        if( !retval.Contains( url ) )
                        {
                            retval.Add( url );
                        }
                    }
                }
                m = m.NextMatch();
            }

            // remove the last three ... they are 'leader' boxes, not this conference
            retval.RemoveRange( retval.Count - 3, 3 );

            return retval;
        }

        public ArrayList GetGamePerformances( string url )
        {
            Regex GetTeams = new Regex( @"</div>(.*)</th></tr><tr align=.right. class=.colhead.>" );
            Regex GetPlayerStatRows = new Regex( @"<td style=.text-align:left;. nowrap>(.*)?</td></tr>" );
            Regex SplitStatRows = new Regex( @"</td><td.*?>" );
            Regex ExtractPlayerName = new Regex( @"^(?:.+?>)?([\w\.\'-]+)\s+?([\w\.\'-]+(?:\s[\w.]+)?)(?:.*?)?$" );
            Regex ExtractShots = new Regex( @"([0-9]*)\-([0-9]*)" );
            string Page = WebPageToString( url );

            MatchCollection TeamMatches = GetTeams.Matches( Page );
            if( TeamMatches.Count != 2 )
            {
                StatGrabberException ex = new StatGrabberException(
                    "Couldn't find the teams in game "
                    + url
                    + " ... maybe ESPN hasn't finalized them?" );
                throw ex;
            }

            int HomeAfterThis = TeamMatches[1].Groups[1].Index;

            ArrayList perfs = new ArrayList();
            MatchCollection StatLines = GetPlayerStatRows.Matches( Page );
            foreach( Match i in StatLines )
            {
                if( i.Groups.Count < 2 )
                {
                    StatGrabberException ex = new StatGrabberException(
                        "Seems to be a problem in the statline structure in game "
                        + url
                        + " ... maybe ESPN has changed format?" );
                    throw ex;
                }

                string[] Cells = SplitStatRows.Split( i.Groups[1].Value );

                PlayerPerformance p = new PlayerPerformance();
                MatchCollection PlayerName = ExtractPlayerName.Matches( Cells[0] );
                if( PlayerName.Count < 1 )
                {
                    StatGrabberException ex = new StatGrabberException(
                        "Player's name doesn't match the pattern: " + Cells[0] );
                    throw ex;
                }
                else
                {
                    p.FirstName = PlayerName[0].Groups[1].Value;
                    p.LastName = PlayerName[0].Groups[2].Value;
                }

                if( i.Index >= HomeAfterThis )
                {
                    p.TeamName = TeamMatches[1].Groups[1].Value;
                }
                else
                {
                    p.TeamName = TeamMatches[0].Groups[1].Value;
                }
                try
                {
                    // cell 1 has minutes
                    p.Minutes = Convert.ToInt32( Cells[1] );

                    // cell 2 has made-attempted FG
                    Match fgs = ExtractShots.Match( Cells[2] );
                    p.ShotsMade = Convert.ToInt32( fgs.Groups[1].Value );
                    p.ShotAttempts = Convert.ToInt32( fgs.Groups[2].Value );

                    // cell 3 has made-attempted on 3 pointers
                    Match threes = ExtractShots.Match( Cells[3] );
                    p.ThreesMade = Convert.ToInt32( threes.Groups[1].Value );
                    p.ThreeAttempts = Convert.ToInt32( threes.Groups[2].Value );

                    // cell 4 has made-attempted FT
                    Match fts = ExtractShots.Match( Cells[4] );
                    p.FTsMade = Convert.ToInt32( fts.Groups[1].Value );
                    p.FTAttempts = Convert.ToInt32( fts.Groups[2].Value );

                    // cell 5 has off rebs
                    p.OffensiveRebounds = Convert.ToInt32( Cells[5] );

                    // cell 6 has rebs
                    p.DefensiveRebounds = Convert.ToInt32( Cells[6] ) - p.OffensiveRebounds;

                    // cell 7 has ast
                    p.Assists = Convert.ToInt32( Cells[7] );

                    // cell 8 has stl
                    p.Steals = Convert.ToInt32( Cells[8] );

                    // cell 9 has blocks
                    p.Blocks += Convert.ToInt32( Cells[9] );

                    // cell 10 has to
                    p.Turnovers = Convert.ToInt32( Cells[10] );

                    // cell 11 has pf
                    p.Fouls = Convert.ToInt32( Cells[11] );

                    perfs.Add( p );
                }
                catch( System.FormatException e )
                {
                    // yes, this is a horrible thing to do, but it works and i'm 
                    // too lazy to switch it
                    // this exception happens when the player gets a DNP of some
                    // sort
                }
            }
            return perfs;
        }

        public ArrayList SavePerformances( SqlDatabase db, ArrayList perfs, DateTime when )
        {
            ArrayList problems = new ArrayList();
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                foreach( PlayerPerformance p in perfs )
                {
                    DbCommand cmd = db.GetStoredProcCommand( "spAddPlayerPerformance",
                        when, p.FirstName, p.LastName, p.TeamName, p.Minutes, p.Assists, p.Blocks,
                        p.DefensiveRebounds, p.Fouls, p.FTAttempts, p.FTsMade, p.OffensiveRebounds,
                        p.ShotAttempts, p.ShotsMade, p.Steals, p.ThreeAttempts,
                        p.ThreesMade, p.Turnovers );
                    int x = (int)db.ExecuteScalar( cmd, trans );
                    if( x != 0 )
                    {
                        problems.Add( p );
                    }
                }
                trans.Commit();
            }
            catch( Exception e )
            {
                trans.Rollback();
                PlayerPerformance p = new PlayerPerformance();
                p.FirstName = "Exception thrown while saving results to db: " + e.Message;
                problems.Add( p );
            }
            conn.Close();
            return problems;
        }

        public string UpdateAveragesAndScores( SqlDatabase db, DateTime when )
        {
            string result = "Successly updated scores and averages";
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                db.ExecuteNonQuery( "spSetCurrentScore", when );
                db.ExecuteNonQuery( "spRecalcPlayerAverages" );
                trans.Commit();
            }
            catch( Exception e )
            {
                result = "Failed to update scores and averages: " + e.Message;
                trans.Rollback();
            }
            conn.Close();
            return result;
        }

        public string DateToString( DateTime x )
        {
            return x.Year.ToString() + x.Month.ToString( "0#" ) + x.Day.ToString( "0#" );
        }

        public string WebPageToString( string url )
        {
            WebRequest wreq = HttpWebRequest.Create( url );
            WebResponse wres = wreq.GetResponse();
            StreamReader sr = new StreamReader( wres.GetResponseStream() );
            string page = sr.ReadToEnd();
            sr.Close();
            return page;
        }
    }
}
