using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace netcaa.Controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Sets the titel of the page that will appear in the html.
        /// </summary>
        public string Title
        {
            set
            {
                lblPageTitle.Text = value;
            }
        }
    }
}