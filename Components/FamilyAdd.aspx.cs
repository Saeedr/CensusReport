using Shahab.CensusRreport.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shahab.CensusRreport.Components
{
    public partial class FamilyAdd : System.Web.UI.Page
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

        private string itemFamilyListKey = "ItemFamilyListKey";
        private string checkPageRefresh = "CheckPageRefresh";

        private List<FamilyMembers> _members = null;
        protected List<FamilyMembers> Members
        {
            get
            {
                if (Session[itemFamilyListKey] == null)
                {
                    _members = new List<FamilyMembers>();
                    Session[itemFamilyListKey] = _members;
                }
                return (List<FamilyMembers>)Session[itemFamilyListKey];
            }

            set
            {
                _members = value;
            }
        }

        #region Initialize
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Session[checkPageRefresh] = DateTime.Now.ToString();
                Session[itemFamilyListKey] = null;
                SetControls();

                if (UserId != -1)
                {
                    LoadFamily();
                }
                else
                {
                    Response.Redirect("/Components/FamilyLogin.aspx");
                }
            }
            else
            {
                string province = Request.Form[ddlProvince.UniqueID];
                string townShip = Request.Form[ddlTownShip.UniqueID];
                string city = Request.Form[ddlCity.UniqueID];

                // Repopulate TownShip and Cities

                if (!string.IsNullOrEmpty(province))
                    BindTownShip(Convert.ToInt32(province));
                if (!string.IsNullOrEmpty(townShip))
                {
                    BindCity(Convert.ToInt32(townShip));
                    ddlTownShip.SelectedValue = townShip;
                }
                if (!string.IsNullOrEmpty(city))
                    ddlCity.SelectedValue = city;

                if (Request.Form["hid_f"] == "1")
                {
                    SaveFamily();
                    Request.Form["hid_f"].Replace("1", "0");
                }	
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState[checkPageRefresh] = Session[checkPageRefresh];
        }
        private bool CheckPageIsPostBack()
        {
            if (ViewState[checkPageRefresh].ToString() == Session[checkPageRefresh].ToString())
            {
                Session[checkPageRefresh] = DateTime.Now.ToString();
                return true;
            }
            else
                return false;
        }
        public void SetControls()
        {

            #region Family Controls

            lblFamilyId.Text = @"-1";

            btnSave.Text = "ثبت نهایی اطلاعات";
            btnNewMember.Text = "ورود اطلاعات اعضای خانوار";
            btnDelete.Text = "حذف عضو انتخاب شده";

            btnDelete.OnClientClick = "return confirm('عضو انتخاب شده حذف گردد؟');";
            btnDelete.Visible = Members.Count > 0;

            pnlMessage.Attributes.Add("style", "display:none");
            pnlMember.Attributes.Add("style", "display:none");

            #region Bind DropDownList
            
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.FamilyType), ddlFamilyType, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.PopulationType), ddlPopulationType, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.RegionStatus), ddlRegionStatus, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.OwnerShipStatus), ddlOwnerShipStatus, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("Places", "PlaceId", "Name", "ParentId IS NULL", ddlProvince, "", "Name", 0);

            #endregion

            #endregion

            #region ColumnHeader
            gvMembers.Columns[Configurations.IndexIdColumn].HeaderText = "ردیف";
            gvMembers.Columns[Configurations.IndexFirstNameColumn].HeaderText = "نام";
            gvMembers.Columns[Configurations.IndexLastNameColumn].HeaderText = "نام خانوادگی";
            gvMembers.Columns[Configurations.IndexNationalCodeColumn].HeaderText = "شماره ملی";
            gvMembers.Columns[Configurations.IndexActivityStatusColumn].HeaderText = "وضعیت فعالیت";
            gvMembers.Columns[Configurations.IndexRelationShipColumn].HeaderText = "نسبت";
            #endregion

            #region Family Member Control

            btnSaveMember.Text = "تایید اطلاعات عضو";

            #region Bind DropDownList

            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.Nationality), ddlNationality, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.Gender), ddlGender, "", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.RelationShip), ddlRelationShip, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.InhabitancyStatus), ddlInhabitancyStatus, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.MaritalStatus), ddlMaritalStatus, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.EducationStatus), ddlEducationStatus, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.ActivityStatus), ddlActivityStatus, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.JobType), ddlJobType, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.InsuranceFirst), ddlInsuranceFirst, "انتخاب کنید", "", 0);
            PublicSqlMethods.FillDDL("BaseInfo", "BaseInfoId", "Value", String.Format("ParentId = {0}", (int)Enumurations.BaseInfoField.InsuranceSecond), ddlInsuranceSecond, "انتخاب کنید", "", 0);

            #endregion


            #endregion

        }
        /// <summary>
        /// پر کردن لیست کشویی شهرستان بر اساس کد استان 
        /// </summary>
        /// <param name="provinceId">کد استان</param>
        private void BindTownShip(int provinceId)
        {
            ddlTownShip.Items.Clear();
            PublicSqlMethods.FillDDL("Places", "PlaceId", "Name", String.Format("ParentId = {0}", provinceId), ddlTownShip, "", "Name", 0);
        }
        /// <summary>
        /// پر کردن لیست کشویی شهر بر اساس کد شهرستان
        /// </summary>
        /// <param name="townShipId">کد شهرستان</param>
        private void BindCity(int townShipId)
        {
            ddlCity.Items.Clear();
            PublicSqlMethods.FillDDL("Places", "PlaceId", "Name", String.Format("ParentId = {0}", townShipId), ddlCity, "", "Name", 0);
        }
        /// <summary>
        /// پاک کردن مقادیر کنترل های اعضا
        /// </summary>
        private void ClearFormMember()
        {
            hdnIndex.Value = @"-1";
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            ddlNationality.SelectedIndex = 0;
            txtNationalCode.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            ddlRelationShip.SelectedIndex = 0;
            txtBirthDate.Text = string.Empty;
            hdnAge.Value = string.Empty;
            ddlInhabitancyStatus.SelectedIndex = 0;
            ddlMaritalStatus.SelectedIndex = 0;
            ddlEducationStatus.SelectedIndex = 0;
            ddlActivityStatus.SelectedIndex = 0;
            ddlJobType.SelectedIndex = 0;
            ddlInsuranceFirst.SelectedIndex = 0;
            ddlInsuranceSecond.SelectedIndex = 0;
            txtNationalCode.Enabled = true;
            ddlEducationStatus.Enabled = true;
            ddlActivityStatus.Enabled = true;
            ddlJobType.Enabled = true;
            ddlInsuranceSecond.Enabled = true;
            ddlMaritalStatus.Enabled = true;
        }
        /// <summary>
        /// پاک کردن مقادیر کنترل های خانوار
        /// </summary>
        private void ClearFormFamily()
        {
            lblFamilyId.Text = @"-1";
            txtBlockNumber.Text = string.Empty;
            txtFamilyNumber.Text = string.Empty;
            txtBuildingNumber.Text = string.Empty;
            ddlFamilyType.SelectedIndex = 0;
            ddlPopulationType.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlRegionStatus.SelectedIndex = 0;
            ddlOwnerShipStatus.SelectedIndex = 0;
            ddlProvince.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            txtPostalCode.Text = string.Empty;
            txtMobileNumber.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        #endregion

        #region Grid
        /// <summary>
        /// بارگزاری اطلاعات اعضای خانوار
        /// </summary>
        /// <param name="member"></param>
        private void LoadFamilyMember(FamilyMembers member)
        {
            txtFirstName.Text = member.FirstName;
            txtLastName.Text = member.LastName;
            ddlNationality.SelectedValue = member.Nationality.ToString();

            if (ddlNationality.SelectedValue == Configurations.NoIranianValue)
            {
                txtNationalCode.Text = "";
                txtNationalCode.Enabled = false;
            }
            else
            {
                txtNationalCode.Text = member.NationalCode;
                txtNationalCode.Enabled = true;
            }

            ddlGender.SelectedValue = member.Gender.ToString();

            if (member.RelationShip != -1)
            {
                ddlRelationShip.SelectedValue = member.RelationShip.ToString();
                ddlRelationShip.Enabled = true;
            }
            else
            {
                ddlRelationShip.SelectedIndex = -1;
                ddlRelationShip.Enabled = false;
            }

            txtBirthDate.Text = PublicSqlMethods.MiladiToShamsi(member.BirthDate);
            ddlInhabitancyStatus.SelectedValue = member.InhabitancyStatus.ToString();
            ddlMaritalStatus.SelectedValue = member.MaritalStatus.ToString();
            ddlRelationShip.Enabled = (member.RelationShip == Convert.ToInt32(Configurations.SpouseRelationValue)) ? false : true;

            if (member.EducationStatus != -1)
            {
                ddlEducationStatus.SelectedValue = member.EducationStatus.ToString();
                ddlEducationStatus.Enabled = true;
            }
            else
            {
                ddlEducationStatus.SelectedIndex = -1;
                ddlEducationStatus.Enabled = false; 
            }

            if (member.ActivityStatus != -1)
            {
                ddlActivityStatus.SelectedValue = member.ActivityStatus.ToString();
                ddlActivityStatus.Enabled = true;
            }
            else
            {
                ddlActivityStatus.SelectedIndex = -1;
                ddlActivityStatus.Enabled = false; 
            }

            if (member.JobType != -1)
            {
                ddlJobType.SelectedValue = member.JobType.ToString();
                ddlJobType.Enabled = true;
            }
            else
            {
                ddlJobType.SelectedIndex = -1;
                ddlJobType.Enabled = false;
            }

            ddlInsuranceFirst.SelectedValue = member.InsuranceFirst.ToString();
            ddlInsuranceSecond.SelectedValue = member.InsuranceSecond.ToString();

            ddlInsuranceSecond.Enabled = member.InsuranceFirst == Convert.ToInt32(Configurations.NoInsuranceValue) ? false : true;

            var age = Convert.ToInt32(CalculateAgeFromBirthDate(txtBirthDate.Text));
            hdnAge.Value = age.ToString();
            if (age < 1)
                lblAge.Text = "زیر یک سال";
            else
                lblAge.Text = String.Format("{0} سال", age);

            if (age <= Convert.ToInt32(Configurations.MinAgeConditionValue))
                ddlMaritalStatus.Enabled = false;
        }

        protected void gvMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                var members = e.Row.DataItem as FamilyMembers;

                if(members.Status == Enumurations.Status.Disabled)
                {
                    e.Row.Cells[Configurations.IndexNationalCodeColumn].ForeColor = System.Drawing.Color.Red;
                }
                var lblRelationShip = (Label)e.Row.FindControl("lblRelationShip");
                if (lblRelationShip != null)
                { 
                    lblRelationShip.Text = members.RelationShip == -1 ? "---" : BaseInfo.GetBaseInfo(members.RelationShip, false).Value; 
                }

                var lblActivityStatus = (Label)e.Row.FindControl("lblActivityStatus");
                if (lblActivityStatus != null)
                    lblActivityStatus.Text = BaseInfo.GetBaseInfo(members.ActivityStatus, false).Value;
            }
        }

        protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditMember")
            {
                FamilyMembers member = Members[Convert.ToInt32(e.CommandArgument)];
                LoadFamilyMember(member);
                hdnIndex.Value = e.CommandArgument.ToString();
                pnlMember.Attributes.Add("style", "display:block");

                if (ddlFamilyType.SelectedValue == Configurations.InstituteFamilyTypeValue)
                {
                    ddlRelationShip.SelectedIndex = -1;
                    ddlRelationShip.Enabled = false; 
                }
                else
                    ddlRelationShip.Enabled = true;

                pnlMessage.Attributes.Add("style", "display:none");
            }
        }

        private void BindGrid()
        {
            gvMembers.DataSource = Members;
            gvMembers.DataBind();
            btnDelete.Visible = gvMembers.Visible = Members.Count > 0;
        }
        #endregion

        #region Fetch Object
        /// <summary>
        /// واکشی اطلاعات خانوار از روی کنترل ها
        /// </summary>
        /// <returns></returns>
        private Family FetchObject()
        {
            Family family = new Family();
            family.FamilyId = Convert.ToInt32(lblFamilyId.Text);
            family.UserId = UserId;
            family.BlockNumber = txtBlockNumber.Text;
            family.FamilyNumber = txtFamilyNumber.Text;
            family.BuildingNumber = txtBuildingNumber.Text;
            family.FamilyType = Convert.ToInt32(ddlFamilyType.SelectedValue);
            family.PopulationType = Convert.ToInt32(ddlPopulationType.SelectedValue);
            family.PlaceId = Convert.ToInt32(ddlCity.SelectedValue);
            family.RegionStatus = Convert.ToInt32(ddlRegionStatus.SelectedValue);
            family.OwnerShipStatus = Convert.ToInt32(ddlOwnerShipStatus.SelectedValue);
            family.PostalCode = txtPostalCode.Text;
            family.MobileNumber = txtMobileNumber.Text;
            family.PhoneNumber = txtPhoneNumber.Text;
            family.Address = txtAddress.Text;
            family.Status = Enumurations.Status.Enabled;
            family.Priority = 0;

            return family;
        }
        /// <summary>
        /// واکشی اطلاعات عضو خانوار از روی کنترل های عضو
        /// </summary>
        /// <returns></returns>
        private FamilyMembers FetchObjectMember()
        {
            FamilyMembers member = new FamilyMembers();

            if (Convert.ToInt32(hdnIndex.Value) != -1)
            {
                member.FamilyMemberId = Members[Convert.ToInt32(hdnIndex.Value)].FamilyMemberId;
            }
            member.FirstName = txtFirstName.Text;
            member.LastName = txtLastName.Text;
            member.Nationality = Convert.ToInt32(ddlNationality.SelectedValue); ;
            member.NationalCode = (ddlNationality.SelectedValue == Configurations.NoIranianValue) ? "" : txtNationalCode.Text;
            member.Gender = Convert.ToInt32(ddlGender.SelectedValue);
            member.RelationShip = ddlFamilyType.SelectedValue == Configurations.InstituteFamilyTypeValue ? -1 : Convert.ToInt32(ddlRelationShip.SelectedValue);
            if (txtBirthDate.Text != string.Empty)
            { 
                member.BirthDate = PublicSqlMethods.ShamsiToMiladi(txtBirthDate.Text); 
            }
            member.InhabitancyStatus = Convert.ToInt32(ddlInhabitancyStatus.SelectedValue);

            if (member.RelationShip == Convert.ToInt32(Configurations.SpouseRelationValue))
            {
                member.MaritalStatus = Convert.ToInt32(Configurations.HaveSpouseValue);
            }
            else
                member.MaritalStatus = Convert.ToInt32(ddlMaritalStatus.SelectedValue);

            if (Convert.ToInt32(hdnAge.Value) <= Configurations.MinAgeConditionValue)
            {
                member.EducationStatus = -1;
                member.ActivityStatus = -1;
                member.JobType = -1;
                member.MaritalStatus = 0;
            }
            else
            {
                member.EducationStatus = Convert.ToInt32(ddlEducationStatus.SelectedValue);
                member.ActivityStatus = Convert.ToInt32(ddlActivityStatus.SelectedValue);
                if (member.ActivityStatus != Convert.ToInt32(Configurations.WorkingValue))
                    member.JobType = -1;
                else
                    member.JobType = Convert.ToInt32(ddlJobType.SelectedValue);
            }
            member.InsuranceFirst = Convert.ToInt32(ddlInsuranceFirst.SelectedValue);
            member.InsuranceSecond = ddlInsuranceFirst.SelectedValue == Configurations.NoInsuranceValue ? Convert.ToInt32(Configurations.NoInsuranceValue) : Convert.ToInt32(ddlInsuranceSecond.SelectedValue);
            member.Status = Enumurations.Status.Enabled;
            member.FamilyId = Convert.ToInt32(lblFamilyId.Text);
            member.Priority = 0;

            return member;
        }

        #endregion
        
        #region ButtonClick

        private void SaveFamily()
        {
            bool uniqueNationalCode = true;
            bool validRelation = true;
            bool success = true;
            try
            {
                var family = FetchObject();
                family.Members = new List<FamilyMembers>();
                foreach (var member in Members)
                {
                    if (Request.Form["hid_f"] == "1")
                    {
                        member.RelationShip = -1;
                    }
                    if (member.RelationShip == -1 && ddlFamilyType.SelectedValue != Configurations.InstituteFamilyTypeValue)
                    {
                        validRelation = false;
                    }
                    if (member.NationalCode != string.Empty)
                    {
                        var expectRelation = (member.RelationShip == Convert.ToInt32(Configurations.SupervisorValue)) ? member.RelationShip : -1;
                        if (member.CheckUniqueNationalCode(member.NationalCode, family.FamilyId, expectRelation))
                        {
                            member.Status = Enumurations.Status.Enabled;
                            family.Members.Add(member);
                        }
                        else
                        {
                            uniqueNationalCode = false;
                            member.Status = Enumurations.Status.Disabled;
                        }
                    }
                    else
                    {
                        family.Members.Add(member);
                    }
                }
                if (uniqueNationalCode && validRelation)
                {
                    success = family.Save();
                    Shahab.CensusRreport.Library.User.UpdateFlag(family.UserId);
                    if (success)
                    {
                        liMessage.Text = "<li>اطلاعات با موفقیت ذخیره شد. برای ویرایش خانوار <a href='/Components/FamilyAdd.aspx'>اینجا</a> را کلیک کنید.</li>";
                        pnlMessage.CssClass = "success";
                        pnlMainForm.Visible = false;
                    }
                    else
                    {
                        liMessage.Text = "<li>امکان ذخیره اطلاعات وجود ندارد</li>";
                        pnlMessage.CssClass = "fail";
                    }

                }
                else if (!uniqueNationalCode || !validRelation)
                {
                    if (!validRelation)
                        liMessage.Text += "<li>اعضایی که فیلد نسبت با سرپرست برای آنها انتخاب نشده است را اصلاح کنید.</li>";
                    if (!uniqueNationalCode)
                        liMessage.Text += "<li>شماره ملی های مشخص شده تکراری می باشد.</li>";

                    pnlMessage.CssClass = "errorWrapper";
                    family.Members.Clear();
                }
            }
            catch(Exception ex)
            {
                //throw ex;
                liMessage.Text = "<li>امکان ذخیره اطلاعات وجود ندارد</li>";
                pnlMessage.CssClass = "fail";
            }
            if (uniqueNationalCode && validRelation && success)
            {
                ClearFormFamily();
                ClearFormMember();
                Session[itemFamilyListKey] = null;
                Session[checkPageRefresh] = DateTime.Now.ToString();
                ViewState[checkPageRefresh] = null;
            }
            BindGrid();
        }
        private void SaveMember()
        {
            if (CheckValidMember() && CheckRequiredFieldFamily())
            {
                try
                {
                    var member = FetchObjectMember();

                    if (IsValidNationalCode(member.NationalCode))
                    {
                        if (Convert.ToInt32(hdnIndex.Value) == -1)
                        {
                            Members.Add(member);
                        }
                        else if (Convert.ToInt32(hdnIndex.Value) != -1)
                            Members[Convert.ToInt32(hdnIndex.Value)] = member;
                    }
                    else
                    {
                        lblInValidNationalCode.Text = "شماره ملی نامعتبر است";
                    }
                }
                catch
                {
                }
                pnlMember.Attributes.Add("style", "display:none");
                pnlMessage.Attributes.Add("style", "display:none");
                BindGrid();
                ClearFormMember();
            }
            else
            {
                pnlMessage.CssClass = "errorWrapper";
                pnlMessage.Attributes.Add("style", "display:block");
                pnlMember.Attributes.Add("style", "display:block");

                if (ddlFamilyType.SelectedValue == Configurations.InstituteFamilyTypeValue)
                    ddlRelationShip.Enabled = false;
                else
                    ddlRelationShip.Enabled = true;

                if (ddlNationality.SelectedValue == Configurations.NoIranianValue)
                    txtNationalCode.Enabled = false;
                else
                    txtNationalCode.Enabled = true;

                if (ddlInsuranceFirst.SelectedValue == Configurations.NoInsuranceValue)
                {
                    ddlInsuranceSecond.Enabled = false;
                }

                if(chkNoPostalCode.Checked)
                {
                    txtPostalCode.Enabled = false;
                    txtPostalCode.Text = string.Empty;
                }

                ddlJobType.Enabled = (ddlActivityStatus.SelectedValue == Configurations.WorkingValue);

                if (hdnAge.Value != "")
                {
                    if (Convert.ToInt32(hdnAge.Value) < 1)
                    {
                        lblAge.Text = "زیر یک سال";
                    }
                    else
                        lblAge.Text = String.Format("{0} سال", hdnAge.Value);

                    if (Convert.ToInt32(hdnAge.Value) <= Convert.ToInt32(Configurations.MinAgeConditionValue))
                    {
                        ddlMaritalStatus.Enabled = false;
                        ddlEducationStatus.Enabled = false;
                        ddlActivityStatus.Enabled = false;
                        ddlJobType.Enabled = false;
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckPageIsPostBack())
            {
                liMessage.Text = "";
                if (!String.IsNullOrEmpty(txtFirstName.Text) || !String.IsNullOrEmpty(txtLastName.Text) || !String.IsNullOrEmpty(txtNationalCode.Text) || !String.IsNullOrEmpty(txtBirthDate.Text))
                {
                    SaveMember();
                }
                if (CheckValidFamily())
                {
                    if (CheckInstituteFamilyType() && !CheckNotRelationInInstituteFamilyType())
                    {
                        MsgBox1.confirm("شما نوع خانوار موسسه ای را انتخاب کرده اید، آیا می خواهید ذخیره اطلاعات انجام شود؟", "hid_f");
                    }
                    else
                    {
                        SaveFamily();
                    }
                    
                }
                else
                {
                    pnlMessage.CssClass = "errorWrapper";
                }

                pnlMessage.Attributes.Add("style", "display:block");
            }
        }

        protected void btnSaveMember_Click(object sender, EventArgs e)
        {
            if (CheckPageIsPostBack())
            {
                SaveMember();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckPageIsPostBack())
            {
                foreach (GridViewRow row in gvMembers.Rows)
                {
                    CheckBox chkSelect = (row.Cells[Configurations.IndexSelectedColumn].FindControl("chkSelect") as CheckBox);
                    if (chkSelect.Checked)
                    {
                        Members.RemoveAt(row.RowIndex);
                        Members.Insert(row.RowIndex, null);
                    }
                }

            }
            Members.RemoveAll(m => m == null);
            BindGrid();
            pnlMessage.Attributes.Add("style", "display:none");
        }

        protected void btnNewMember_Click(object sender, EventArgs e)
        {
            ClearFormMember();

            pnlMember.Attributes.Add("style", "display:block");

            if (ddlFamilyType.SelectedValue == Configurations.InstituteFamilyTypeValue)
                ddlRelationShip.Enabled = false;
            else
                ddlRelationShip.Enabled = true;
        }
        #endregion

        #region Check validation
        /// <summary>
        /// چک کردن ضروری بودن فیلدهای خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckRequiredFieldFamily()
        {
            StringBuilder message = new StringBuilder();

            bool valid = true;
            //if(txtBlockNumber.Text == string.Empty)
            //{
            //    valid = false;
            //    message.Append("<li>شماره بلوک را وارد کنید</li>");
            //}
            //if(txtFamilyNumber.Text == string.Empty)
            //{
            //    valid = false;
            //    message.Append("<li>شماره خانوار را وارد کنید</li>");
            //}
            if(txtBuildingNumber.Text == string.Empty)
            {
                valid = false;
                message.Append("<li>شماره ساختمان را وارد کنید</li>");
            }
            if (!chkNoPostalCode.Checked)
            {
                if (txtPostalCode.Text == string.Empty)
                {
                    valid = false;
                    message.Append("<li>کد پستی را وارد کنید</li>");
                }
            }
            else
            {
                txtPostalCode.Enabled = false;
                txtPostalCode.Text = string.Empty;
            }
            if (txtMobileNumber.Text == string.Empty)
            {
                valid = false;
                message.Append("<li>تلفن همراه سرپرست را وارد کنید</li>");
            }
            if (txtPhoneNumber.Text == string.Empty)
            {
                valid = false;
                message.Append("<li>تلفن ثابت خانوار را وارد کنید</li>");
            }
            if (txtAddress.Text == String.Empty)
            {
                valid = false;
                message.Append("<li>نشانی خانوار را وارد کنید</li>");
            }

            if (ddlFamilyType.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>نوع خانوار را انتخاب کنید</li>");
            }

            //if (ddlPopulationType.SelectedValue == "-1")
            //{
            //    valid = false;
            //    message.Append("<li>نوع جمعیت را انتخاب کنید</li>");
            //}

            //if (ddlRegionStatus.SelectedValue == "-1")
            //{
            //    valid = false;
            //    message.Append("<li>وضعیت منطقه شهری را انتخاب کنید</li>");
            //}

            if (ddlOwnerShipStatus.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>وضعیت مالکیت را انتخاب کنید</li>");
            }
            if (ddlProvince.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>استان را انتخاب کنید</li>");
            }
            if (ddlTownShip.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>شهرستان را انتخاب کنید</li>");
            }
            if (ddlCity.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>شهر را انتخاب کنید</li>");
            }


            liMessage.Text += message.ToString();
            return valid;
        }
        /// <summary>
        /// چک کردن ضروری بودن فیلدهای عضو خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckRequiredFieldFamilyMembers()
        {
            StringBuilder message = new StringBuilder();

            bool valid = true;
            if (txtFirstName.Text == string.Empty)
            {
                valid = false;
                message.Append("<li>نام را وارد کنید</li>");
            }
            if (txtLastName.Text == string.Empty)
            {
                valid = false;
                message.Append("<li>نام خانوادگی را وارد کنید</li>");
            }
            if (txtNationalCode.Text == string.Empty && ddlNationality.SelectedValue != Configurations.NoIranianValue)
            {
                valid = false;
                message.Append("<li>شماره ملی را وارد کنید</li>");
            }
            if (txtBirthDate.Text == string.Empty)
            {
                valid = false;
                message.Append("<li>تاریخ تولد را وارد کنید</li>");
            }

            if (ddlNationality.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>ملیت را انتخاب کنید</li>");
            }
            if (ddlGender.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>جنسیت را انتخاب کنید</li>");
            }
            if (ddlRelationShip.SelectedValue == "-1" && ddlFamilyType.SelectedValue != Configurations.InstituteFamilyTypeValue)
            {
                valid = false;
                message.Append("<li>نسبت را انتخاب کنید</li>");
            }
            if (ddlInhabitancyStatus.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>وضعیت اقامت را انتخاب کنید</li>");
            }
            if (ddlMaritalStatus.SelectedValue == "-1")
            {
                if (Convert.ToInt32(hdnAge.Value) > Configurations.MinAgeConditionValue)
                {
                    valid = false;
                    message.Append("<li>وضعیت تاهل را انتخاب کنید</li>");
                }
            }
            if (ddlEducationStatus.SelectedValue == "-1" && Convert.ToInt32(hdnAge.Value) > Configurations.MinAgeConditionValue)
            {
                valid = false;
                message.Append("<li>وضعیت سواد را انتخاب کنید</li>");
            }
            if (ddlActivityStatus.SelectedValue == "-1" && Convert.ToInt32(hdnAge.Value) > Configurations.MinAgeConditionValue)
            {
                valid = false;
                message.Append("<li>وضعیت فعالیت را انتخاب کنید</li>");
            }
            if (ddlJobType.SelectedValue == "-1" && Convert.ToInt32(hdnAge.Value) > Configurations.MinAgeConditionValue && ddlActivityStatus.SelectedValue == Configurations.WorkingValue)
            {
                valid = false;
                message.Append("<li>نوع شغل را انتخاب کنید</li>");
            }
            if (ddlInsuranceFirst.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>بیمه پایه اول را انتخاب کنید</li>");
            }
            else if(ddlInsuranceFirst.SelectedValue == Configurations.NoInsuranceValue)
                ddlInsuranceSecond.SelectedValue = Configurations.NoInsuranceValue;

            if (ddlInsuranceSecond.SelectedValue == "-1")
            {
                valid = false;
                message.Append("<li>بیمه پایه دوم را انتخاب کنید</li>");
            }

            liMessage.Text = message.ToString();

            return valid;
        }
        /// <summary>
        /// بررسی تعداد سرپرست برای اعضای خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckMoreThanOneSupervisor()
        {
            var supervisorId = Convert.ToInt32(Configurations.SupervisorValue);
            var rowIndex = Convert.ToInt32(hdnIndex.Value);
            List<FamilyMembers> familyMembers = Members.Where((x, index) => index != rowIndex && x.RelationShip == supervisorId).ToList();

            if ((ddlRelationShip.SelectedValue == supervisorId.ToString()) && (familyMembers.Count > 0))
                return true;
            else
                return false;
        }
        /// <summary>
        /// بررسی نوع خانوار موسسه ای است یا نه؟
        /// </summary>
        /// <returns></returns>
        private bool CheckInstituteFamilyType()
        {
            if (ddlFamilyType.SelectedValue == Configurations.InstituteFamilyTypeValue)
                return true;
            return false;
        }
        /// <summary>
        /// بررسی وجود فقط یک سرپرست در خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckOneSupervisor()
        {
            var rowIndex = Convert.ToInt32(hdnIndex.Value);
            List<FamilyMembers> familyMembers = Members.Where((x, index) => index != rowIndex && x.RelationShip == Convert.ToInt32(Configurations.SupervisorValue)).ToList();

            if (familyMembers.Count == 1)
                return true;
            return false;
        }
        /// <summary>
        /// بررسی تکراری نبودن کد ملی در سطح خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckOneNationalCodeInFamily()
        {
            var rowIndex = Convert.ToInt32(hdnIndex.Value);
            List<FamilyMembers> familyMembers = Members.Where((x, index) => index != rowIndex && x.NationalCode.Trim() == txtNationalCode.Text.Trim()).ToList();

            if (familyMembers.Count == 0)
                return true;
            else
                return false; 
        }
        /// <summary>
        /// بررسی یکسان نبودن بیمه پایه اول و دوم
        /// </summary>
        /// <returns></returns>
        private bool CheckInsurance()
        {
            if (ddlInsuranceFirst.SelectedValue == ddlInsuranceSecond.SelectedValue && ddlInsuranceFirst.SelectedValue != Configurations.NoInsuranceValue && ddlInsuranceFirst.SelectedValue != "-1")
                return false;
            else
                return true;
        }
        /// <summary>
        /// بررسی اعتبار سنجی کنترل های عضو خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckValidMember()
        {
            var valid = true;

            if (!CheckRequiredFieldFamilyMembers())
                valid = false;
            var validNumeric = new Tuple<bool, bool>(true, true);

            if (ddlNationality.SelectedValue != Configurations.NoIranianValue && !String.IsNullOrEmpty(txtNationalCode.Text))
            {
                validNumeric = Validation.IsNemericValue(txtNationalCode.Text,txtNationalCode.MaxLength);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای شماره ملی عدد وارد کنید.</li>";
                    valid = false;
                }
                if (!validNumeric.Item2)
                {
                    liMessage.Text += "<li>برای شماره ملی باید ده رقم وارد کنید.</li>";
                    valid = false;
                }
                if (!CheckOneNationalCodeInFamily())
                {
                    liMessage.Text += "<li>قبلاً این شماره ملی را برای یکی از اعضای خانوار ثبت کرده اید.</li>";
                    valid = false;
                }
            }
            if (CheckMoreThanOneSupervisor() && !CheckInstituteFamilyType())
            {
                liMessage.Text += "<li>قبلاً سرپرست را برای این خانوار ثبت کرده اید.</li>";
                valid = false; 
            }
            if (!Validation.ValidDate(txtBirthDate.Text, true) && txtBirthDate.Text != string.Empty)
            {
                liMessage.Text += "<li>تاریخ تولد نامعتبر می باشد</li>";
                valid = false; 
            }
            if (!CheckInsurance())
            {
                liMessage.Text += "<li>بیمه پایه اول و دوم نمی تواند هر دو یکسان باشد</li>";
                valid = false; 
            }
            return valid;
        }
        /// <summary>
        /// بررسی نداشتن مقدار برای فیلد نسبت با سرپرست برای خانوار موسسه ای
        /// </summary>
        /// <returns></returns>
        private bool CheckNotRelationInInstituteFamilyType()
        {
            List<FamilyMembers> familyMembers = Members.Where(x => x.RelationShip != -1).ToList();
            if (familyMembers.Count == 0)
                return true;
            return false;
        }
        /// <summary>
        /// برررسی اعتبارسنجی کنترل های خانوار
        /// </summary>
        /// <returns></returns>
        private bool CheckValidFamily()
        {
            var validNumeric = new Tuple<bool, bool>(true, true);
            
            bool valid = true;
            //if (!CheckRequiredFieldFamily())
            //    valid = false;
            if (!String.IsNullOrEmpty(txtBlockNumber.Text))
            {
                validNumeric = Validation.IsNemericValue(txtBlockNumber.Text, 0);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای شماره بلوک عدد وارد کنید.</li>";
                    valid = false;
                }
            }
            if (!String.IsNullOrEmpty(txtFamilyNumber.Text))
            {
                validNumeric = Validation.IsNemericValue(txtFamilyNumber.Text, 0);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای شماره خانوار عدد وارد کنید.</li>";
                    valid = false;
                }
            }
            if (!String.IsNullOrEmpty(txtBuildingNumber.Text))
            {
                validNumeric = Validation.IsNemericValue(txtBuildingNumber.Text, 0);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای شماره ساختمان عدد وارد کنید.</li>";
                    valid = false;
                }
            }
            if (!String.IsNullOrEmpty(txtPostalCode.Text))
            {
                validNumeric = Validation.IsNemericValue(txtPostalCode.Text, txtPostalCode.MaxLength);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای کد پستی عدد وارد کنید.</li>";
                    valid = false;
                }
                if (!validNumeric.Item2)
                {
                    liMessage.Text += "<li>برای کد پستی باید ده رقم وارد کنید.</li>";
                    valid = false;
                }
            }
            if (!String.IsNullOrEmpty(txtMobileNumber.Text))
            {
                validNumeric = Validation.IsNemericValue(txtMobileNumber.Text, txtMobileNumber.MaxLength);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای تلفن همراه سرپرست  عدد وارد کنید.</li>";
                    valid = false;
                }
                if (!validNumeric.Item2)
                {
                    liMessage.Text += "<li>برای تلفن همراه سرپرست باید یازده رقم وارد کنید.</li>";
                    valid = false;
                }
                if (validNumeric.Item1 && validNumeric.Item2 && !Validation.ValidMobile(txtMobileNumber.Text))
                {
                    liMessage.Text += "<li>تلفن همراه سرپرست نامعتبر می باشد.</li>";
                    valid = false;
                }
            }
            if (!String.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                validNumeric = Validation.IsNemericValue(txtPhoneNumber.Text, 5, txtPhoneNumber.MaxLength);
                if (!validNumeric.Item1)
                {
                    liMessage.Text += "<li>برای تلفن ثابت خانوار عدد وارد کنید.</li>";
                    valid = false;
                }
                if (!validNumeric.Item2)
                {
                    liMessage.Text += "<li>برای تلفن ثابت خانوار باید حداقل 5 و حداکثر 15 رقم وارد کنید.</li>";
                    valid = false;
                }
            }
            if (!CheckInstituteFamilyType() && !CheckOneSupervisor())
            {
                valid = false;
                liMessage.Text += "<li>هر خانوار باید یک سرپرست داشته باشد.</li>";
            }
            return valid;
        }

        #endregion

        #region WebMethod

        /// <summary>
        /// محاسبه سن از روی تاریخ تولد؛ این تابع از سمت کلاینت صدا زده می شود
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [WebMethod]
        public static string CalculateAgeFromBirthDate(string date)
        {
            bool valid = true;
            int years = -1;
            string age = string.Empty;
            if (!string.IsNullOrEmpty(date))
            {
                var pattern = "(?<year>((12|13)[0-9]{2}))/(?<month>(0?[1-9]|1[012]))/(?<day>(3[01]|[12][0-9]|0?[1-9]))";
                Match matchResult = Regex.Match(date, pattern);
                if (matchResult.Success)
                {
                    var year = Convert.ToInt32(matchResult.Groups["year"].Value);
                    var month = Convert.ToInt32(matchResult.Groups["month"].Value);
                    var day = Convert.ToInt32(matchResult.Groups["day"].Value);

                    if (month > 6 && month < 12 && day > 30)
                    {
                        valid = false;
                    }
                    else if (month == 12 && (((year + 1) % 4 != 0 && day == 30) || day == 31))
                    {
                        valid = false;
                    }

                    if (valid)
                    {
                        System.Globalization.PersianCalendar pcCalendar = new System.Globalization.PersianCalendar();
                        var birthDate = pcCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                        if (DateTime.Compare(DateTime.Now.Date, birthDate.Date) >= 0)
                        {
                            years = DateTime.Now.Year - birthDate.Year;
                            if (DateTime.Compare(DateTime.Now.Date, birthDate.Date) > 0 && ((birthDate.Month > DateTime.Now.Month) || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day)))
                                years--;
                            age = years.ToString();
                        }
                    }
                }
            }
            return age;
        }
        /// <summary>
        /// لیست تقسیمات کشوری را بر اساس کد پدر آن می دهد.
        /// </summary>
        /// <param name="parentId">کد پدر</param>
        /// <returns></returns>
        [WebMethod]
        public static ArrayList PopulatePlaces(int parentId)
        {
            ArrayList list = new ArrayList();
            String strQuery = "SELECT PlaceId, Name FROM Places where ParentId=@ParentId ORDER BY Name";
            using (SqlConnection con = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ParentId", parentId);
                    cmd.CommandText = strQuery;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        list.Add(new ListItem(
                       sdr["Name"].ToString(),
                       sdr["PlaceId"].ToString()
                        ));
                    }
                    con.Close();
                    return list;
                }
            }
        }
        /// <summary>
        /// چک کردن معتبر بودن کد ملی از طریق سرویس
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

        #endregion
        /// <summary>
        /// بارگذاری اطلاعات خانوار از پایگاه داده بر روی کنترل ها
        /// </summary>

        private void LoadFamily()
        {
            Family family = Family.GetFamily(UserId, true);
            if (family != null)
            {
                txtBlockNumber.Text = family.BlockNumber;
                txtFamilyNumber.Text = family.FamilyNumber;
                txtBuildingNumber.Text = family.BuildingNumber;
                ddlFamilyType.SelectedValue = family.FamilyType.ToString();
                ddlPopulationType.SelectedValue = family.PopulationType.ToString();
                ddlRegionStatus.SelectedValue = family.RegionStatus.ToString();
                ddlOwnerShipStatus.SelectedValue = family.OwnerShipStatus.ToString();

                ddlProvince.SelectedValue = family.PlaceInfo.ParentInfo.ParentId.ToString();
                BindTownShip(family.PlaceInfo.ParentInfo.ParentId);
                BindCity(family.PlaceInfo.ParentId);

                ddlTownShip.SelectedValue = family.PlaceInfo.ParentId.ToString();
                ddlCity.SelectedValue = family.PlaceId.ToString();

                if(family.PostalCode == string.Empty)
                {
                    txtPostalCode.Enabled = false;
                    chkNoPostalCode.Checked = true;
                }
                txtPostalCode.Text = family.PostalCode;
                txtMobileNumber.Text = family.MobileNumber;
                txtPhoneNumber.Text = family.PhoneNumber;
                txtAddress.Text = family.Address;
                lblFamilyId.Text = family.FamilyId.ToString();
                Session[itemFamilyListKey] = family.Members;
                BindGrid();
            }
        }

        protected void ddlTownShip_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity(Convert.ToInt32(ddlTownShip.SelectedValue));
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTownShip(Convert.ToInt32(ddlProvince.SelectedValue));
        }

        protected void ddlRelationShip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}