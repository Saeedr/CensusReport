using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shahab.CensusRreport.Library
{
    public class FamilyMembers
    {
        #region Properties
        private int _familyMemberId;

        public int FamilyMemberId
        {
            get { return _familyMemberId; }
            set { _familyMemberId = value; }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private int _nationality;

        public int Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        private string _nationalCode;

        public string NationalCode
        {
            get { return _nationalCode; }
            set { _nationalCode = value; }
        }

        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private int _relationShip;

        public int RelationShip
        {
            get { return _relationShip; }
            set { _relationShip = value; }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        private int _inhabitancyStatus;

        public int InhabitancyStatus
        {
            get { return _inhabitancyStatus; }
            set { _inhabitancyStatus = value; }
        }

        private int _maritalStatus;

        public int MaritalStatus
        {
            get { return _maritalStatus; }
            set { _maritalStatus = value; }
        }

        private int _educationStatus;

        public int EducationStatus
        {
            get { return _educationStatus; }
            set { _educationStatus = value; }
        }

        private int _activityStatus;
        
        public int ActivityStatus
        {
            get { return _activityStatus; }
            set { _activityStatus = value; }
        }

        private int _jobType;

        public int JobType
        {
            get { return _jobType; }
            set { _jobType = value; }
        }

        private int _insuranceFirst;

        public int InsuranceFirst
        {
            get { return _insuranceFirst; }
            set { _insuranceFirst = value; }
        }

        private int _insuranceSecond;

        public int InsuranceSecond
        {
            get { return _insuranceSecond; }
            set { _insuranceSecond = value; }
        }

        private int _familyId;

        public int FamilyId
        {
            get { return _familyId; }
            set { _familyId = value; }
        }

        private Enumurations.Status _status;

        public Enumurations.Status Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private double _priority;

        public double Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private BaseInfo _inhabitancyStatusInfo;

        public BaseInfo InhabitancyStatusInfo
        {
            get { return _inhabitancyStatusInfo; }
            set { _inhabitancyStatusInfo = value; }
        }

        private BaseInfo _maritalStatusInfo;

        public BaseInfo MaritalStatusInfo
        {
            get { return _maritalStatusInfo; }
            set { _maritalStatusInfo = value; }
        }

        private BaseInfo _educationStatusInfo;

        public BaseInfo EducationStatusInfo
        {
            get { return _educationStatusInfo; }
            set { _educationStatusInfo = value; }
        }

        private BaseInfo _activityStatusInfo;

        public BaseInfo ActivityStatusInfo
        {
            get { return _activityStatusInfo; }
            set { _activityStatusInfo = value; }
        }

        private BaseInfo _jobTypeInfo;

        public BaseInfo JobTypeInfo
        {
            get { return _jobTypeInfo; }
            set { _jobTypeInfo = value; }
        }

        private BaseInfo _insuranceFirstInfo;

        public BaseInfo InsuranceFirstInfo
        {
            get { return _insuranceFirstInfo; }
            set { _insuranceFirstInfo = value; }
        }

        private BaseInfo _insuranceSecondInfo;

        public BaseInfo InsuranceSecondInfo
        {
            get { return _insuranceSecondInfo; }
            set { _insuranceSecondInfo = value; }
        }

        private BaseInfo _nationalityInfo;

        public BaseInfo NationalityInfo
        {
            get { return _nationalityInfo; }
            set { _nationalityInfo = value; }
        }

        private BaseInfo _genderInfo;

        public BaseInfo GenderInfo
        {
            get { return _genderInfo; }
            set { _genderInfo = value; }
        }

        private BaseInfo _relationShipInfo;

        public BaseInfo RelationShipInfo
        {
            get { return _relationShipInfo; }
            set { _relationShipInfo = value; }
        }

        private Family _familyInfo;

        public Family FamilyInfo
        {
            get { return _familyInfo; }
            set { _familyInfo = value; }
        }
        #endregion

        public void initializeFamilyMember()
        {
            FamilyMemberId = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            Nationality = -1;
            NationalCode = string.Empty;
            Gender = -1;
            RelationShip = -1;
            BirthDate = DateTime.MinValue;
            InhabitancyStatus = -1;
            MaritalStatus = -1;
            EducationStatus = -1;
            ActivityStatus = -1;
            JobType = -1;
            InsuranceFirst = -1;
            InsuranceSecond = -1;
            FamilyId = -1;
            Status = Enumurations.Status.Enabled;
            Priority = 0;
        }

        public FamilyMembers()
        {
            initializeFamilyMember();
        }
        /// <summary>
        /// این تابع لیست اعضای خانوار را بر اساس کد خانوار بر می گرداند.
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        public static List<FamilyMembers> GetFamilyMembers(int familyId)
        {
            List<FamilyMembers> members = new List<FamilyMembers>();
            var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@FamilyId", familyId)
                    };

            DataTable dt = PublicSqlMethods.GetDataTable("GetFamilyMembersByFamilyId", CommandType.StoredProcedure, parameters);

            for (int i = 0; i < dt.Rows.Count; i ++ )
            {

                var member = new FamilyMembers();

                member.FamilyMemberId = Convert.ToInt32(dt.Rows[i]["FamilyMemberId"]);
                member.FirstName = dt.Rows[i]["FirstName"].ToString();
                member.LastName = dt.Rows[i]["LastName"].ToString();
                member.Nationality = Convert.ToInt32(dt.Rows[i]["Nationality"]);
                member.NationalCode = dt.Rows[i]["NationalCode"].ToString();
                member.Gender = Convert.ToInt32(dt.Rows[i]["Gender"]);
                member.RelationShip = (dt.Rows[i]["RelationShip"] == DBNull.Value) ? -1 : Convert.ToInt32(dt.Rows[i]["RelationShip"]);
                member.BirthDate = Convert.ToDateTime(dt.Rows[i]["BirthDate"]);
                member.InhabitancyStatus = Convert.ToInt32(dt.Rows[i]["InhabitancyStatus"]);
                member.MaritalStatus = Convert.ToInt32(dt.Rows[i]["MaritalStatus"]);
                member.EducationStatus = (dt.Rows[i]["EducationStatus"] == DBNull.Value) ? -1 : Convert.ToInt32(dt.Rows[i]["EducationStatus"]);
                member.ActivityStatus = (dt.Rows[i]["ActivityStatus"] == DBNull.Value) ? -1 : Convert.ToInt32(dt.Rows[i]["ActivityStatus"]);
                member.JobType = (dt.Rows[i]["JobType"] == DBNull.Value) ? -1 : Convert.ToInt32(dt.Rows[i]["JobType"]);
                member.InsuranceFirst = Convert.ToInt32(dt.Rows[i]["InsuranceFirst"]);
                member.InsuranceSecond = Convert.ToInt32(dt.Rows[i]["InsuranceSecond"]);
                member.FamilyId = Convert.ToInt32(dt.Rows[i]["FamilyId"]);
                member.Status = (Enumurations.Status)Convert.ToInt32(dt.Rows[i]["Status"]);
                member.Priority = Convert.ToDouble(dt.Rows[i]["Priority"]);
                members.Add(member);
            }
            return members;
        }
        /// <summary>
        /// این تابع اطلاعات عضو خاتوار را ذخیره می کند.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            bool success = false;
            FamilyMemberId = PublicSqlMethods.NewID("FamilyMembers", "FamilyMemberId");
            var member = (FamilyMembers)PublicSqlMethods.ConvertToFarsi(this);
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@FamilyMemberId", SqlDbType.Int) { Value = member.FamilyMemberId},
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 250) { Value = member.FirstName},
                    new SqlParameter("@LastName", SqlDbType.NVarChar, 250) { Value = member.LastName},
                    new SqlParameter("@Nationality", SqlDbType.Int) { Value = member.Nationality},
                    new SqlParameter("@NationalCode", SqlDbType.VarChar, 50) { Value = member.NationalCode},
                    new SqlParameter("@Gender", SqlDbType.Int) { Value = member.Gender},
                    new SqlParameter("@RelationShip", SqlDbType.Int) { Value = ((member.RelationShip == -1) ? (object)DBNull.Value : member.RelationShip)},
                    new SqlParameter("@BirthDate", SqlDbType.DateTime) { Value = member.BirthDate},
                    new SqlParameter("@InhabitancyStatus", SqlDbType.Int) { Value = member.InhabitancyStatus},
                    new SqlParameter("@MaritalStatus", SqlDbType.Int) { Value = (member.MaritalStatus == 0) ? (object)DBNull.Value : member.MaritalStatus},
                    new SqlParameter("@EducationStatus", SqlDbType.Int) { Value = ((member.EducationStatus == -1) ? (object)DBNull.Value : member.EducationStatus)},
                    new SqlParameter("@ActivityStatus", SqlDbType.Int) { Value = ((member.ActivityStatus == -1) ? (object)DBNull.Value : member.ActivityStatus)},
                    new SqlParameter("@JobType", SqlDbType.Int) { Value = ((member.JobType == -1) ? (object)DBNull.Value : member.JobType)},
                    new SqlParameter("@InsuranceFirst", SqlDbType.Int) { Value = member.InsuranceFirst},
                    new SqlParameter("@InsuranceSecond", SqlDbType.Int) { Value = member.InsuranceSecond},
                    new SqlParameter("@FamilyId", SqlDbType.Int) { Value = member.FamilyId},
                    new SqlParameter("@Status", SqlDbType.Int) { Value = (int) member.Status},
                    new SqlParameter("@Priority", SqlDbType.Float) { Value = member.Priority}
                };

                PublicSqlMethods.ExecuteNonQuery("InsertFamilyMemeber", CommandType.StoredProcedure, parameters);
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// این تابع یکتا بودن کدملی را بررسی می کند.
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <param name="familyId"></param>
        /// <param name="expectRelation"></param>
        /// <returns></returns>
        public bool CheckUniqueNationalCode(string nationalCode, int familyId, int expectRelation)
        {
            string sqlCommand = " SELECT [dbo].[CheckUniqueNationalCodeInFamilyMembers] (@NationalCode,@SupervisorId,@FamilyId)";
            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.Add("@NationalCode", SqlDbType.VarChar, 50).Value = nationalCode;
                cmd.Parameters.Add("@SupervisorId", SqlDbType.Int).Value = expectRelation;
                cmd.Parameters.Add("@FamilyId", SqlDbType.Int).Value = familyId;
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    return result == "0" ? true : false;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}