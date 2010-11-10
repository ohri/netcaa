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
	/// Summary description for SelectPlayers.
	/// </summary>
	public partial class SelectPlayers : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				if( (TransactionTypes)Session["Mode"] == TransactionTypes.SignPlayers )
				{
					fillPlayerBoxFA( "%G%" );
				}
				else if( (TransactionTypes)Session["Mode"] == TransactionTypes.CutPlayers || (TransactionTypes)Session["Mode"] == TransactionTypes.IRPlayers )
				{
					rblPositions.Visible = false;
					fillPlayerBoxRoster( (int)Session["TeamId"] );
				}
				else if( (TransactionTypes)Session["Mode"] == TransactionTypes.ActivatePlayers )
				{
					rblPositions.Visible = false;
					fillPlayerBoxRoster( (int)Session["TeamId"] );
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

		protected void rblPositions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillPlayerBoxFA( rblPositions.SelectedValue.ToString() );		
		}

		private void fillPlayerBoxFA( string pos )
		{
            DataSet dsPlayers = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetFreeAgentsByPosition", pos );		
			lbPlayers.DataSource = dsPlayers;
			lbPlayers.DataValueField = "PlayerId";
			lbPlayers.DataTextField = "Player";
			lbPlayers.DataBind();
		}	

		private void fillPlayerBoxRoster( int teamid )
		{
            DataSet dsPlayers = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetRoster", teamid );		
			lbPlayers.DataSource = dsPlayers;
			lbPlayers.DataValueField = "PlayerId";
			lbPlayers.DataTextField = "FullPlayer";
			lbPlayers.DataBind();
		}

		protected void btnSelect_Click(object sender, System.EventArgs e)
		{
			int count = 0;
			foreach( ListItem item in lbPlayers.Items )
			{
				if( item.Selected )
				{
					count++;
				}
			}
			if( count > 0 )
			{
				int []x = new int[count];
				count = 0;
				foreach( ListItem item in lbPlayers.Items )
				{
					if( item.Selected )
					{
						x[count++] = Convert.ToInt32( item.Value );
					}
				}

				Session["SelectedPlayers"] = x;
			}
            Response.Redirect("/Pages/TransactionEditor.aspx");
		}	
	}
}
