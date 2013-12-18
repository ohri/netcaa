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
    public partial class TeamDraft : System.Web.UI.Page
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

                DataSet teams = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetAllTeams");
                ddlTeams.DataSource = teams;
                ddlTeams.DataTextField = "TeamAbbrev";
                ddlTeams.DataValueField = "TeamId";
                ddlTeams.DataBind();
                if (Request.QueryString["TeamId"] != null)
                {
                    ddlTeams.SelectedValue = Request["TeamId"];
                }

                DataSet draft = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTeamDraft", ddlTeams.SelectedValue, DBUtilities.GetCurrentSeasonId());
                gvDraft.DataSource = draft;
                gvDraft.DataBind();
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            DataSet draft = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTeamDraft", ddlTeams.SelectedValue, ddlSeasons.SelectedValue);
            gvDraft.DataSource = draft;
            gvDraft.DataBind();
        }
    }
}