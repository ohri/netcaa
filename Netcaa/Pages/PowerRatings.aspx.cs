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
    public partial class PowerRatings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.dgPowerRatings.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgPowerRatings_SortCommand);

            if (!IsPostBack)
            {
                DataSet seasons = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spFetchSeasons");
                ddlSeasons.DataSource = seasons;
                ddlSeasons.DataTextField = "Season";
                ddlSeasons.DataValueField = "SeasonId";
                ddlSeasons.DataBind();
                ddlSeasons.SelectedValue = DBUtilities.GetCurrentSeasonId().ToString();

                DataSet dsPowerRatings = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    @"spGetPowerRatings");
                dgPowerRatings.DataSource = dsPowerRatings;
                dgPowerRatings.DataBind();
            }
        }
        private void dgPowerRatings_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            DataSet dsPowerRatings = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                @"spGetPowerRatings");
            DataView dv = new DataView(dsPowerRatings.Tables[0]);

            if ((string)Session["SortColumn"] == e.SortExpression)
            {
                if ((bool)Session["SortAscending"])
                {
                    dv.Sort = e.SortExpression + " desc";
                    Session["SortAscending"] = false;
                }
                else
                {
                    dv.Sort = e.SortExpression;
                    Session["SortAscending"] = true;
                }
            }
            else
            {
                dv.Sort = e.SortExpression;
                Session["SortAscending"] = true;
            }
            Session["SortColumn"] = e.SortExpression;
            dgPowerRatings.DataSource = dv;
            dgPowerRatings.DataBind();
        }

        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            DataSet dsPowerRatings = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                @"spGetPowerRatings", ddlSeasons.SelectedItem.Value);
            dgPowerRatings.DataSource = dsPowerRatings;
            dgPowerRatings.DataBind();
        }
    }
}