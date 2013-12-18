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
using Encryption;

namespace netcaa.Pages
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DBUtilities.IsUserAdmin(Page.User.Identity.Name))
            {
                Response.Redirect("/Static/notauthorized.html");
            }

            if (!this.IsPostBack)
            {
                // Put user code to initialize the page here
                DataSet ds = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetOwners");
                this.ddlOwners.DataSource = ds;
                ddlOwners.DataTextField = "username";
                ddlOwners.DataValueField = "ownerid";
                ddlOwners.DataBind();

                cbDraftOpen.Checked = DBUtilities.DraftIsOpen();
            }

            lblPickMessage.Text = "";
        }

        protected void ButtonResetPassword_Click(object sender, System.EventArgs e)
        {
            string username = ddlOwners.SelectedItem.Text;
            string newhashed = SaltedHash.CreateSaltedPasswordHash(username);
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spSetOwnerPassword", username, newhashed);
            lblMessage.Text = "Password has been reset to " + username + " for user " + username;
        }

        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            int round = 0;
            int pick = 0;

            try
            {
                round = Convert.ToInt32(tbRound.Text);
                pick = Convert.ToInt32(tbPick.Text);
            }
            catch
            {
                lblPickMessage.Text = "Invalid pick information.";
                return;
            }

            if (round < 1 || round > 12 || pick < 1 || pick > 16)
            {
                lblPickMessage.Text = "Invalid pick information.";
                return;
            }

            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spUndoDraftPick", round, pick);
            lblPickMessage.Text = "Pick undone.";
        }

        protected void cbDraftOpen_CheckedChanged(object sender, System.EventArgs e)
        {
            int status = 0;
            if (cbDraftOpen.Checked)
            {
                status = 1;
            }
            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spSetDraftStatus", status);
        }
    }
}