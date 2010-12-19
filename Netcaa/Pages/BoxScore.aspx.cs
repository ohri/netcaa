using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace netcaa.Pages
{
    public partial class BoxScore : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            SqlDatabase db = new SqlDatabase( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"] );

            // get game information
            int gameid = Convert.ToInt32( Request["GameId"] );
//            DataSet dsGameInfo = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
//                "spGetGameDetails", gameid );
            DataSet dsGameInfo = db.ExecuteDataSet( "spGetGameDetails", gameid );
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
            //DataSet dsHome = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
            //    "spGetTeamContributions", gameid, hometeamid );
            DataSet dsHome = db.ExecuteDataSet( "spGetTeamContributions", gameid, hometeamid );
            AddSummaryData( dsHome );
            dgHome.DataSource = dsHome;
            dgHome.DataBind();

            //DataSet dsAway = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
            //    "spGetTeamContributions", gameid, awayteamid );
            DataSet dsAway = db.ExecuteDataSet( "spGetTeamContributions", gameid, awayteamid );
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

        protected void dg_ItemDataBound( object sender, DataGridItemEventArgs e )
        {
            if( e.Item.Cells[0].Text == "ST" )
            {
                // this is the sub total row
                e.Item.Cells[0].Text = "Sub-Total";
                e.Item.Cells[0].ColumnSpan = 2;
                e.Item.Cells.RemoveAt( 1 );
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