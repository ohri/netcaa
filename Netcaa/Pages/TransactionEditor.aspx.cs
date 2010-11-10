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
    public partial class TransactionEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "New")
                {
                    // this is a brand new transaction; create one and stash the id
                    object x = SqlHelper.ExecuteScalar(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                        "spCreateTransaction", Session["TeamId"]);
                    int TransactionId = int.Parse(x.ToString());
                    Session["TransactionId"] = TransactionId;
                }
                else
                {
                    // stash the transaction id
                    if (Request.QueryString["TransactionId"] != null)
                    {
                        Session["TransactionId"] = Request.QueryString["TransactionId"];
                    }
                    // else, something really bad is happening TODO
                }

                // if we got called with something in the selected players session area
                // add it to the transaction
                if (Session["SelectedPlayers"] != null)
                {
                    int[] players = (int[])Session["SelectedPlayers"];
                    foreach (int i in players)
                    {
                        AddTransactionItem((TransactionTypes)Session["Mode"], i);
                    }
                    Session["SelectedPlayers"] = null;
                    Session["Mode"] = null;
                }

                // now populate the page - signings
                DataSet dsItems = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spFetchTransactionItems", Session["TransactionId"], TransactionTypes.SignPlayers);
                lbComing.DataSource = dsItems;
                lbComing.DataValueField = "PlayerId";
                lbComing.DataTextField = "Player";
                lbComing.DataBind();

                // now populate the page - cuts
                dsItems = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spFetchTransactionItems", Session["TransactionId"], TransactionTypes.CutPlayers);
                lbGoing.DataSource = dsItems;
                lbGoing.DataValueField = "PlayerId";
                lbGoing.DataTextField = "Player";
                lbGoing.DataBind();

                // now populate the page - activations
                dsItems = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spFetchTeamInjured", Session["TeamId"], Session["TransactionId"]);
                foreach (DataRow r in dsItems.Tables[0].Rows)
                {
                    cblActivations.Items.Add(new ListItem(r["Player"].ToString(), r["PlayerId"].ToString()));
                    if (r["Activate"].ToString() == "1")
                    {
                        cblActivations.Items[cblActivations.Items.Count - 1].Selected = true;
                    }
                }

                // now populate the page - ir's
                dsItems = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spFetchTransactionItems", Session["TransactionId"], TransactionTypes.IRPlayers);
                lbDeactivate.DataSource = dsItems;
                lbDeactivate.DataValueField = "PlayerId";
                lbDeactivate.DataTextField = "Player";
                lbDeactivate.DataBind();

                // get to sign and to cut
                int tocut = 0;
                SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTransactionInfo", Session["TransactionId"], tocut);
                if (tocut != 0)
                {
                    tbCutAtLeast.Text = tocut.ToString();
                }
            }
        }

        private void AddTransactionItem(TransactionTypes ttype, int playerid)
        {
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spAddTransactionItem", Session["TransactionId"], ttype, playerid);
        }

        protected void btnAddSignings_Click(object sender, EventArgs e)
        {
            Session["Mode"] = TransactionTypes.SignPlayers;
            SaveTransactionState();
            Response.Redirect("/Pages/SelectPlayers.aspx");
        }

        protected void btnAddCuts_Click(object sender, EventArgs e)
        {
            Session["Mode"] = TransactionTypes.CutPlayers;
            SaveTransactionState();
            Response.Redirect("/Pages/SelectPlayers.aspx");
        }

        protected void btnAddToIR_Click(object sender, EventArgs e)
        {
            Session["Mode"] = TransactionTypes.IRPlayers;
            SaveTransactionState();
            Response.Redirect("/Pages/SelectPlayers.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveTransactionState();

            // clear the transaction id from the session
            Session["TransactionId"] = null;

            // go to transaction view
            Response.Redirect("/Pages/Transactions.aspx");
        }

        protected void SaveTransactionState()
        {
            // wipe out all of the current transaction items
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spRemoveTransactionItems", Session["TransactionId"]);

            // save off the transaction details
            // signings
            foreach (ListItem i in lbComing.Items)
            {
                AddTransactionItem(TransactionTypes.SignPlayers, Convert.ToInt32(i.Value));
            }

            // cuts
            foreach (ListItem i in lbGoing.Items)
            {
                AddTransactionItem(TransactionTypes.CutPlayers, Convert.ToInt32(i.Value));
            }

            // activations

            foreach (ListItem i in cblActivations.Items)
            {
                if (i.Selected)
                {
                    AddTransactionItem(TransactionTypes.ActivatePlayers, Convert.ToInt32(i.Value));
                }
            }

            // ir's
            foreach (ListItem i in lbDeactivate.Items)
            {
                AddTransactionItem(TransactionTypes.IRPlayers, Convert.ToInt32(i.Value));
            }

            // to cut
            int cutatleast = 0;
            if (tbCutAtLeast.Text.Length > 0)
            {
                cutatleast = Convert.ToInt32(tbCutAtLeast.Text);
            }
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spUpdateTransactionInfo", Session["TransactionId"], cutatleast);
        }

        protected void btnDeleteTransaction_Click(object sender, EventArgs e)
        {
            // delete the transaction
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spDeleteTransaction", Session["TransactionId"]);

            // clear the transaction id from the session
            Session["TransactionId"] = null;

            // go to transaction view
            Response.Redirect("/Pages/Transactions.aspx");
        }

        protected void btnRemoveSigning_Click(object sender, EventArgs e)
        {
            remove(lbComing);
        }
        protected void btnUpSigning_Click(object sender, EventArgs e)
        {
            move(lbComing, -1);
        }
        protected void btnDownSigning_Click(object sender, EventArgs e)
        {
            move(lbComing, 1);
        }
        protected void btnRemoveCut_Click(object sender, EventArgs e)
        {
            remove(lbGoing);
        }
        protected void btnUpCut_Click(object sender, EventArgs e)
        {
            move(lbGoing, -1);
        }
        protected void btnDownCut_Click(object sender, EventArgs e)
        {
            move(lbGoing, 1);
        }
        protected void btnRemoveIR_Click(object sender, EventArgs e)
        {
            remove(lbDeactivate);
        }

        protected void remove(ListBox lb)
        {
            int[] selected = lb.GetSelectedIndices();
            foreach (int i in selected)
            {
                lb.Items.RemoveAt(i);
            }
        }

        protected void move(ListBox lb, int amount)
        {
            int i = lb.SelectedIndex;
            ListItem item = lb.Items[i];
            lb.Items.RemoveAt(i);
            lb.Items.Insert(i + amount, item);
        }
    }
}