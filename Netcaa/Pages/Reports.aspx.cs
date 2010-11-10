using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace netcaa.Pages
{
    public partial class Reports : System.Web.UI.Page
    {
        private string connection = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            this.dgReportOutput.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgReportOutput_PageIndexChanged);
            this.dgReportOutput.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgReportOutput_ItemDataBound);

            if (!IsPostBack)
            {
                // i hate how i did this...some day i'll put these into the db or
                // at least a list structure up top so its data driven

                DataTable reports = new DataTable();
                reports.Columns.Add("ReportName");
                reports.Columns.Add("ReportFunction");

                DataRow row = reports.NewRow();
                reports.Rows.Add(row);

                row = reports.NewRow();
                row["ReportName"] = "Top Players";
                row["ReportFunction"] = "spGetBestPlayers";
                reports.Rows.Add(row);

                row = reports.NewRow();
                row["ReportName"] = "Top Free Agents";
                row["ReportFunction"] = "spGetBestFreeAgents";
                reports.Rows.Add(row);

                row = reports.NewRow();
                row["ReportName"] = "Top Free Agents - Lately";
                row["ReportFunction"] = "spGetBestFreeAgentsLately";
                reports.Rows.Add(row);

                row = reports.NewRow();
                row["ReportName"] = "Top Rookies";
                row["ReportFunction"] = "spGetBestRookies";
                reports.Rows.Add(row);

                ddlReports.DataSource = reports;
                ddlReports.DataTextField = "ReportName";
                ddlReports.DataValueField = "ReportFunction";
                ddlReports.DataBind();

                DataSet seasons = SqlHelper.ExecuteDataset(connection, "spFetchSeasons");
                ddlSeasons.DataSource = seasons;
                ddlSeasons.DataTextField = "Season";
                ddlSeasons.DataValueField = "SeasonId";
                ddlSeasons.DataBind();
                ddlSeasons.SelectedValue = DBUtilities.GetCurrentSeasonId().ToString();
            }
        }
        protected void ButtonShowReport_Click(object sender, System.EventArgs e)
        {
            if (ddlReports.SelectedItem.Text != "")
            {
                dgReportOutput.CurrentPageIndex = 0;
                BindGrid();
            }
        }

        private void dgReportOutput_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgReportOutput.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void dgReportOutput_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("lblRowCount");
            if (lbl == null) return;
            int index = e.Item.ItemIndex + 1 + dgReportOutput.PageSize * dgReportOutput.CurrentPageIndex;
            lbl.Text = Convert.ToString(index);
        }

        private void BindGrid()
        {
            DataSet players = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                ddlReports.SelectedValue, ddlSeasons.SelectedValue);
            dgReportOutput.DataSource = players.Tables[0];
            if (ddlReports.SelectedItem.Text == "Top Free Agents" || ddlReports.SelectedItem.Text == "Top Free Agents - Lately")
            {
                dgReportOutput.Columns[4].Visible = false;
            }
            else
            {
                dgReportOutput.Columns[4].Visible = true;
            }
            dgReportOutput.DataBind();
        }
    }
}