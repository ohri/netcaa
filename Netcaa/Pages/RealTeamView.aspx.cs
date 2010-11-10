using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace netcaa.Pages
{
    public partial class RealTeamView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet roster = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetNBARoster", Request.QueryString["RealTeamId"]);
            dgRoster.DataSource = roster;
            dgRoster.DataBind();

            lblPageTitle.Text = roster.Tables[0].Rows[0]["TeamName"].ToString();
        }
    }
}