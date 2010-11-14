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
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

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

				calStatDate.SelectedDate = DateTime.Today.AddDays( -1 );
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
            tbOutput.Text += "\r\n" + calStatDate.SelectedDate.ToString() + "\r\n";

            StatGrabber.StatGrabber sg = new StatGrabber.StatGrabber();

            ArrayList urls = sg.GetGames( this.calStatDate.SelectedDate );

            tbOutput.Text += "Ran GetGames, got back " + urls.Count + " games\r\n";

            SqlDatabase db = new SqlDatabase( @"data source=localhost\sqlexpress;initial catalog=netba;user id=netba_web;password=go_muddogs07!;Persist Security Info=true" );
            ArrayList problems = new ArrayList();
            foreach( string url in urls )
            {
                try
                {
                    ArrayList perfs = sg.GetGamePerformances( url );
                    tbOutput.Text += "Got " + perfs.Count + " perfs from " + url + "\r\n";
                    problems.AddRange( sg.SavePerformances( db, perfs, calStatDate.SelectedDate ) );
                }
                catch( StatGrabber.StatGrabberException ex )
                {
                    tbOutput.Text += ex.Message;
                }
            }

            if( problems.Count > 0 )
            {
                foreach( StatGrabber.PlayerPerformance p in problems )
                {
                    tbOutput.Text += p.FirstName + " " + p.LastName + " " + p.TeamName + "\r\n";
                }
            }
            else
            {
                tbOutput.Text += "No problems identifying players\r\n";
            }

            tbOutput.Text += sg.UpdateAveragesAndScores( db, calStatDate.SelectedDate );

            Log.AddLogEntry(
                LogEntryTypes.StatsProcessed,
                Page.User.Identity.Name,
                tbOutput.Text );
        }
        protected void btnAutosub_Click( object sender, EventArgs e )
        {
            string result = AutoSub.ProcessAutosubs( ddlWeeks.SelectedValue );
            tbOutput.Text += result;
        }
    }
}
