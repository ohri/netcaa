using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;
using Microsoft.ApplicationBlocks.Data;
using System.Web.Security;
using Logger;
using System.Data;

namespace netcaa.Pages
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (TextPassword.Text.Length > 0)
                {
                    // get the hashed password from the db
                    DataSet ds = null;
                    //					try
                    //					{
                    ds = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                        "spGetPassword", TextUsername.Text);
                    if (SaltedHash.ValidatePassword(TextPassword.Text, (string)ds.Tables[0].Rows[0][0]))
                    {
                        Log.AddLogEntry(Logger.LogEntryTypes.Login, TextUsername.Text, "User successfully logged in");
                        FormsAuthentication.RedirectFromLoginPage(TextUsername.Text, false);
                    }
                    else
                    {
                        Log.AddLogEntry(Logger.LogEntryTypes.FailedLogin, TextUsername.Text, "User failed to log in");
                        lblMessage.Text = "Login attempt failed.";
                    }
                    //}
                    //catch( Exception ex )
                    //{
                    //    Log.AddLogEntry( Logger.LogEntryTypes.FailedLogin, TextUsername.Text, "User failed to log in" );
                    //    lblMessage.Text = "Unknown username";
                    //}

                }
                else
                {
                    Log.AddLogEntry(Logger.LogEntryTypes.FailedLogin, TextUsername.Text, "User failed to log in");
                    lblMessage.Text = "Login attempt failed.";
                }
            }
        }
    }
}