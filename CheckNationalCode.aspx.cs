using PMIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusReport
{
    public partial class CheckNationalCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                PMIService pmiService = new PMIService();
                PMIClient.DataModel.PersonInfoVO personelInfoVO = pmiService.GetPersonByNationalCode(txtNationalCode.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}