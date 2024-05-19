
using System;
using SpeechLib;
using Shahab.CensusRreport.Library;

namespace Shahab.CensusRreport.UserControls
{
    public partial class CaptchaControl : System.Web.UI.UserControl
    {
        #region Properties

        private int _captchaLength;
        public int CaptchaLength
        {
            get { return _captchaLength; }
            set
            {
                try
                {
                    int k = Convert.ToInt32(value);
                    if (k < 3 || k > 10)
                        _captchaLength = 4;
                    else
                        _captchaLength = k;
                }
                catch (Exception ex)
                {
                    _captchaLength = 4;
                }
            }
        }


        private string _fontFamily;
        public string FontFamily
        {
            get { return _fontFamily; }
            set
            {
                if (value != string.Empty && value != null)
                    _fontFamily = value;
                else
                    _fontFamily = "Arial";
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                try
                {
                    _fontSize = Convert.ToInt32(value);
                    if (_fontSize <= 10 || _fontSize >= 24)
                        _fontSize = 16;
                }
                catch (Exception ex)
                {
                    _fontSize = 16;
                }
            }
        }


        private string _backgroundImagePath;
        public string BackgroundImagePath
        {
            get { return _backgroundImagePath; }
            set
            {
                if (System.IO.File.Exists(Server.MapPath(value)))
                    _backgroundImagePath = value;
                else
                    _backgroundImagePath = System.Configuration.ConfigurationManager.AppSettings["defaultImagePath"];
            }
        }

        private string _textColor;
        public string TextColor
        {
            get { return _textColor; }
            set
            {
                if (value == string.Empty || value == null)
                    _textColor = "Black";
                else
                    _textColor = value;
            }
        }


        private string _characterSet;
        public string CharacterSet
        {
            get { return _characterSet; }
            set
            {
                if (value == "" || value == null)
                    _characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
                else
                    _characterSet = value;
            }
        }
        private bool _isValid = false;
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        private bool _reload = false;

        public bool Reload
        {
            get { return _reload; }
            set { _reload = value; }
        }
        #endregion

        #region field initialisation

        public Captcha cc;

        #endregion

        private Captcha GetCaptchaClass()
        {
            if (Session["CaptchaClass"] != null)
                cc = (Captcha)Session["CaptchaClass"];
            else
                cc = new Captcha();

            cc.FontSize = this.FontSize;
            cc.FontFamily = this.FontFamily;
            cc.BackgroundImagePath = this.BackgroundImagePath;
            cc.TextColor = this.TextColor;
            return cc;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                    
            SetValues();

            cc = GetCaptchaClass();

            if (!Page.IsPostBack)
            {
                LoadCaptcha();
            }

        }

        private string GetRandomText()
        {
            char[] letters = CharacterSet.ToCharArray();
            string text = string.Empty;
            Random r = new Random();
            int num = -1;

            for (int i = 0; i < this.CaptchaLength; i++)
            {
                num = (int)(r.NextDouble() * (letters.Length - 1));
                text += letters[num].ToString();
            }
            return text;
        }

        private void LoadCaptcha()
        {
            string text = GetRandomText();
            
            ViewState.Add("captcha", text);
            Session.Add("CaptchaClass", cc);//add captcha object to Session
            Session.Add("captcha", text);//add captcha text to session
            imgCaptcha.ImageUrl = "/UserControls/CaptchaHandler.ashx";
        }

        private void SetValues()
        {
            if (CharacterSet == null)
                CharacterSet = "";
            if(CaptchaLength == 0)
                CaptchaLength = 4;

            if(BackgroundImagePath == null)
                BackgroundImagePath = "";
            
            if(FontFamily == null)
                FontFamily = "";            
            
            if(FontSize == 0)
                FontSize = 0.0;

            if(TextColor == null)
                TextColor = "";
        }

        public void Validate()
        {
            if (txtCaptcha.Text != string.Empty)
            {
                string text = txtCaptcha.Text;
                if (text.CompareTo((string)ViewState["captcha"]) == 0)
                {
                    IsValid = true;
                    lblResult.Text = string.Empty;
                }
                else
                {
                    IsValid = false;
                    lblResult.Text = "کد وارد شده صحیح نیست.";
                    LoadCaptcha();
                    txtCaptcha.Text = string.Empty;
                }
            }
            else
            {
                IsValid = false;
                lblResult.Text = "کد را وارد کنید.";
            }
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        protected void lblSpeak_Click(object sender, EventArgs e)
        {
            SpVoice voice = new SpVoice();

            char[] text = ((string)ViewState["captcha"]).ToCharArray();


            for (int i = 0; i < text.Length; i++)
            {
                voice.Speak(text[i].ToString(), SpeechVoiceSpeakFlags.SVSFDefault);
            }
        }	

    }
}