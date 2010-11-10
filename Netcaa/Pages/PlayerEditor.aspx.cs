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

namespace netcaa.Pages
{
	/// <summary>
	/// Summary description for PlayerEditor.
	/// </summary>
	public partial class PlayerEditor : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !DBUtilities.IsUserAdmin( Page.User.Identity.Name ) )
			{
				Response.Redirect( "/Static/notauthorized.html" );
			}
			
			if( !IsPostBack )
			{
				FillPlayerListBox( "%G%" );
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

		protected void ddlPositions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillPlayerListBox( ddlPositions.SelectedValue );
		}

		protected void ButtonEdit_Click(object sender, System.EventArgs e)
		{
			if( lbPlayers.SelectedItem != null )
			{
				EnablePlayerPanel( Convert.ToInt32( lbPlayers.SelectedValue ) );
			}
		}

		protected void ButtonSave_Click(object sender, System.EventArgs e)
		{
			// save off the data
			Object teamid = DBNull.Value;
			Object realteamid = DBNull.Value;
			if( ddlTeam.SelectedValue != "" )
			{
				teamid = Convert.ToInt32( ddlTeam.SelectedValue );
			}
			if( ddlRealTeam.SelectedValue != "" )
			{
				realteamid = Convert.ToInt32( ddlRealTeam.SelectedValue );
			}
			int isonir = 0;
			if( cbIR.Checked == true )
			{
				isonir = 1;
			}
            if (lbPlayers.SelectedValue != "" )
			{
                SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
					"spEditPlayer",
					Convert.ToInt32( lbPlayers.SelectedValue ),
					tbFirst.Text,
					tbLast.Text,
					tbAlias.Text,
					teamid,
					realteamid,
					Convert.ToInt32( ddlPosition.SelectedValue ),
					isonir );
			}
			else
			{
                SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
					"spAddPlayer",
					tbFirst.Text,
					tbLast.Text,
					tbAlias.Text,
					Convert.ToInt32( ddlPosition.SelectedValue ),
					teamid,
					realteamid,
					isonir );
			}

			SetPlayerEditState( false );
		}

		protected void ButtonCancel_Click(object sender, System.EventArgs e)
		{
			SetPlayerEditState( false );
		}

		protected void ButtonNew_Click(object sender, System.EventArgs e)
		{
            lbPlayers.ClearSelection();
			EnablePlayerPanel( -1 );
			tbFirst.Text = "";
			tbLast.Text = "";
			tbAlias.Text = "";
			ddlTeam.SelectedIndex = 0;
			ddlRealTeam.SelectedIndex = 0;
			ddlPosition.SelectedIndex = 0;
			cbIR.Checked = false;
		}

		private void EnablePlayerPanel( int PlayerId )
		{
			SetPlayerEditState( true );

            DataSet dsPositions = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetPositions" );
			ddlPosition.DataSource = dsPositions;
			ddlPosition.DataTextField = "PositionName";
			ddlPosition.DataValueField = "PositionId";
			ddlPosition.DataBind();

            DataSet dsTeams = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetAllTeams" );
			ddlTeam.DataSource = dsTeams;
			ddlTeam.DataTextField = "TeamAbbrev";
			ddlTeam.DataValueField = "TeamId";
			ddlTeam.DataBind();
			ListItem newitem = new ListItem( "", null );
			ddlTeam.Items.Insert( 0, newitem );

            DataSet dsRealTeams = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetRealTeams" );
			ddlRealTeam.DataSource = dsRealTeams;
			ddlRealTeam.DataTextField = "TeamAbbreviation";
			ddlRealTeam.DataValueField = "RealTeamId";
			ddlRealTeam.DataBind();
			newitem = new ListItem( "", null );
			ddlRealTeam.Items.Insert( 0, newitem );

			if( PlayerId != -1 )
			{
				// retrieve the info for this player
                DataSet dsPlayer = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
					"spGetPlayerDetail", PlayerId );

				// fill in the fields for this specific player
				tbFirst.Text = dsPlayer.Tables[0].Rows[0]["FirstName"].ToString();
				tbLast.Text = dsPlayer.Tables[0].Rows[0]["LastName"].ToString();
				tbAlias.Text = dsPlayer.Tables[0].Rows[0]["Alias"].ToString();
				ddlTeam.SelectedValue = dsPlayer.Tables[0].Rows[0]["TeamId"].ToString();
				ddlRealTeam.SelectedValue = dsPlayer.Tables[0].Rows[0]["RealTeamId"].ToString();
				ddlPosition.SelectedValue = dsPlayer.Tables[0].Rows[0]["PositionId"].ToString();
				if( Convert.ToInt32( dsPlayer.Tables[0].Rows[0]["IsOnInjuredReserve"] ) == 0 )
				{
					cbIR.Checked = false;
				}
				else
				{
					cbIR.Checked = true;
				}
			}
		}

		private void FillPlayerListBox( string Position )
		{
            DataSet dsPlayers = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetPlayersByPosition", Position );
			lbPlayers.DataSource = dsPlayers;
			lbPlayers.DataTextField = "player";
			lbPlayers.DataValueField = "PlayerId";
			lbPlayers.DataBind();		
		}

		private void SetPlayerEditState( bool Enabled )
		{
			ddlPositions.Enabled = !Enabled;
			lbPlayers.Enabled = !Enabled;
			ButtonNew.Enabled = !Enabled;
			ButtonEdit.Enabled = !Enabled;

			tbFirst.Enabled = Enabled;
			tbLast.Enabled = Enabled;
			tbAlias.Enabled = Enabled;
			ddlTeam.Enabled = Enabled;
			ddlRealTeam.Enabled = Enabled;
			ddlPosition.Enabled = Enabled;
			cbIR.Enabled = Enabled;
			ButtonCancel.Enabled = Enabled;
			ButtonSave.Enabled = Enabled;
		}
	}
}
