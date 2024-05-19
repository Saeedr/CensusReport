using Shahab.CensusRreport.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusRreport.Components
{
    public partial class FamilySignUp : System.Web.UI.Page
    {
        private bool IsLogined
        {
            get
            {
                return Shahab.CensusRreport.Library.User.IsLogined();
            }
        }
        public int UserId
        {
            get
            {
                if (IsLogined)
                {
                    var userId = ((User)Session["authUserId"]).UserId;
                    return userId > 0 ? userId : -1;
                }
                return -1;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                SetControls();
                if (UserId != -1)
                {
                    txtNationalCode.Enabled = false;
                    LoadUser();
                }
            }
        }

        /// <summary>
        /// بارگذاری اطلاعات خانوار از روی پایگاه داده
        /// </summary>
        private void LoadUser()
        {
            User user = Shahab.CensusRreport.Library.User.GetUser(UserId);
            if(user != null)
            {
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtNationalCode.Text = user.NationalCode;
                txtMobile.Text = user.MobileNumber;
                txtEmail.Text = user.Email;
                txtOldPassword.Attributes.Remove("required");
                txtPassword.Attributes.Remove("required");
                txtConfirmPassword.Attributes.Remove("required");
            }
            else
            {
                Shahab.CensusRreport.Library.User.LogOut();
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

            if (IsLogined)
            {
                lblPassword.Text = "پسورد جدید:";
                pnlOldPassword.Visible = true;
                btnSignup.Text = "ویرایش";
            }
            else
            {
                lblPassword.Text = "ایجاد رمز ورود:";
                pnlOldPassword.Visible = false;
                btnSignup.Text = "ثبت نام";
            }

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                liMessage.Text = string.Empty;
                var success = false;
                var validOldPassword = true;
                var uniqueUser = true;
                CaptchaControl.Validate();
                if (CheckValidField() && CaptchaControl.IsValid)
                {
                    var user = FetchUser();
                    if (user.UserId != -1 && txtPassword.Text != string.Empty)
                    {
                        validOldPassword = user.CheckEqualOldPassword(txtOldPassword.Text);
                        if (validOldPassword)
                            uniqueUser = !user.CheckExistUser(user.NationalCode, txtPassword.Text);
                    }
                    else if (user.UserId == -1)
                    { 
                        uniqueUser = user.CheckUniqueUser(txtNationalCode.Text, txtPassword.Text); 
                    }

                    if (validOldPassword)
                    {
                        if (uniqueUser)
                        {
                            if (UserId == -1)
                                success = user.Save(user);
                            else
                                success = user.Update(user);

                            if (success)
                            {
                                if (UserId == -1)
                                {
                                    liMessage.Text = "<li>کاربر گرامی ثبت نام شما با موفقیت انجام شد، برای ورود <a href='/Components/FamilyLogin.aspx'>اینجا</a> را کلیک کنید.</li>";
                                    SMSSend(user.UserId);
                                }
                                else
                                {
                                    liMessage.Text = "<li>کاربر گرامی اطلاعات شما با موفقیت ویرایش شد.</li>";
                                }
                                pnlMessage.CssClass = "success";
                                fsSignUp.Visible = false;
                            }
                            else
                            {
                                liMessage.Text = "امکان ثبت نام وجود ندارد";
                                pnlMessage.CssClass = "errorWrapper";
                            }
                        }
                        else
                        {
                            liMessage.Text = "کاربری با این اطلاعات قبلاً ثبت نام کرده است.";
                            pnlMessage.CssClass = "errorWrapper";
                        }
                    }
                    else{
                            liMessage.Text += "<li>رمز ورود قدیمی صحیح نمی باشد.</li>";
                            pnlMessage.CssClass = "errorWrapper";
                        }
                }
                else
                {
                    if(UserId != -1)
                    {
                        txtOldPassword.Attributes.Remove("required");
                        txtPassword.Attributes.Remove("required");
                        txtConfirmPassword.Attributes.Remove("required");
                    }

                    if (liMessage.Text != string.Empty)
                        pnlMessage.CssClass = "errorWrapper";
                }
            }
            catch
            {
                liMessage.Text = "امکان ثبت نام وجود ندارد";
            }
        }
        /// <summary>
        /// ارسال پیامک بعد از ثبت نام عضو به شماره موبایل وارد شده در هنگام ثبت نام
        /// </summary>
        /// <param name="userId"></param>
        private void SMSSend(int userId)
        {
            string username = "edsoft_ghorbani";
            string password = "!t0rbit";
            string[] senderNumbers = { "10008642" };
            string[] recipientNumbers = { txtMobile.Text };
            string[] messageBodies = { String.Format("اطلاعات شما در سامانه طرح دريافت خدمات سلامت با کد پيگيری {0} ثبت گرديد\r\n معاونت بهداشت وزارت بهداشت و درمان.",userId.ToString()) };
            DateTime date = DateTime.Now;
            string[] senddate = { Convert.ToString(ConvertDatetimeToUnixTimeStamp(date)) };
            int[] messageClasses = { };
            long[] MessageIDs = { };

            //string serviceUrl = string.Format(Configurations.SMSServiceUrl, username, password, senderNumbers[0], recipientNumbers[0], messageBodies[0]);
            //HttpGet(serviceUrl);
            Shahab.CensusReport.SMSService.v2 ws = new Shahab.CensusReport.SMSService.v2();
            MessageIDs = ws.SendSMS(username, password, senderNumbers, recipientNumbers, messageBodies, senddate, null, null);
        }

        public string HttpGet(string URI)
        {
            try 
            { 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                request.Method = "GET";
                string data = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return data;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// واکشی اطلاعات کاربر از روی کنترل ها
        /// </summary>
        /// <returns></returns>
        private User FetchUser()
        {
            User user = new User();
            user.UserId = UserId;
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.NationalCode = txtNationalCode.Text;
            user.MobileNumber = txtMobile.Text;
            user.Password = txtPassword.Text;
            user.RegisterDate = DateTime.Now;
            user.Email = txtEmail.Text;
            user.Status = Enumurations.Status.Enabled;
            user.Priority = 0;

            return user;
        }

        #region Check validation
        /// <summary>
        /// بررسی اعتبارسنجی کنترل های کاربر
        /// </summary>
        /// <returns></returns>
        private bool CheckValidField()
        {
            var validNumeric = new Tuple<bool, bool>(true, true);
            
            bool valid = true;
            if(txtFirstName.Text == string.Empty)
            {
                valid = false;
                liMessage.Text += "<li>نام را وارد کنید</li>";
            }
            if (txtFirstName.Text == string.Empty)
            {
                valid = false;
                liMessage.Text +="<li>نام خانوادگی را وارد کنید</li>";
            }
            if (txtNationalCode.Text == string.Empty)
            {
                valid = false;
                liMessage.Text +="<li>شماره ملی را وارد کنید</li>";
            }
            if (txtMobile.Text == string.Empty)
            {
                valid = false;
                liMessage.Text +="<li>تلفن همراه را وارد کنید</li>";
            }

            if (UserId == -1)
            {
                if (txtPassword.Text == string.Empty)
                {
                    valid = false;
                    liMessage.Text += "<li>رمز ورود را وارد کنید</li>";
                }
                else
                {
                    if (Validation.HasArabicAndPersianCharacters(txtPassword.Text))
                    {
                        liMessage.Text += "<li>رمز ورود شما نباید شامل حروف فارسی و عربی باشد.</li>";
                        valid = false;
                    }
                    if (txtPassword.Text.Length < 6)
                    {
                        valid = false;
                        liMessage.Text += "<li>رمز ورود باید حداقل شامل شش کاراکتر باشد.</li>";
                    }
                }
                if (txtConfirmPassword.Text == string.Empty)
                {
                    valid = false;
                    liMessage.Text += "<li>تکرار رمز ورود را وارد کنید</li>";
                }
            }
            else
            {
                if (txtPassword.Text != string.Empty || txtConfirmPassword.Text != string.Empty)
                {
                    if(txtOldPassword.Text == string.Empty)
                    {
                        valid = false;
                        liMessage.Text += "<li>رمز ورود قدیمی خود را وارد کنید.</li>";
                    }

                    if (txtPassword.Text == string.Empty)
                    {
                        valid = false;
                        liMessage.Text += "<li>رمز ورود را وارد کنید</li>";
                    }
                    else
                    {
                        if (Validation.HasArabicAndPersianCharacters(txtPassword.Text))
                        {
                            liMessage.Text += "<li>رمز ورود شما نباید شامل حروف فارسی و عربی باشد.</li>";
                            valid = false;
                        }
                        if (txtPassword.Text.Length < 6)
                        {
                            valid = false;
                            liMessage.Text += "<li>رمز ورود باید حداقل شامل شش کاراکتر باشد.</li>";
                        }
                    }

                    if (txtConfirmPassword.Text == string.Empty)
                    {
                        valid = false;
                        liMessage.Text += "<li>تکرار رمز ورود را وارد کنید</li>";
                    }
                }
            }
            if (!String.IsNullOrEmpty(txtNationalCode.Text))
            {
                validNumeric = Validation.IsNemericValue(txtNationalCode.Text, txtNationalCode.MaxLength);
                if (!validNumeric.Item1)
                {
                    liMessage.Text +="<li>برای شماره ملی عدد وارد کنید.</li>";
                    valid = false;
                }
                if (!validNumeric.Item2)
                {
                    liMessage.Text +="<li>برای شماره ملی باید ده رقم وارد کنید.</li>";
                    valid = false;
                }
                if (Validation.CheckValidNationalCode(txtNationalCode.Text) == null)
                {
                    liMessage.Text += "<li>کد ملی نامعتبر می باشد.</li>";
                    valid = false;
                }
            }

            if (!String.IsNullOrEmpty(txtMobile.Text))
            {
                validNumeric = Validation.IsNemericValue(txtMobile.Text, txtMobile.MaxLength);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای تلفن همراه  عدد وارد کنید.</li>";
                    valid = false;
                }
                if (!validNumeric.Item2)
                {
                    liMessage.Text += "<li>برای تلفن همراه باید یازده رقم وارد کنید.</li>";
                    valid = false;
                }
                if (validNumeric.Item1 && validNumeric.Item2 && !Validation.ValidMobile(txtMobile.Text))
                {
                    liMessage.Text += "<li>تلفن همراه نامعتبر می باشد.</li>";
                    valid = false;
                }
            }

            if (txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
            {
                if(txtPassword.Text.CompareTo(txtConfirmPassword.Text) != 0)
                {
                    liMessage.Text +="<li>رمز ورود و تکرار آن یکی نمی باشد</li>";
                    valid = false;
                }
            }

            if(!string.IsNullOrEmpty(txtEmail.Text))
                if (!Validation.ValidEmail(txtEmail.Text))
                {
                    liMessage.Text += "<li>ایمیل نامعتبر می باشد</li>";
                    valid = false;
                }

            return valid;
        }

        #endregion
        /// <summary>
        /// بررسی معتبر بودن کد ملی از طریق سرویس
        /// </summary>
        /// <param name="nationalCode">کد ملی</param>
        /// <returns></returns>
        [WebMethod]
        public static Boolean IsValidNationalCode(String nationalCode)
        {
           var validNationalCode = true;
            PMIClient.DataModel.PersonInfoVO personelInfoVO = Validation.CheckValidNationalCode(nationalCode);
            validNationalCode = (personelInfoVO != null);
            return validNationalCode;
        }

        static DateTime EPOCH = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        /// <summary>
        /// تبدیل تاریخ به فرمت مورد نظر برای هنگام ارسال پیامک
        /// </summary>
        /// <param name="date">تاریخ</param>
        /// <param name="Time_Zone"></param>
        /// <returns></returns>
        public static double ConvertDatetimeToUnixTimeStamp(DateTime date, int Time_Zone = 0)
        {
            TimeSpan The_Date = (date - EPOCH);
            return Math.Floor(The_Date.TotalSeconds) - (Time_Zone * 3600);
        }
    }
}