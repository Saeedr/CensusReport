using Shahab.CensusRreport.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusReport.UserControls
{
    public partial class Welcome : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName();
        }

        public void SetName()
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (User.IsLogined())
            {
                var user = (User)Session["authUserId"];
                firstName = user.FirstName;
                lastName = user.LastName;
                lblWwelcome.Text = String.Format("کاربر : {0} {1}", firstName, lastName);
            }
            else
                Visible = false;
        }
    }
}