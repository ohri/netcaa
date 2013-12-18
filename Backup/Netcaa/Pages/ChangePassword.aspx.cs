using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Encryption;
using Microsoft.ApplicationBlocks.Data;
using Logger;

namespace netcaa.Pages
{
	/// <summary>
	/// Summary description for ChangePassword.
	/// </summary>
	public partial class ChangePassword : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btnChangePassword_Click(object sender, System.EventArgs e)
		{
            DataSet ds = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
				"spGetPassword", Page.User.Identity.Name );
			if( SaltedHash.ValidatePassword( txtOldPassword.Text, ds.Tables[0].Rows[0][0].ToString() ) )
			{
				if( txtNewPassword.Text == txtConfirmNewPass.Text )
				{
					string newpass = SaltedHash.CreateSaltedPasswordHash( txtConfirmNewPass.Text );
                    SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
						"spSetOwnerPassword", Page.User.Identity.Name, newpass );
					Log.AddLogEntry( LogEntryTypes.ChangedPassword, Page.User.Identity.Name, "Changed password" );
					lblMessage.Text = "Password successfully changed.";
				}
				else
				{
					lblMessage.Text = "You typed two different new passwords, try again.";
				}
			}
			else
			{
				lblMessage.Text = "Old password is incorect.";
			}
		}
	}
}
