using PMIClient;
using PMIClient.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Shahab.CensusRreport.Library
{
    public class Validation
    {
        /// <summary>
        /// بررسی معتبر بودن تاریخ شمسی
        /// </summary>
        /// <param name="shamsiDate">تاریخ شمسی</param>
        /// <param name="lessThanToday">آیا تاریخ وارد شده از امروز کمتر باشد</param>
        /// <returns></returns>
        public static bool ValidDate(string shamsiDate, bool lessThanToday)
        {
            bool valid = true;
            if (!string.IsNullOrEmpty(shamsiDate))
            {
                var pattern = "(?<year>((12|13)[0-9]{2}))/(?<month>(0?[1-9]|1[012]))/(?<day>(3[01]|[12][0-9]|0?[1-9]))";
                Match matchResult = Regex.Match(shamsiDate, pattern);
                if (matchResult.Success)
                {
                    var yy = Convert.ToInt32(matchResult.Groups["year"].Value);
                    var mm = Convert.ToInt32(matchResult.Groups["month"].Value);
                    var dd = Convert.ToInt32(matchResult.Groups["day"].Value);

                    if (mm > 6 && mm < 12 && dd > 30)
                    {
                        valid = false;
                    }
                    else if (mm == 12 && (((yy + 1) % 4 != 0 && dd == 30) || dd == 31))
                    {
                        valid = false;
                    }

                    if (valid)
                    {
                        System.Globalization.PersianCalendar pcCalendar = new System.Globalization.PersianCalendar();
                        DateTime birthDate = pcCalendar.ToDateTime(yy, mm, dd, 0, 0, 0, 0);
                        if (DateTime.Compare(DateTime.Now.Date, birthDate.Date) < 0)
                        {
                            valid = false;
                        }
                    }
                }
                else
                {
                    valid = false;
                }
            }
            return valid;
        }
        /// <summary>
        /// بررسی عددی بودن مقدار و طول آن
        /// </summary>
        /// <param name="vlaue">مقدار ورودی</param>
        /// <param name="length">طول</param>
        /// <returns></returns>
        public static Tuple<bool,bool> IsNemericValue(string vlaue, int length)
        {
            Regex isNumber = new Regex(@"^\d*$");
            Match m = isNumber.Match(vlaue);
            return new Tuple<bool, bool>(m.Success, length > 0 && vlaue.Length == length);
        }
        /// <summary>
        /// بررسی عددی بودن ورودی و محدوده طول آن
        /// </summary>
        /// <param name="vlaue">مقدار ورودی</param>
        /// <param name="minLength">کمترین مقدار طول</param>
        /// <param name="maxLength">بیشترین مقدار طول</param>
        /// <returns></returns>

        public static Tuple<bool, bool> IsNemericValue(string vlaue, int minLength, int maxLength)
        {
            Regex isNumber = new Regex(@"^\d*$");
            Match m = isNumber.Match(vlaue);
            return new Tuple<bool, bool>(m.Success, vlaue.Length >= minLength && vlaue.Length <= maxLength);
        }
        /// <summary>
        /// بررسی اعتبار شماره موبایل
        /// </summary>
        /// <param name="mobile">شماره موبایل</param>
        /// <returns></returns>
        public static bool ValidMobile(string mobile)
        {
            Regex isMobile = new Regex(@"09(0[12]|[13][0-9]|2[01])[0-9]{7}");
            Match m = isMobile.Match(mobile);
            return m.Success;
        }
        /// <summary>
        /// بررسی اعتبار پست الکترونیکی
        /// </summary>
        /// <param name="email">پست الکترونیکی</param>
        /// <returns></returns>
        public static bool ValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        /// <summary>
        /// بررسی اعتبار کد ملی از طریق سرویس
        /// </summary>
        /// <param name="nationalCode">کد ملی</param>
        /// <returns></returns>
        public static PersonInfoVO CheckValidNationalCode(string nationalCode)
        {
            try
            {
                PMIService pmiService = new PMIService();
                PersonInfoVO personelInfoVO = pmiService.GetPersonByNationalCode(nationalCode);
                return personelInfoVO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// بررسی وجود کاراکترهای فارسی و عربی در رشته ورودی
        /// </summary>
        /// <param name="text">رشته ورودی</param>
        /// <returns></returns>
        public static bool HasArabicAndPersianCharacters(string text)
        {
            Regex regex = new Regex(
                "[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]|\uFB8A|\u067E|\u0686|\u06AF");
            return regex.IsMatch(text);
        } 
    }
}