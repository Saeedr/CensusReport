using Shahab.CensusRreport.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusRreport.Components
{
    public partial class FamilyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetControls();
            BindGrid();

        }

        private void SetControls()
        {
            #region ColumnHeader
            gvFamilyList.Columns[Configurations.IndexFamilyRowColumn].HeaderText = "ردیف";
            gvFamilyList.Columns[Configurations.IndexFamilyNumberColumn].HeaderText = "شماره خانوار";
            gvFamilyList.Columns[Configurations.IndexFamilyFullNameColumn].HeaderText = "نام";
            gvFamilyList.Columns[Configurations.IndexFamilyProvinceColumn].HeaderText = "استان";
            gvFamilyList.Columns[Configurations.IndexFamilyCityColumn].HeaderText = "شهر";
            gvFamilyList.Columns[Configurations.IndexFamilyPostalCodeColumn].HeaderText = "کد پستی";
            gvFamilyList.Columns[Configurations.IndexFamilyNationalCodeColumn].HeaderText = "شماره ملی";
            gvFamilyList.Columns[Configurations.IndexFamilyTypeColumn].HeaderText = "نوع خانوار";
            #endregion
        }

        private void BindGrid()
        {
            List<Family> families = Family.GetFamilySupervisor();
            gvFamilyList.DataSource = families;
            gvFamilyList.DataBind();
        }

        protected void gvFamilyList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
            }
        }
    }
}