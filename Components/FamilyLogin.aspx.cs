using Shahab.CensusRreport.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusReport.Components
{
    public partial class FamilyLogin : System.Web.UI.Page
    {
        private bool _logOut;

        public bool LogOut
        {
            get
            {
                if (Request["logout"] != null)
                {
                    return Convert.ToInt32(Request["logout"]) == 1 ? true : false;
                }
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                SetControls();
                if(!LogOut)
                {
                    if(IsLogined())
                        Response.Redirect("/Components/FamilyAdd.aspx", true);
                }
                else
                {
                    Shahab.CensusRreport.Library.User.LogOut();
                    Response.Redirect("/Components/FamilyLogin.aspx", true);
                }
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            Configurations.RegisterScript(this.Page, this.GetType(), "jquery-loader", "/Scripts/jquery-loader.js");
            Configurations.RegisterScript(this.Page, this.GetType(), "jquery.password-checker", "/Scripts/jquery.password-checker.js");
            base.OnPreRender(e);
        }
        private void SetControls()
        {
            btnLogin.Text = "ورود";

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            liMessage.Text = string.Empty;
            if (!IsLogined())
            {
                if (Validate())
                {
                    var user = Shahab.CensusRreport.Library.User.GetUser(txtNationalCode.Text, txtPassword.Text);
                    if (user != null)
                    {
                        user.UpdateLastLogin(user.UserId);
                        Session["authUserId"] = user;
                        Response.Redirect("/Components/FamilyAdd.aspx", true);
                        pnlMessage.Visible = false;
                    }
                    else
                    {
                        Shahab.CensusRreport.Library.User.LogOut();
                        liMessage.Text += "<li>کاربری با چنین مشخصات ثبت نشده است.</li>";
                        pnlMessage.CssClass = "errorWrapper";
                        pnlMessage.Visible = true;
                    }
                }
                else
                {
                    pnlMessage.CssClass = "errorWrapper";
                    pnlMessage.Visible = true;
                }
            }
        }
        /// <summary>
        /// اعتبار سنجی کنترل های ورود به سامانه
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(txtNationalCode.Text))
            {
                if (Validation.CheckValidNationalCode(txtNationalCode.Text) == null)
                {
                    liMessage.Text += "<li>شماره ملی نامعتبر می باشد.</li>";
                    valid = false;
                }
            }
            else
            {
                liMessage.Text += "<li>شماره ملی را وارد کنید.</li>";
                valid = false;
            }
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                if (Validation.HasArabicAndPersianCharacters(txtPassword.Text))
                {
                    liMessage.Text += "<li>رمز ورود شما نباید شامل حروف فارسی و عربی باشد.</li>";
                    valid = false;
                }
                if(txtPassword.Text.Length < 6)
                {
                    liMessage.Text += "<li>رمز ورود باید حداقل شامل شش کاراکتر باشد.</li>";
                    valid = false;
                }
            }
            else
            {
                liMessage.Text += "<li>رمز ورود را وارد کنید.</li>";
                valid = false;
            }
            return valid;
        }
        /// <summary>
        /// بررسی login بودن کاربر
        /// </summary>
        /// <returns></returns>
        private bool IsLogined()
        {
            return (Session["authUserId"] != null);
        }
    }
}