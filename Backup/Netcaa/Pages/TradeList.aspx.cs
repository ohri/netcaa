using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;

namespace netcaa.Pages
{
    public partial class TradeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsActiveTrades = SqlHelper.ExecuteDataset(DBUtilities.Connection, "spFetchActiveTrades", Page.User.Identity.Name);
                FormatData(dsActiveTrades.Tables[0]);
                dgActiveTrades.DataSource = dsActiveTrades;
                dgActiveTrades.DataBind();

                DataSet dsCompletedTrades = SqlHelper.ExecuteDataset(DBUtilities.Connection, "spFetchCompletedTrades", Page.User.Identity.Name);
                FormatData(dsCompletedTrades.Tables[0]);
                dgCompletedTrades.DataSource = dsCompletedTrades;
                dgCompletedTrades.DataBind();

                DataSet dsTeams = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetAllTeams");
                ddlTeams.DataSource = dsTeams;
                ddlTeams.DataTextField = "TeamAbbrev";
                ddlTeams.DataValueField = "TeamId";
                ddlTeams.DataBind();
                ListItem newitem = new ListItem("", null);
                ddlTeams.Items.Insert(0, newitem);
            }
        }
        protected void btnInitiateTrade_Click(object sender, EventArgs e)
        {
            // create new trade in db, returning id
            int tradeid = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spAddTrade",
                DBUtilities.GetUsersTeamId(Page.User.Identity.Name),
                ddlTeams.SelectedValue);

            // call trade edit page with that id
            Response.Redirect(String.Concat("/Pages/TradePropose.aspx?TradeId=", tradeid.ToString()));
        }

        protected void FormatData(DataTable t)
        {
            t.Columns.Add("TradeContents", typeof(string));

            foreach (DataRow r in t.Rows)
            {
                int TradeId = 0;
                int.TryParse(r["TradeId"].ToString(), out TradeId);
                DataSet dc = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTradeInfo",
                    TradeId);

                r["TradeContents"] = SupportFunctions.CreateTradeContentHtml(
                    r["TeamAAssetts"].ToString(),
                    r["TeamBAssetts"].ToString(),
                    dc.Tables[0].Rows[0]["TeamAName"].ToString(),
                    dc.Tables[0].Rows[0]["TeamBName"].ToString());
            }
        }
    }
}