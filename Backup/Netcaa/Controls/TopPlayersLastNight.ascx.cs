using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace netcaa.Controls
{
    public partial class TopPlayersLastNight : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsTopPerformances = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    @"spGetBestRecentPerformances");
                dgTopPerformances.DataSource = dsTopPerformances;
                dgTopPerformances.DataBind();

                if (dsTopPerformances.Tables.Count > 0 && dsTopPerformances.Tables[0].Rows.Count > 0 )
                {
                    lblDate.Text = ((DateTime)dsTopPerformances.Tables[0].Rows[0]["GameDate"]).ToString("d");
                }
            }
        }
    }
}