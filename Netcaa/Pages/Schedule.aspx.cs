using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;

namespace netcaa.Pages
{
    public partial class Schedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet seasons = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
            "spFetchSeasons");
                ddlSeasons.DataSource = seasons;
                ddlSeasons.DataTextField = "Season";
                ddlSeasons.DataValueField = "SeasonId";
                ddlSeasons.DataBind();
                ddlSeasons.SelectedValue = DBUtilities.GetCurrentSeasonId().ToString();

                DataSet schedule = schedule = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetFullSchedule");

                dgSchedules.DataSource = schedule;
                dgSchedules.DataBind();
            }
        }

        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            DataSet schedule = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetFullSchedule", ddlSeasons.SelectedItem.Value);
            dgSchedules.DataSource = schedule;
            dgSchedules.DataBind();
        }
    }
}