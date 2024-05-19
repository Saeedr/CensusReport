
using System;
using System.Web;
using System.Drawing;
using Shahab.CensusRreport.Library;

namespace Shahab.CensusRreport.UserControls
{
    public class CaptchaHandler : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {

        private Captcha cc;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";

            if (context.Session != null)
            {
                if (context.Session["CaptchaClass"] != null)
                {
                    cc = (Captcha)context.Session["CaptchaClass"];
                }
                else
                {
                    cc = new Captcha();
                }
            }           

            string text = string.Empty;
            if(context.Session["captcha"] !=null)
                text = (string)context.Session["captcha"];


            int imageWidth = Convert.ToInt32(((cc.FontSize + 8) * text.Length));
            int imageHeight = Convert.ToInt32(cc.FontSize * 2.5);


            Bitmap bmp = new Bitmap(imageWidth, imageHeight);
            var Grph = Graphics.FromImage(bmp);
            Grph.FillRectangle(new SolidBrush(Color.Lavender), 0, 0, bmp.Width, bmp.Height);

            var grp = Graphics.FromImage(bmp);
            Image background = Image.FromFile(HttpContext.Current.Server.MapPath(cc.BackgroundImagePath));
            grp.DrawImage(background, new Rectangle(0, 0, bmp.Width, bmp.Height));


            Grph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            Grph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int xPos = 10;

            Font f = cc.GetFont();

            char[] textArray = text.ToCharArray();
            int yPosition = 0;
            Random r = new Random();

            for (int i = 0; i < textArray.Length; i++)
            {
                if (i % 2 == 0)
                    Grph.RotateTransform(5);
                else
                    Grph.RotateTransform(-5);

                yPosition = (int)(r.NextDouble() * 10);

                Grph.DrawString(textArray[i].ToString(), f, new SolidBrush(Color.FromName(cc.TextColor)), xPos, yPosition);
                xPos += 20;

            }
            
            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private int yPos()
        {
            Random r = new Random();
            return (int)(r.NextDouble() * 10);
        }                
    }
}