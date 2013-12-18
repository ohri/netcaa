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
    public partial class Standings : System.Web.UI.Page
    {
        private string CurDivision;
        private string CurConference;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.dgStandings.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgStandings_ItemDataBound);

            if (!IsPostBack)
            {
                DataSet seasons = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spFetchSeasons");
                ddlSeasons.DataSource = seasons;
                ddlSeasons.DataTextField = "Season";
                ddlSeasons.DataValueField = "SeasonId";
                ddlSeasons.DataBind();
                ddlSeasons.SelectedValue = DBUtilities.GetCurrentSeasonId().ToString();

                DataSet dsStandings = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    @"spGetStandings");
                AddExtraRows(dsStandings);
                dgStandings.DataSource = dsStandings;
                dgStandings.DataBind();
            }
        }

        private void dgStandings_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.Cells[2].Text == "-1")
            {
                // this is the conference heading
                e.Item.Cells[0].ColumnSpan = 3;
                e.Item.Cells[0].CssClass = "bolditem";
                e.Item.Cells.RemoveAt(2);
                e.Item.Cells.RemoveAt(1);
            }
            else if (e.Item.Cells[1].Text == "-1")
            {
                // this is a division heading
                e.Item.Cells[0].ColumnSpan = 3;
                e.Item.Cells[0].CssClass = "italicitem";
                e.Item.Cells.RemoveAt(2);
                e.Item.Cells.RemoveAt(1);
            }
        }

        private void AddExtraRows(DataSet dsStandings)
        {
            int i = 0;
            while (i < dsStandings.Tables[0].Rows.Count)
            {
                if (CurConference != dsStandings.Tables[0].Rows[i]["Conference"].ToString())
                {
                    DataRow x = dsStandings.Tables[0].NewRow();
                    x["Team"] = dsStandings.Tables[0].Rows[i]["Conference"];
                    x["Wins"] = -1;
                    x["Losses"] = -1;
                    CurConference = dsStandings.Tables[0].Rows[i]["Conference"].ToString();
                    dsStandings.Tables[0].Rows.InsertAt(x, i);

                    i++;

                    x = dsStandings.Tables[0].NewRow();
                    x["Team"] = dsStandings.Tables[0].Rows[i]["Division"] + " Division";
                    x["Wins"] = -1;
                    CurDivision = dsStandings.Tables[0].Rows[i]["Division"].ToString();
                    dsStandings.Tables[0].Rows.InsertAt(x, i);
                }
                else if (CurDivision != dsStandings.Tables[0].Rows[i]["Division"].ToString())
                {
                    DataRow x = dsStandings.Tables[0].NewRow();
                    x["Team"] = dsStandings.Tables[0].Rows[i]["Division"] + " Division";
                    x["Wins"] = -1;
                    CurDivision = dsStandings.Tables[0].Rows[i]["Division"].ToString();
                    dsStandings.Tables[0].Rows.InsertAt(x, i);
                }

                i++;
            }
        }

        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            DataSet dsStandings = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                @"spGetStandings", ddlSeasons.SelectedItem.Value);
            AddExtraRows(dsStandings);
            dgStandings.DataSource = dsStandings;
            dgStandings.DataBind();
        }

    }
}