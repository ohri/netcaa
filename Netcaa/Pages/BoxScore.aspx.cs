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
	/// Summary description for BoxScore.
	/// </summary>
	public partial class BoxScore : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// get game information
			int gameid = Convert.ToInt32( Request["GameId"] );
            DataSet dsGameInfo = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetGameDetails", gameid );		
			int hometeamid = (int)dsGameInfo.Tables[0].Rows[0]["hometeamid"];
			int awayteamid = (int)dsGameInfo.Tables[0].Rows[0]["visitorteamid"];

			lblPageTitle.Text = "Week " + dsGameInfo.Tables[0].Rows[0]["Week"].ToString() 
				+ ": " + dsGameInfo.Tables[0].Rows[0]["visitor"].ToString() 
				+ " " + dsGameInfo.Tables[0].Rows[0]["visitorscore"].ToString()
				+ " @ " + dsGameInfo.Tables[0].Rows[0]["home"].ToString()
				+ " " + dsGameInfo.Tables[0].Rows[0]["homescore"].ToString();
			lblHome.Text = dsGameInfo.Tables[0].Rows[0]["home"].ToString();
			lblAway.Text = dsGameInfo.Tables[0].Rows[0]["visitor"].ToString();

			if( dsGameInfo.Tables[0].Rows[0]["HomeWins"] != DBNull.Value )
			{
				lblGameScore.Text = dsGameInfo.Tables[0].Rows[0]["visitor"].ToString() 
				+ " wins " + dsGameInfo.Tables[0].Rows[0]["visitorwins"].ToString()
				+ " games, " + dsGameInfo.Tables[0].Rows[0]["home"].ToString()
				+ " wins " + dsGameInfo.Tables[0].Rows[0]["homewins"].ToString()
				+ " games";
			}

			// get the team boxes
            DataSet dsHome = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetTeamContributions", gameid, hometeamid );	
			AddSummaryData( dsHome );
			dgHome.DataSource = dsHome;
			dgHome.DataBind();

            DataSet dsAway = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetTeamContributions", gameid, awayteamid );
			AddSummaryData( dsAway );
			dgAway.DataSource = dsAway;
			dgAway.DataBind();
		}

		private void AddSummaryData( DataSet ds )
		{
			// loop through the contributions summing offense, defense
			int TotalOffense = 0;
			int TotalDefense = 0;
			foreach( DataRow row in ds.Tables[0].Rows )
			{
				if( row["Offense"] != DBNull.Value )
				{
					TotalOffense += Convert.ToInt32( row["Offense"] );
				}
				if( row["Defense"] != DBNull.Value )
				{
					TotalDefense += Convert.ToInt32( row["Defense"] );
				}
			}

			// add the summary rows
			DataRow newrow = ds.Tables[0].NewRow();
			newrow["Offense"] = TotalOffense;
			newrow["Defense"] = TotalDefense;
			newrow["Status"] = "ST";
			newrow["Player"] = "&nbsp;";
			ds.Tables[0].Rows.Add( newrow );
			
			newrow = ds.Tables[0].NewRow();
			newrow["Offense"] = TotalOffense + TotalDefense;
			newrow["Defense"] = TotalOffense + TotalDefense;
			newrow["Status"] = "T";
			newrow["Player"] = "&nbsp;";
			ds.Tables[0].Rows.Add( newrow );
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
			this.dgAway.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dg_ItemDataBound);
			this.dgHome.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dg_ItemDataBound);

		}
		#endregion

		private void dg_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if( e.Item.Cells[0].Text == "ST" )
			{
				// this is the sub total row
				e.Item.Cells[0].Text = "Sub-Total";
				e.Item.Cells[0].ColumnSpan = 2;
				e.Item.Cells.RemoveAt( 1 );
//				e.Item.Cells[0].CssClass = "bolditem";
//				e.Item.Cells[1].CssClass = "bolditem";
//				e.Item.Cells[2].CssClass = "bolditem";
			}
			else if( e.Item.Cells[0].Text == "T" )
			{
				// this is the total row
				e.Item.Cells[0].Text = "Total";
				e.Item.Cells[0].ColumnSpan = 2;
				e.Item.Cells.RemoveAt( 1 );
				e.Item.Cells[1].ColumnSpan = 2;
				e.Item.Cells.RemoveAt( 2 );
				e.Item.Cells[1].HorizontalAlign = HorizontalAlign.Center;
				e.Item.Cells[0].CssClass = "bolditem";
				e.Item.Cells[1].CssClass = "bolditem";
			}
		}
	}
}
