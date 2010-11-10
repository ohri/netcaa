using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace netcaa.Controls
{
    public partial class NavBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsteams = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    @"spGetAllTeams");

                ddlTeams.DataSource = dsteams;
                ddlTeams.DataTextField = "TeamAbbrev";
                ddlTeams.DataValueField = "TeamId";
                ddlTeams.DataBind();
                ddlTeams.Items.Insert(0, "Team Pages");

                int TradeCount = (int)SqlHelper.ExecuteScalar(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    @"spGetTradeCount",
                    Page.User.Identity.Name);

                if (TradeCount > 0)
                {
                    lblTrades.Text = "Trades (" + TradeCount.ToString() + ")";
                }
            }
        }

        protected void ddlTeams_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlTeams.SelectedIndex != 0)
            {
                Response.Redirect("/Pages/TeamPage.aspx?TeamId=" + ddlTeams.SelectedItem.Value.ToString());
            }
        }

        protected void ButtonSearch_Click(object sender, System.EventArgs e)
        {
            Session["SearchString"] = txtPlayerSearch.Text;
            Response.Redirect("/Pages/PlayerSearch.aspx");
        }

    }
}