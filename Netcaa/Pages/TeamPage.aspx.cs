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
    public partial class TeamPage : System.Web.UI.Page
    {
        private bool TeamOwner = false;
        private bool ProtectedAvailable = false;

        protected void Page_Load( object sender, EventArgs e )
        {
            DataSet owners = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTeamOwners", Request.QueryString["TeamId"] );
            if( Page.User.Identity.Name == owners.Tables[0].Rows[0]["username"].ToString() )
            {
                TeamOwner = true;
            }

            ProtectedAvailable = DBUtilities.ProtectedListsAvailable();

            if( !IsPostBack )
            {
                DataSet roster = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetRoster", Request.QueryString["TeamId"] );
                gvRoster.DataSource = roster;
                gvRoster.DataBind();
                if( !TeamOwner || !ProtectedAvailable )
                {
                    // it pains me that i have to use a numeric index :-(
                    gvRoster.Columns[7].Visible = false;
                }

                hlEmail.NavigateUrl = "mailto:" + owners.Tables[0].Rows[0]["EmailAddress"];
                hlEmail.Text = owners.Tables[0].Rows[0]["FirstName"] + " " + owners.Tables[0].Rows[0]["LastName"];
                string OwnerInfo = "";
                if( owners.Tables[0].Rows[0]["City"].ToString().Length > 0 )
                {
                    OwnerInfo += "<br />" + owners.Tables[0].Rows[0]["City"]
                        + ", " + owners.Tables[0].Rows[0]["State"];
                }
                if( owners.Tables[0].Rows[0]["IMInfo"].ToString().Length > 0 )
                {
                    OwnerInfo += "<br />" + owners.Tables[0].Rows[0]["IMInfo"];
                }
                if( OwnerInfo.Length > 0 )
                {
                    litOwnerInfo.Text = OwnerInfo;
                }

                if( TeamOwner )
                {
                    litEditLink.Text = "<br /><a  style=\"font-size:x-small;\" href=\"EditInfo.aspx\">Edit Info</a>";
                }

                lblPageTitle.Text = owners.Tables[0].Rows[0]["TeamName"].ToString();

                DataSet record = SqlHelper.ExecuteDataset( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTeamStatus", Request.QueryString["TeamId"] );
                if( record.Tables[0].Rows.Count == 1 )
                {
                    lblRecord.Text = record.Tables[0].Rows[0]["Wins"].ToString() + '-' + record.Tables[0].Rows[0]["Losses"].ToString();
                }

                hlTeamHistory.NavigateUrl += Request.QueryString["TeamId"];

                DateTime LastLogin = (DateTime)SqlHelper.ExecuteScalar( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetLastLogin", owners.Tables[0].Rows[0]["OwnerId"] );
                lblLastLogin.Text = LastLogin.ToString();
            }
        }

        protected void cbProtected_OnCheckedChanged( object sender, EventArgs e )
        {
            if( TeamOwner )
            {
                CheckBox cbProtected = (CheckBox)sender;
                GridViewRow row = (GridViewRow)cbProtected.NamingContainer;

                bool status = cbProtected.Checked;
                int id = int.Parse( gvRoster.DataKeys[row.RowIndex]["PlayerId"].ToString() );

                // now write to the db
                int dontcare = (int)SqlHelper.ExecuteNonQuery( System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spSetProtectedStatus", id, status );
            }
        }

        protected void gvRoster_RowDataBound( object sender, GridViewRowEventArgs e )
        {
            if( !TeamOwner && e.Row.RowIndex >= 0 && ProtectedAvailable )
            {
                CheckBox cb = (CheckBox)e.Row.FindControl( "cbProtected" );
                if( cb.Checked )
                {
                    Label l = (Label)e.Row.FindControl( "lblProtected" );
                    l.Visible = true;
                }
            }
        }

    }
}