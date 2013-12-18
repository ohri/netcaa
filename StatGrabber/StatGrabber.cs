using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace StatGrabber
{
    public class StatGrabber
    {
        public StatGrabber()
        {
        }

        public ArrayList GetGames( DateTime DateToGet, SqlDatabase db )
        {
            Regex boxstart = new Regex( @"/ncb/boxscore\?gameId=" );
            Regex topPerformers = new Regex( @"<h4>TOP PERFORMERS<span>" );
            ArrayList retval = new ArrayList();
            DataSet conferences = db.ExecuteDataSet( "spFetchRealConferences" );
            foreach( DataRow r in conferences.Tables[0].Rows )
            {
                if( Boolean.Parse( r["IsAvailableForUse"].ToString() ) )
                {
                    string page = WebPageToString( "http://scores.espn.go.com/ncb/scoreboard?confId=" + r["ESPNId"] + "&date=" + DateToString( DateToGet ) );

                    //Extract the address
                    Match m = boxstart.Match( page );
                    Match stopHere = topPerformers.Match( page );
                    while( m.Success )
                    {
                        int sPos = m.Index;
                        int ePos = 0;
                        if( sPos > 0 && sPos < stopHere.Index )
                        {
                            Regex end = new Regex( "\"" );
                            Match me = end.Match( page, sPos );
                            ePos = me.Index;
                            if( ePos > -1 )
                            {
                                string url = "http://scores.espn.go.com" + page.Substring( sPos, ePos - sPos );
                                if( !retval.Contains( url ) )
                                {
                                    retval.Add( url );
                                }
                            }
                        }
                        m = m.NextMatch();
                    }
                }
            }
            return retval;
        }

        public ArrayList GetGamePerformances( string url, ArrayList problems )
        {
            Regex GetTeams = new Regex( @"6px;""></div>(.*)</th></tr><tr" );
            Regex GetPlayerStatRows = new Regex( @"<td style=.text-align:left. nowrap>(.*)?</td></tr>" );
            Regex SplitStatRows = new Regex( @"</td><td.*?>" );
//            Regex ExtractPlayerName = new Regex( @"^(?:.+?>)?([\w\.\'-]+)\s+?([\w\.\'-]+(?:\s[\w.]+)?)(?:.*?)?$" );
//Regex ExtractPlayerName = new Regex( @"^(?:.+?>)?([\w\.\'-\(\)]+)\s+?([\w\.\'-\(\)]+(?:\s[\w.\(\)]+)?)(?:.*?)?$" );
            //                                       -atag-   --first name--      ---last name-------------------
            Regex ExtractPlayerName = new Regex( @"^(?:.+?>)?(.+?)(\s)(.+?)<.*?$" );
            // julie put in this simplified version 11/26/11; seems to work fine, not sure
            // why we had a more complex version before. definitely wasnt working for players
            // with a dash in first name

            // <td style="text-align:left;" nowrap>A Thomas II</td>
            // <td style="text-align:left;" nowrap><a href="http://espn.go.com/mens-college-basketball/player/_/id/51354/okaro-white">Okaro White</a>, F</td>

            // <td style="text-align:left" nowrap><a href="http://espn.go.com/mens-college-basketball/player/_/id/58169/javonte-green">Javonte Green</a>, F</td><td>19</td><td>6-11</td><td>0-2</td><td>3-3</td><td align="right">2</td><td align="right">4</td><td>6</td><td>0</td><td>2</td><td>0</td><td>1</td><td>3</td><td>15</td></tr><tr align="right" class=odd>

            Regex ExtractPlayerNameNoLink = new Regex( @"^([A-Z])\s(.+)" );

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

                bool happy = true;
                if( PlayerName.Count >= 1 )
                {
                    p.FirstName = PlayerName[0].Groups[1].Value;
                    p.LastName = PlayerName[0].Groups[3].Value;
                }
                else
                {
                    // try the no-link version
                    PlayerName = ExtractPlayerNameNoLink.Matches( Cells[0] );
                    if( PlayerName.Count >= 1 )
                    {
                        p.FirstName = PlayerName[0].Groups[1].Value;
                        p.LastName = PlayerName[0].Groups[2].Value;
                    }
                    else
                    {
                        p.FirstName = "Player's name doesn't match the pattern: " + Cells[0];
                        p.LastName = " in " + Cells[0];
                        problems.Add( p );
                        happy = false;
                    }
                }
                
                if( happy )
                {
                    if( i.Index >= HomeAfterThis )
                    {
                        p.TeamName = TeamMatches[1].Groups[1].Value;
                        p.Opponent = TeamMatches[0].Groups[1].Value;
                    }
                    else
                    {
                        p.TeamName = TeamMatches[0].Groups[1].Value;
                        p.Opponent = TeamMatches[1].Groups[1].Value;
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
            }
            return perfs;
        }

        public ArrayList SavePerformances( SqlDatabase db, ArrayList perfs, DateTime when )
        {
            ArrayList problems = new ArrayList();
            try
            {
                foreach( PlayerPerformance p in perfs )
                {
                    DbCommand cmd = db.GetStoredProcCommand( "spAddPlayerPerformance",
                        when, p.FirstName, p.LastName, p.TeamName, p.Minutes, p.Assists, p.Blocks,
                        p.DefensiveRebounds, p.Fouls, p.FTAttempts, p.FTsMade, p.OffensiveRebounds,
                        p.ShotAttempts, p.ShotsMade, p.Steals, p.ThreeAttempts,
                        p.ThreesMade, p.Turnovers, p.Opponent );
                    int x = (int)db.ExecuteScalar( cmd );
                    if( x != 0 )
                    {
                        problems.Add( p );
                    }
                }

                string[] q = ( ( from PlayerPerformance perf in perfs
                        select perf.TeamName ).Distinct() ).ToArray();

                db.ExecuteNonQuery( "spAddDefensiveBonuses", q[0], q[1], when );
                db.ExecuteNonQuery( "spAddDefensiveBonuses", q[1], q[0], when );
            }
            catch( Exception e )
            {
                PlayerPerformance p = new PlayerPerformance();
                p.FirstName = "Exception thrown while saving results to db: " + e.Message;
                problems.Add( p );
            }
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
