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
    public partial class draft : System.Web.UI.Page
    {
        private int nextPickId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

            this.dgDraft.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDraft_ItemDataBound);

            if (!IsPostBack)
            {
                DataSet seasons = SqlHelper.ExecuteDataset(DBUtilities.Connection, "spFetchSeasons");
                ddlSeasons.DataSource = seasons;
                ddlSeasons.DataTextField = "Season";
                ddlSeasons.DataValueField = "SeasonId";
                ddlSeasons.DataBind();
                ddlSeasons.SelectedValue = DBUtilities.GetCurrentSeasonId().ToString();
                //int t = 11;
                //ddlSeasons.SelectedValue = t.ToString();

                // draft list from SQL Server
                DataSet draftList = SqlHelper.ExecuteDataset(DBUtilities.Connection, "spGetDraftOrder", ddlSeasons.SelectedValue);
                AddExtraRows(draftList);
                dgDraft.DataSource = draftList;
                dgDraft.DataBind();
            }

            DataSet nextPick = SqlHelper.ExecuteDataset(DBUtilities.Connection, "spGetNextDraftPick");
            try
            {
                nextPickId = Convert.ToInt32(nextPick.Tables[0].Rows[0]["NextPickId"]);
            }
            catch
            {
                nextPickId = -1;
            }

            lblCurrentTime.Text = DateTime.Now.ToShortTimeString();
        }

        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            DataSet draftList = SqlHelper.ExecuteDataset(DBUtilities.Connection, "spGetDraftOrder", ddlSeasons.SelectedValue);
            dgDraft.DataSource = draftList;
            AddExtraRows(draftList);
            dgDraft.DataBind();
        }

        private void dgDraft_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.Cells[4].Text == "-1")
            {
                e.Item.Cells[0].Text = "Round " + e.Item.Cells[0].Text;
                e.Item.Cells[0].CssClass = "bolditem";
                e.Item.Cells[1].ColumnSpan = 4;
                e.Item.Cells[1].CssClass = "bolditem";
                e.Item.Cells.RemoveAt(2);
                e.Item.Cells.RemoveAt(2);
                e.Item.Cells.RemoveAt(2);
                e.Item.Cells.RemoveAt(2);
            }
            else if (e.Item.Cells[0].Text == "Pick")
            {
                e.Item.Cells[0].CssClass = "bolditem";
                e.Item.Cells[1].CssClass = "bolditem";
                e.Item.Cells[2].CssClass = "bolditem";
                e.Item.Cells[3].CssClass = "bolditem";
                e.Item.Cells[4].CssClass = "bolditem";
            }
        }

        private void AddExtraRows(DataSet ds)
        {
            int i = 0;
            int lastround = 0;
            while (i < ds.Tables[0].Rows.Count)
            {

                if (ds.Tables[0].Rows[i]["PlayerId"] == DBNull.Value)
                {
                    // need to add the "make a pick" link if this is the current pick
                    if (DBUtilities.DraftIsOpen())
                    {
                        int pickId = Convert.ToInt32(ds.Tables[0].Rows[i]["DraftPickId"].ToString(), 10);
                        ds.Tables[0].Rows[i]["Player"] = "<a href=\"SelectPlayer.aspx?PickId=" + pickId + "\">Select Now</a>";
                    }
                }

                if (ds.Tables[0].Rows[i]["Round"].ToString() != lastround.ToString())
                {
                    DataRow x = ds.Tables[0].NewRow();
                    x["Player"] = -1;
                    x["Pick"] = ds.Tables[0].Rows[i]["Round"];
                    x["PickTime"] = ds.Tables[0].Rows[i]["PickDate"];
                    lastround = Convert.ToInt32(ds.Tables[0].Rows[i]["Round"].ToString(), 10);
                    ds.Tables[0].Rows.InsertAt(x, i);

                    x = ds.Tables[0].NewRow();
                    x["Pick"] = "Pick";
                    x["PickTime"] = "Time";
                    x["OwningTeamAbbrev"] = "Owning";
                    x["OriginalTeamAbbrev"] = "Original";
                    x["Player"] = "Player";
                    i++;
                    ds.Tables[0].Rows.InsertAt(x, i);
                }
                i++;
            }
        }
    }
}