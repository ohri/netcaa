using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using Logger;

namespace netcaa.Pages
{
	/// <summary>
	/// Summary description for SelectPlayer.
	/// </summary>
	public partial class SelectPlayer : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				fillPlayerBox( "%G%" );

				if( Request.QueryString["PickId"] != null )
				{
					ViewState["PickId"] = Convert.ToInt32(Request.QueryString["PickId"].ToString());
				}
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

		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			pnlSelection.Visible = false;
			pnlConfirmation.Visible = true;

			lblPickInfo.Text = 
				"<i>" 
				+ getPickInfo( (int)ViewState["PickId"] ) 
				+ " - <b>" 
				+ getPlayerInfo( Convert.ToInt32( lbPlayers.SelectedValue ) ) 
				+ "</b></i>";
		}

		protected void btnOk_Click(object sender, System.EventArgs e)
		{
			makePick( (int)ViewState["PickId"], Convert.ToInt32( lbPlayers.SelectedValue ) );
			sendDraftEmail( (int)ViewState["PickId"], Convert.ToInt32( lbPlayers.SelectedValue ) );
            Response.Redirect("/Pages/Draft.aspx");
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
            Response.Redirect("/Pages/Draft.aspx");
		}

		private void fillPlayerBox( string pos )
		{
            DataSet dsPlayers = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetFreeAgentsByPosition", pos );		
			lbPlayers.DataSource = dsPlayers;
			lbPlayers.DataValueField = "PlayerId";
			lbPlayers.DataTextField = "Player";
			lbPlayers.DataBind();
		}

		protected void rblPositions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillPlayerBox( rblPositions.SelectedValue.ToString() );
		}

		private void makePick( int PickId, int PlayerId )
		{
			// make the pick
			try
			{
                SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
					"spExecuteDraftPick", PickId, PlayerId );
				Log.AddLogEntry( Logger.LogEntryTypes.PickSubmitted, 
					Page.User.Identity.Name, 
					"Executed draft pick " + PickId );
			}
			catch( Exception ex )
			{
				Log.AddLogEntry( Logger.LogEntryTypes.SystemError, 
					Page.User.Identity.Name, 
					"Failed to execute draft pick with pickid " + PickId + " with exception: " + ex.Message );
			}		
		}

        private string getNextPickInfo( int PickId )
        {
            DataSet draftPickInfo = null;

            try
            {
                draftPickInfo = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetNextDraftPickDetail", PickId );
            }
            catch( Exception ex )
            {
                return "";
            }
            return
                "R"
                + draftPickInfo.Tables[ 0 ].Rows[ 0 ][ "Round" ].ToString()
                + "P"
                + draftPickInfo.Tables[ 0 ].Rows[ 0 ][ "RoundPosition" ].ToString()
                + " "
                + draftPickInfo.Tables[ 0 ].Rows[ 0 ][ "OwningTeamAbbrev" ].ToString();
        }

		private string getPickInfo( int PickId )
		{
			DataSet draftPickInfo = null;

			try
			{
                draftPickInfo = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"], 
					"spGetDraftPickDetail", PickId );
			}
			catch( Exception ex )
			{
				return "";
			}
			return
				"R"
				+ draftPickInfo.Tables[0].Rows[0]["Round"].ToString()
				+ "P"
				+ draftPickInfo.Tables[0].Rows[0]["RoundPosition"].ToString()
				+ " "
				+ draftPickInfo.Tables[0].Rows[0]["OwningTeamAbbrev"].ToString();
		}

		private string getPlayerInfo( int PlayerId )
		{
            DataSet playerInfo = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"], 
				"spGetPlayerDetail", PlayerId );
			return
				playerInfo.Tables[0].Rows[0]["FirstName"].ToString()
				+ " "
				+ playerInfo.Tables[0].Rows[0]["LastName"].ToString()
				+ " "
				+ playerInfo.Tables[0].Rows[0]["PositionName"].ToString()
				+ " " 
				+ playerInfo.Tables[0].Rows[0]["TeamAbbreviation"].ToString();
		}

        private void sendDraftEmail( int PickId, int PlayerId )
        {
            string pickInfo = getPickInfo( PickId );
            string nextPickInfo = getNextPickInfo( PickId );
            string msgBody =
                "Pick "
                + pickInfo
                + " is done: <br /><br /><b>"
                + getPlayerInfo( PlayerId )
                + "</b><br /><br />Selection made by "
                + Page.User.Identity.Name.ToString()
                + "<br /><br />Comments:<br /><i>"
                + tbComments.Text
                + "</i><br /><br />"
                + nextPickInfo
                + " is up!";

            string subject = "Draft Update: " + pickInfo + " done -- " + nextPickInfo + " is up";
            if( tbComments.Text.Length > 0 )
            {
                subject += " (C)";
            }

            mailer.sendSynchronousLeagueMail( subject, msgBody, true, Page.User.Identity.Name );
        }
	}
}
