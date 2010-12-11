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
    public partial class TeamPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.dgRoster.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRoster_ItemDataBound);

            DataSet roster = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetRoster", Request.QueryString["TeamId"]);
            dgRoster.DataSource = roster;
            dgRoster.DataBind();

            DataSet owners = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTeamOwners", Request.QueryString["TeamId"]);

            litEmailAddress.Text = "<a href=\"mailto:" + owners.Tables[0].Rows[0]["EmailAddress"] + "\">"
                + owners.Tables[0].Rows[0]["FirstName"] + " "
                + owners.Tables[0].Rows[0]["LastName"] + "</a>";
            string OwnerInfo = "";
            if (owners.Tables[0].Rows[0]["City"].ToString().Length > 0)
            {
                OwnerInfo += "<br />" + owners.Tables[0].Rows[0]["City"]
                    + ", " + owners.Tables[0].Rows[0]["State"];
            }
            if (owners.Tables[0].Rows[0]["IMInfo"].ToString().Length > 0)
            {
                OwnerInfo += "<br />" + owners.Tables[0].Rows[0]["IMInfo"];
            }
            if (OwnerInfo.Length > 0)
            {
                litOwnerInfo.Text = OwnerInfo;
            }

            if (Page.User.Identity.Name == owners.Tables[0].Rows[0]["username"].ToString())
            {
                litEditLink.Text = "<br /><a  style=\"font-size:x-small;\" href=\"EditInfo.aspx\">Edit Info</a>";
            }

            lblPageTitle.Text = owners.Tables[0].Rows[0]["TeamName"].ToString();

            DataSet record = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTeamStatus", Request.QueryString["TeamId"]);
            if (record.Tables[0].Rows.Count == 1)
            {
                lblRecord.Text = record.Tables[0].Rows[0]["Wins"].ToString() + '-' + record.Tables[0].Rows[0]["Losses"].ToString();
            }

            hlTeamHistory.NavigateUrl += Request.QueryString["TeamId"];

            DateTime LastLogin = (DateTime)SqlHelper.ExecuteScalar(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
        "spGetLastLogin", owners.Tables[0].Rows[0]["OwnerId"]);
            lblLastLogin.Text = LastLogin.ToString();
        }

        private void dgRoster_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            Literal lit = (Literal)e.Item.FindControl("litIR");
            if (lit == null) return;
            DataRowView rowview = (DataRowView)e.Item.DataItem;
            if ((bool)rowview.Row["IsOnInjuredReserve"] == true)
            {
                lit.Text = @"<img src=/Images/ir.gif alt='IR' \>";
            }
        }
    }
}