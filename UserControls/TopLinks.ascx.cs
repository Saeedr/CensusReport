using Shahab.CensusRreport.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusReport.UserControls
{
    public partial class TopLinks : System.Web.UI.UserControl
    {
        private bool IsLogined
        {
            get
            {
                return Shahab.CensusRreport.Library.User.IsLogined();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
                SetControls();
        }

        private void SetControls()
        {
            if (!IsLogined)
            {
                lnkLogin.Text = "ورود به سیستم";
                lnkLogOut.Visible = false;
                lnkRegisterInformation.CssClass = "last";
            }
            else
            {
                lnkLogin.Text = "پروفایل کاربری";
                lnkLogOut.Visible = true;
                lnkRegisterInformation.CssClass = "";
            }
        }
        protected void lnkSignup_Click(object sender, EventArgs e)
        {
            User.LogOut();
            Response.Redirect("/Components/FamilySignUp.aspx");
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            if (!IsLogined)
            {
                Response.Redirect("/Components/FamilyLogin.aspx");
            }
            else
            {
                Response.Redirect("/Components/FamilySignUp.aspx");
            }
        }

        protected void lnkRegisterInformation_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Components/FamilyAdd.aspx");
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Components/FamilyLogin.aspx?logout=1");
        }
    }
}