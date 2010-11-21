using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using Logger;

namespace netcaa.Pages
{
	/// <summary>
	/// Summary description for Lineups.
	/// </summary>
	public partial class Lineups : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
                DataSet teams = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
					"spGetAllTeams" );		
				lbTeams.DataSource = teams;
				lbTeams.DataTextField = "TeamAbbrev";
				lbTeams.DataValueField = "TeamId";
				lbTeams.DataBind();

                DataSet weeks = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
					"spGetAllWeeks" );		
				lbWeeks.DataSource = weeks;
				lbWeeks.DataTextField = "Week";
				lbWeeks.DataValueField = "WeekId";
				lbWeeks.DataBind();

				int currentweekid = DBUtilities.GetLineupWeekId();
                if (currentweekid == -1)
                {
                    Response.Redirect( "/Static/no_lineups.html" );
                }
				Session["LineupWeekId"] = currentweekid;

				int i = 0;
				while( i < lbWeeks.Items.Count )
				{
					if( Convert.ToInt32( lbWeeks.Items[i].Value ) == currentweekid )
					{
						break;
					}
					i++;
				}
				lbWeeks.SelectedIndex = i;

				int teamid = DBUtilities.GetUsersTeamId( Page.User.Identity.Name );
				Session["TeamId"] = teamid;
				i = 0;
				while( i < lbTeams.Items.Count )
				{
					if( Convert.ToInt32( lbTeams.Items[i].Value ) == teamid )
					{
						break;
					}
					i++;
				}
				lbTeams.SelectedIndex = i;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

        protected void ButtonSubmitTeamWeek_Click( object sender, System.EventArgs e )
        {
            PlayerSelection.Visible = true;
            TeamWeekSelection.Visible = false;

            int GameId = GameId = (int)SqlHelper.ExecuteScalar( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetGame", lbTeams.SelectedValue, lbWeeks.SelectedValue );
            DataSet GameInfo = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetGameDetails", GameId );
            DataRow r = GameInfo.Tables[0].Rows[0];

            lblGameHeader.Text = "";

            Session["TeamId"] = lbTeams.SelectedValue;

            lblGameHeader.Text = "Week " + r["Week"] + ": ";
            lblGameHeader.Text += r["visitor"].ToString() + " @ " + r["home"].ToString();
            lblGameHeader.Text += ", " + r["NumGames"] + " games";

            Session["LineupGameId"] = GameId;

            DataSet lineup = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTeamLineup", GameId, lbTeams.SelectedValue );
            lbRoster.DataSource = lineup;
            lbRoster.DataTextField = "player";
            lbRoster.DataValueField = "PlayerId";
            lbRoster.DataBind();

            // if there was an existing lineup, prepopulate!
            if( lineup.Tables[0].Rows[0]["LineupId"] == DBNull.Value )
            {
                litHiddenPlaceholder.Text = @"<input type=""hidden"" name=""hiddenSPG"" id=""hiddenSPG"" /> <input type=""hidden"" name=""hiddenSSG"" id=""hiddenSSG"" /><input type=""hidden"" name=""hiddenSSF"" id=""hiddenSSF"" /> <input type=""hidden"" name=""hiddenSPF"" id=""hiddenSPF"" /><input type=""hidden"" name=""hiddenSC"" id=""hiddenSC"" /> <input type=""hidden"" name=""hiddenBPG"" id=""hiddenBPG"" /><input type=""hidden"" name=""hiddenBSG"" id=""hiddenBSG"" /> <input type=""hidden"" name=""hiddenBSF"" id=""hiddenBSF"" /><input type=""hidden"" name=""hiddenBPF"" id=""hiddenBPF"" /> <input type=""hidden"" name=""hiddenBC"" id=""hiddenBC"" /><input type=""hidden"" name=""hiddenG1"" id=""hiddenG1"" /> <input type=""hidden"" name=""hiddenG2"" id=""hiddenG2"" />";
            }
            else
            {
                litHiddenPlaceholder.Text = @"<input type=""hidden"" name=""hiddenSPG"" value="""
                    + lineup.Tables[0].Rows[0]["PlayerId"].ToString()
                    + @""" /><input type=""hidden"" name=""hiddenSSG"" id=""hiddenSSG"" value="""
                    + lineup.Tables[0].Rows[1]["PlayerId"].ToString()
                    + @""" /><input type=""hidden"" name=""hiddenSSF"" id=""hiddenSSF"" value="""
                    + lineup.Tables[0].Rows[2]["PlayerId"].ToString()
                    + @""" /> <input type=""hidden"" name=""hiddenSPF"" id=""hiddenSPF"" value="""
                    + lineup.Tables[0].Rows[3]["PlayerId"].ToString()
                    + @""" /><input type=""hidden"" name=""hiddenSC"" id=""hiddenSC"" value="""
                    + lineup.Tables[0].Rows[4]["PlayerId"].ToString()
                    + @""" /> <input type=""hidden"" name=""hiddenBPG"" id=""hiddenBPG"" value="""
                    + lineup.Tables[0].Rows[5]["PlayerId"].ToString()
                    + @""" /><input type=""hidden"" name=""hiddenBSG"" id=""hiddenBSG"" value="""
                    + lineup.Tables[0].Rows[6]["PlayerId"].ToString()
                    + @""" /> <input type=""hidden"" name=""hiddenBSF"" id=""hiddenBSF"" value="""
                    + lineup.Tables[0].Rows[7]["PlayerId"].ToString()
                    + @""" /><input type=""hidden"" name=""hiddenBPF"" id=""hiddenBPF"" value="""
                    + lineup.Tables[0].Rows[8]["PlayerId"].ToString()
                    + @""" /> <input type=""hidden"" name=""hiddenBC"" id=""hiddenBC"" value="""
                    + lineup.Tables[0].Rows[9]["PlayerId"].ToString()
                    + @""" /><input type=""hidden"" name=""hiddenG1"" id=""hiddenG1"" value="""
                    + lineup.Tables[0].Rows[10]["PlayerId"].ToString()
                    + @""" /> <input type=""hidden"" name=""hiddenG2"" id=""hiddenG2"" value="""
                    + lineup.Tables[0].Rows[11]["PlayerId"].ToString()
                    + @""" />";

                lblPG.Text = lineup.Tables[0].Rows[0]["player"].ToString();
                lblSG.Text = lineup.Tables[0].Rows[1]["player"].ToString();
                lblSF.Text = lineup.Tables[0].Rows[2]["player"].ToString();
                lblPF.Text = lineup.Tables[0].Rows[3]["player"].ToString();
                lblC.Text = lineup.Tables[0].Rows[4]["player"].ToString();
                lblBackupPG.Text = lineup.Tables[0].Rows[5]["player"].ToString();
                lblBackupSG.Text = lineup.Tables[0].Rows[6]["player"].ToString();
                lblBackupSF.Text = lineup.Tables[0].Rows[7]["player"].ToString();
                lblBackupPF.Text = lineup.Tables[0].Rows[8]["player"].ToString();
                lblBackupC.Text = lineup.Tables[0].Rows[9]["player"].ToString();
                lblGarbage1.Text = lineup.Tables[0].Rows[10]["player"].ToString();
                lblGarbage2.Text = lineup.Tables[0].Rows[11]["player"].ToString();
            }
        }

        protected void ButtonSubmitLineup_Click( object sender, System.EventArgs e )
        {
            Log.AddLogEntry( LogEntryTypes.LineupSubmission,
                Page.User.Identity.Name,
                "Lineup submitted for team id " + Session["TeamId"] + ", gameid " + Session["LineupGameId"] );

            // delete the existing lineup records for this 
            try
            {
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spClearTeamLineup", Session["TeamId"], Session["LineupGameId"] );
            }
            catch( Exception ex )
            {
                Log.AddLogEntry( Logger.LogEntryTypes.SystemError,
                    Page.User.Identity.Name,
                    "Failed to clear existing lineup for teamid " + Session["TeamId"] + ", gameid " + Session["LineupGameId"] + " with exception: " + ex.Message );
                Response.Redirect( "/Static/default_error.html" );
                return;
            }

            // write each of the new lineup items
            try
            {
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenSPG"], "S", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenSSG"], "S", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenSSF"], "S", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenSPF"], "S", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenSC"], "S", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenBPG"], "B", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenBSG"], "B", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenBSF"], "B", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenBPF"], "B", Session["TeamId"], Session["LineupGameId"] );
                SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetPlayerLineupStatus", Request.Params["hiddenBC"], "B", Session["TeamId"], Session["LineupGameId"] );

                // it's possible someone could be short a garbage player
                if( Request.Params["hiddenG1"].Length != 0 )
                {
                    SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                        "spSetPlayerLineupStatus", Request.Params["hiddenG1"], "G", Session["TeamId"], Session["LineupGameId"] );
                }
                if( Request.Params["hiddenG2"].Length != 0 )
                {
                    SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                        "spSetPlayerLineupStatus", Request.Params["hiddenG2"], "G", Session["TeamId"], Session["LineupGameId"] );
                }

            }
            catch( Exception ex )
            {
                Log.AddLogEntry( Logger.LogEntryTypes.SystemError,
                    Page.User.Identity.Name,
                    "Failed to save new lineup records for teamid " + Session["TeamId"] + ", gameid " + Session["LineupGameId"] + " with exception: " + ex.Message );
                Response.Redirect( "/Static/default_error.html" );
                return;
            }

            SendLineupEmail( int.Parse( Session["TeamId"].ToString() ), int.Parse( Session["LineupGameId"].ToString() ) );

            Response.Redirect( "/Pages/home.aspx" );
        }

        protected void SendLineupEmail( int teamid, int gameid )
        {
            DataSet GameInfo = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetGameDetails", gameid );
            DataRow r = GameInfo.Tables[0].Rows[0];
            String MsgBody = "<h2><font face=arial>";
            String thisteam;
            if( r["hometeamid"].ToString() == teamid.ToString() )
            {
                MsgBody += r["home"].ToString() + " Lineup vs. " + r["visitor"].ToString() + "</font></h2>";
                thisteam = r["home"].ToString();
            }
            else
            {
                MsgBody += r["visitor"].ToString() + " Lineup @ " + r["home"].ToString() + "</font></h2>";
                thisteam = r["visitor"].ToString();
            }
            MsgBody += "<font face=arial><p><strong>Week " + r["Week"].ToString() + ": " + r["NumGames"].ToString() + " games</strong></p><br />";

            MsgBody += "<strong>Comment: </strong>" + tbComment.Text + "<br />";
            MsgBody += "<i>&nbsp;&nbsp;-- " + Page.User.Identity.Name.ToString() + "</i></font><br /><br />";

            DataSet lineup = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTeamLineupSimple", gameid, teamid );

            MsgBody += "<font face=arial><strong><u>Starters</u></strong><br />";
            MsgBody += lineup.Tables[0].Rows[0]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[1]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[2]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[3]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[4]["player"] + "<br />";
            MsgBody += "<br /><strong><u>Backups</u></strong><br />";
            MsgBody += lineup.Tables[0].Rows[5]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[6]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[7]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[8]["player"] + "<br />";
            MsgBody += lineup.Tables[0].Rows[9]["player"] + "<br />";
            MsgBody += "<br /><strong><u>Garbage</u></strong><br />";
            if( lineup.Tables[0].Rows.Count > 10 )
            {
                MsgBody += lineup.Tables[0].Rows[10]["player"] + "<br />";
            }
            if( lineup.Tables[0].Rows.Count > 11 )
            {
                MsgBody += lineup.Tables[0].Rows[11]["player"] + "<br />";
            }
            MsgBody += "</font>";

            String subject = thisteam + "Lineup for Week " + r["Week"].ToString();
            if( tbComment.Text.Length > 0 )
            {
                subject += " (C)";
            }

            mailer.sendSynchronousLeagueMail(
                subject,
                MsgBody,
                true,
                Page.User.Identity.Name );
        }
    }
}
