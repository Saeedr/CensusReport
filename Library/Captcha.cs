
using System;

namespace Shahab.CensusRreport.Library
{
    public class Captcha
    {


        private double _fontSize;
        public string FontFamily
        {
            get { return _fontFamily; }
            set
            {
                if (value != string.Empty && value != null)
                    this._fontFamily = value;
                else
                    this._fontFamily = "Arial";
            }
        }

        private string _fontFamily;
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                try
                {
                    if (value <= 10 || value >= 24)
                        this._fontSize = 16;
                    else
                        this._fontSize = value;
                }
                catch (Exception ex)
                {
                    this._fontSize = 16;
                }
            }
        }

        private string _textColor;
        public string TextColor
        {
            get { return _textColor; }
            set
            {
                if (value == string.Empty || value == null)
                    this._textColor = "Black";
                else
                    this._textColor = value;
            }
        }

        private string _backgroundImagePath;
        public string BackgroundImagePath
        {
            get { return _backgroundImagePath; }
            set
            {
                this._backgroundImagePath = value;
            }
        }

        public System.Drawing.Font GetFont()
        {
            return new System.Drawing.Font(FontFamily, (float)FontSize);
        }

    }


}