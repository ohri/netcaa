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
    public partial class Alerts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet alerts = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spFetchAlerts", DBUtilities.GetUsersTeamId(Page.User.Identity.Name));

            if (alerts.Tables[0].Rows.Count == 0)
            {
                DataRow noAlerts = alerts.Tables[0].NewRow();
                noAlerts["message"] = "No alerts at this time.";
                alerts.Tables[0].Rows.Add(noAlerts);

                dlAlerts.ItemStyle.ForeColor = System.Drawing.Color.Black;
            }

            dlAlerts.DataSource = alerts;
            dlAlerts.DataBind();
        }
    }
}