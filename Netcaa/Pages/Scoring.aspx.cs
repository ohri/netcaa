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
using System.Data.SqlClient;
using Logger;
using StatGrabber;

namespace netcaa.Pages
{
	/// <summary>
	/// Summary description for Scoring.
	/// </summary>
	public partial class Scoring : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !DBUtilities.IsUserAdmin( Page.User.Identity.Name ) )
			{
				Response.Redirect( "/Static/notauthorized.html" );
			}		

			// Put user code to initialize the page here
			if( !IsPostBack )
			{
                DataSet ds = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetAllWeeks" );
				ddlWeeks.DataSource = ds;
				ddlWeeks.DataTextField = "Week";
				ddlWeeks.DataValueField = "WeekId";
				ddlWeeks.DataBind();

				calStatDate.SelectedDate = DateTime.Now.AddDays( -1 );
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

		protected void ButtonFinalize_Click(object sender, System.EventArgs e)
		{
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spFinalizeGames", Convert.ToInt32( ddlWeeks.SelectedValue ) );
			Log.AddLogEntry( LogEntryTypes.WeekFinalized, Page.User.Identity.Name, "Finalized stats for weekid " + ddlWeeks.SelectedValue + ", week " + ddlWeeks.SelectedItem.Text );
		}

		protected void ButtonProcessDaily_Click(object sender, System.EventArgs e)
		{
			tbScrapeResults.Text += "\r\n" + calStatDate.SelectedDate.ToString() + "\r\n";

            StatGrabber.StatGrabber sg = new StatGrabber.StatGrabber();
			ArrayList urls = sg.GetGames( calStatDate.SelectedDate );

			tbScrapeResults.Text += "Ran GetGames, got back " + urls.Count + " games\r\n";

            ArrayList problems = null;
            ArrayList performances = null;

            if( urls.Count > 0 )
            {
                try
                {
                    performances = sg.GetPerformances( urls );
                    tbScrapeResults.Text += "Ran GetPerformances, got back " + performances.Count + " perfs\r\n";

                    if( performances.Count > 0 )
                    {
                        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
                        problems = sg.SavePerformances( con, performances, calStatDate.SelectedDate );

                        tbScrapeResults.Text += "Ran SavePerformances, got back " + problems.Count + " problems\r\n";
                        foreach( PlayerPerformance p in problems )
                        {
                            tbScrapeResults.Text += p.FirstName + " " + p.LastName + " " + p.TeamName + "\r\n";
                        }
                    }
                }
                catch( StatGrabberException ex )
                {
                    tbScrapeResults.Text += "StatGrabber threw: "
                        + ex.Message;
                }
            }

			Log.AddLogEntry( 
				LogEntryTypes.StatsProcessed, 
				Page.User.Identity.Name, 
				"Processed stats for " 
					+ calStatDate.SelectedDate.ToString() 
					+ ", found "  
					+ urls.Count
					+ " games, "
					+ ( performances == null ? -1 : performances.Count )
					+ " perfs, "
					+ ( problems == null ? -1 : problems.Count )
					+ " problems" );
		}
        protected void btnAutosub_Click( object sender, EventArgs e )
        {
            string result = AutoSub.ProcessAutosubs( ddlWeeks.SelectedValue );
            tbScrapeResults.Text += result;
        }
    }
}
