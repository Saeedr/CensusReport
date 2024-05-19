using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace Shahab.CensusRreport.Library
{
    public class Configurations
    {
        #region Column order Member Family
        public static int IndexSelectedColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexSelectedColumn"]); }
        }

        public static int IndexIdColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexIdColumn"]); }
        }

        public static int IndexFirstNameColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFirstNameColumn"]); }
        }

        public static int IndexLastNameColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexLastNameColumn"]); }
        }

        public static int IndexNationalCodeColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexNationalCodeColumn"]); }
        }

        public static int IndexActivityStatusColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexActivityStatusColumn"]); }
        }

        public static int IndexRelationShipColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexRelationShipColumn"]); }
        }

        #endregion

        #region Column order Family

        public static int IndexFamilySelectedColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilySelectedColumn"]); }
        }

        public static int IndexFamilyIdColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyIdColumn"]); }
        }

        public static int IndexFamilyRowColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyRowColumn"]); }
        }

        public static int IndexFamilyNumberColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyNumberColumn"]); }
        }

        public static int IndexFamilyFullNameColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyFullNameColumn"]); }
        }
        public static int IndexFamilyProvinceColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyProvinceColumn"]); }
        }
        public static int IndexFamilyCityColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyCityColumn"]); }
        }
        public static int IndexFamilyPostalCodeColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyPostalCodeColumn"]); }
        }
        public static int IndexFamilyNationalCodeColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyNationalCodeColumn"]); }
        }
        public static int IndexFamilyTypeColumn
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["IndexFamilyTypeColumn"]); }
        }
        #endregion

        #region Condition Value
        public static string NoIranianValue 
        { 
            get { return WebConfigurationManager.AppSettings["NoIranianValue"]; }
        }
        public static string InstituteFamilyTypeValue
        {
            get { return WebConfigurationManager.AppSettings["InstituteFamilyTypeValue"]; }
        }

        public static string NoInsuranceValue
        {
            get { return WebConfigurationManager.AppSettings["NoInsuranceValue"]; }
        }

        public static int MinAgeConditionValue
        {
            get { return Convert.ToInt32(WebConfigurationManager.AppSettings["MinAgeConditionValue"]); }
        }

        public static string SupervisorValue
        {
            get { return WebConfigurationManager.AppSettings["SupervisorValue"]; }
        }

        public static string SpouseRelationValue
        {
            get { return WebConfigurationManager.AppSettings["SpouseRelationValue"]; }
        }

        public static string HaveSpouseValue
        {
            get { return WebConfigurationManager.AppSettings["HaveSpouseValue"]; }
        }

        public static string WorkingValue
        {
            get { return WebConfigurationManager.AppSettings["WorkingValue"]; }
        }

        
        #endregion

        public static string SMSServiceUrl
        {
            get { return WebConfigurationManager.AppSettings["SMSServiceUrl"]; }
        }

        public static void RegisterScript(Page page,Type type,string key, string url ){
            ScriptManager currentScriptManager = ScriptManager.GetCurrent(page);
            if (currentScriptManager != null)
            {
                ScriptReference scriptRef = new ScriptReference(url);
                currentScriptManager.Scripts.Add(scriptRef);
            }
            else
            {
                page.ClientScript.RegisterClientScriptInclude(
                    type, key, url);
            }
        }
    }
}