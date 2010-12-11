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
    public partial class TeamHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet record = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetFranchiseRecords", Request.QueryString["TeamId"]);
            dgFranchiseRecord.DataSource = record;
            dgFranchiseRecord.DataBind();

            if( record.Tables.Count > 0 && record.Tables[0].Rows.Count > 0 )
            {
                lblPageTitle.Text = record.Tables[0].Rows[0]["Team"] + " Team History";
            }
        }
    }
}