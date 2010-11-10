using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace StatGrabber
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class PlayerPerformance
	{
		public PlayerPerformance()
		{
			Offense = 0;
			Defense = 0;
		}

		public string FirstName;
		public string LastName;
		public int Offense;
		public int Defense;
		public int Minutes;
		public string TeamName;
	};
	
	public class StatGrabber
	{
		public StatGrabber()
		{
		}

		public ArrayList GetGames( DateTime DateToGet )
		{
			string page = WebPageToString(  "http://espn.go.com/nba/scoreboard?date=" + DateToString( DateToGet ) );

			ArrayList retval = new ArrayList();

			Regex boxstart = new Regex( @"/nba/boxscore\?gameId=" );
			//Extract the address
			Match m = boxstart.Match( page );
			while( m.Success )
			{
				int sPos = m.Index;
				int ePos = 0;
				if (sPos > 0)
				{
					Regex end = new Regex( "\"" );
					Match me = end.Match( page, sPos );
					ePos = me.Index;
					if( ePos > -1 )
					{
                        string url = "http://espn.go.com" + page.Substring(sPos, ePos - sPos);
                        if ( retval.BinarySearch( url ) < 0 )
                        {
                            retval.Add( url );
                        }
					}
				}
				m = m.NextMatch();
			}

			return retval;
		}

		public ArrayList GetPerformances( ArrayList GameURLs )
		{
			ArrayList perfs = new ArrayList();

			//Regex GetTeams = new Regex( @"<td.*?COLSPAN=14\sclass=head\salign=center><strong>(.*)?</td>" );
			//Regex GetTeams = new Regex( @"<td.*?>(.*?)</td>" );
            //Regex GetTeams = new Regex( @"<td colspan=.14. style=.text-align: center; background: #[ABCDEFabcdef0-9]{6};.>(.*)?</td>" );
            //Regex GetTeams = new Regex(@"<td colspan=.15. style=.text-align: center; background: #[ABCDEFabcdef0-9]{6};.>(.*)?</td>");
            //Regex GetTeams = new Regex(@"<td colspan=.15. style=.text-align: center; background: #[ABCDEFabcdef0-9]{6} !important;.>(.*)?</td>");
            Regex GetTeams = new Regex(@"</a>(.*)</th></tr><tr align=.right.>");
            //Regex GetPlayerStatRows = new Regex( @"<tr\s+align=right\s+valign=middle\s+bgcolor=.+?>\n<td\s+align=left\s+nowrap>(.*?)</td></tr>" );
			//Regex GetPlayerStatRows = new Regex( @"<a href=./nba/players/profile.statsId=\d{2,}.>(.*)?</td></tr>" );
			Regex GetPlayerStatRows = new Regex( @"<td style=.text-align:left;. nowrap>(.*)?</td></tr>" );

			Regex SplitStatRows = new Regex( @"</td><td.*?>" );
			//Regex ExtractPlayerName = new Regex( @"<.*>(\S*)\s(.*)<.*" );
			//Regex ExtractPlayerName = new Regex( @"^(?:.+?>)?(.+?)\s+?(.+?)(?:<.+?)?$" );
			Regex ExtractPlayerName = new Regex( @"^(?:.+?>)?([\w\.\'-]+)\s+?([\w\.\'-]+(?:\s[\w.]+)?)(?:.*?)?$" );
			Regex ExtractThrees = new Regex( @"([0-9])+\-[0-9]+" );
			Regex NeneException = new Regex( @".*Nene.*", RegexOptions.IgnoreCase );

			foreach( string GameURL in GameURLs )
			{
				string Page = WebPageToString( GameURL );

				MatchCollection TeamMatches = GetTeams.Matches( Page );
                if (TeamMatches.Count != 2)
                {
                    StatGrabberException ex = new StatGrabberException(
                        "Couldn't find the teams in game "
                        + GameURL
                        + " ... maybe ESPN hasn't finalized them?");
                    throw ex;
                }

				int HomeAfterThis = TeamMatches[1].Groups[1].Index;

				MatchCollection StatLines = GetPlayerStatRows.Matches( Page );
				foreach( Match i in StatLines )
				{
                    if( i.Groups.Count < 2 )
                    {
                        StatGrabberException ex = new StatGrabberException(
                            "Seems to be a problem in the statline structure in game "
                            + GameURL
                            + " ... maybe ESPN has changed format?" );
                        throw ex;
                    }

					string[] Cells = SplitStatRows.Split( i.Groups[1].Value );

					PlayerPerformance p = new PlayerPerformance();
					MatchCollection PlayerName = ExtractPlayerName.Matches( Cells[0] );
					if( PlayerName.Count < 1 )
					{
						// the nene exception
						MatchCollection Nene = NeneException.Matches( Cells[0] );
						if( Nene.Count > 0 )
						{
							// yup, it's him
							p.FirstName = "Nene";
							p.LastName = "Hilario";
						}
						else
						{
                            StatGrabberException ex = new StatGrabberException(
                                "Player's name doesn't match the pattern: " + Cells[0] );
                            throw ex; 
						}
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

						// cell 3 has made-missed on 3 pointers
						Match threes = ExtractThrees.Match( Cells[3] );
						p.Offense += Convert.ToInt32( threes.Groups[1].Value );

						// cell 5 has off rebs
						p.Offense += 2 * Convert.ToInt32( Cells[5] );

						// cell 6 has def rebs
						p.Defense += Convert.ToInt32( Cells[6] );

						// cell 8 has ast
						p.Offense += Convert.ToInt32( Cells[8] );

						// cell 9 has stl
						p.Defense += 2 * Convert.ToInt32( Cells[9] );

						// cell 10 has blocks
						p.Defense += 2 * Convert.ToInt32( Cells[10] );

						// cell 11 has to
						p.Defense -= Convert.ToInt32( Cells[11] );

						// cell 12 has pf
						p.Offense -= Convert.ToInt32( Cells[12] );

                        // cell 13 has +/-

                        // cell 14 has points
                        p.Offense += Convert.ToInt32( Cells[ 14 ] );

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

		public ArrayList SavePerformances( SqlConnection con, ArrayList performances, DateTime when )
		{

			ArrayList problems = new ArrayList();

			foreach( PlayerPerformance p in performances )
			{
				int x = (int)SqlHelper.ExecuteScalar( con, "spAddPlayerPerformance",
						when, p.FirstName, p.LastName, p.TeamName, p.Minutes, p.Offense, p.Defense );
				if( x != 0 )
				{
					problems.Add( p );
				}
			}

			SqlHelper.ExecuteNonQuery( con, "spSetCurrentScore", when );
			SqlHelper.ExecuteNonQuery( con, "spRecalcPlayerAverages" );

			return problems;
		}

        //public void ReportMissingPlayers( ArrayList missing, string SmtpServer, string recipient, DateTime gamedate )
        //{
        //    SmtpMail.SmtpServer = SmtpServer;
        //    MailMessage m = new MailMessage();
        //    m.To = recipient;
        //    m.From = "ohri@sep.com";
        //    m.Subject = "Missing player report for game date " + gamedate.ToString();
        //    foreach( PlayerPerformance p in missing )
        //    {
        //        m.Body += p.FirstName + " " + p.LastName + " " + p.TeamName + "\n";
        //    }
        //    SmtpMail.Send( m );
        //}

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
