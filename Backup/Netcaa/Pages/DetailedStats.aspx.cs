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
using WebChart;

namespace netcaa.Pages
{
    public partial class DetailedStats : System.Web.UI.Page
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

                DataSet player = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetPlayer", Request.QueryString["PlayerId"]);
                lblPlayer.Text = player.Tables[0].Rows[0]["Player"].ToString();

                DataSet careerstats = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetPlayerCareerStats", Request.QueryString["PlayerId"]);
                dgCareerData.DataSource = careerstats;
                dgCareerData.DataBind();

                DataSet bestgames = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetPlayerBestGames", Request.QueryString["PlayerId"]);
                dgBestGames.DataSource = bestgames;
                dgBestGames.DataBind();
            }

            DataSet detailedstats = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetDetailedPlayerStats", Request.QueryString["PlayerId"], ddlSeasons.SelectedValue);
            dgSeasonData.DataSource = detailedstats;
            dgSeasonData.DataBind();

            DataSet movingaverage = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spFetchPlayerMovingAverage", Request.QueryString["PlayerId"], ddlSeasons.SelectedValue);
            ccMovingAverage.Charts[1].DataSource = movingaverage.Tables[0].DefaultView;
            ccMovingAverage.Charts[1].DataXValueField = "GameDate";
            ccMovingAverage.Charts[1].DataYValueField = "MovingPoints";
            ccMovingAverage.Charts[1].DataBind();
            ccMovingAverage.Charts[0].DataSource = movingaverage.Tables[0].DefaultView;
            ccMovingAverage.Charts[0].DataXValueField = "GameDate";
            ccMovingAverage.Charts[0].DataYValueField = "Points";
            ccMovingAverage.Charts[0].DataBind();
            ccMovingAverage.RedrawChart();
        }
    }
}