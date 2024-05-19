using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shahab.CensusRreport.Library
{
    public class Family
    {
        #region Properties
        private int _familyId;

        public int FamilyId
        {
            get { return _familyId; }
            set { _familyId = value; }
        }

        private string _blockNumber;

        public string BlockNumber
        {
            get { return _blockNumber; }
            set { _blockNumber = value; }
        }

        private string _familyNumber;

        public string FamilyNumber
        {
            get { return _familyNumber; }
            set { _familyNumber = value; }
        }

        private string _buildingNumber;

        public string BuildingNumber
        {
            get { return _buildingNumber; }
            set { _buildingNumber = value; }
        }

        private int _familyType;

        public int FamilyType
        {
            get { return _familyType; }
            set { _familyType = value; }
        }

        private int _populationType;

        public int PopulationType
        {
            get { return _populationType; }
            set { _populationType = value; }
        }

        private int _placeId;

        public int PlaceId
        {
            get { return _placeId; }
            set { _placeId = value; }
        }

        private int _regionStatus;

        public int RegionStatus
        {
            get { return _regionStatus; }
            set { _regionStatus = value; }
        }

        private int _ownerShipStatus;

        public int OwnerShipStatus
        {
            get { return _ownerShipStatus; }
            set { _ownerShipStatus = value; }
        }

        private string _postalCode;

        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        private string _mobileNumber;

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
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

        private Places _placeInfo;

        public Places PlaceInfo
        {
            get { return _placeInfo; }
            set { _placeInfo = value; }
        }

        private BaseInfo _familyTypeInfo;

        public BaseInfo FamilyTypeInfo
        {
            get { return _familyTypeInfo; }
            set { _familyTypeInfo = value; }
        }

        private BaseInfo _populationTypeInfo;

        public BaseInfo PopulationTypeInfo
        {
            get { return _populationTypeInfo; }
            set { _populationTypeInfo = value; }
        }

        private BaseInfo _regionStatusInfo;

        public BaseInfo RegionStatusInfo
        {
            get { return _regionStatusInfo; }
            set { _regionStatusInfo = value; }
        }

        private BaseInfo _ownerShipStatusInfo;

        public BaseInfo OwnerShipStatusInfo
        {
            get { return _ownerShipStatusInfo; }
            set { _ownerShipStatusInfo = value; }
        }

        private List<FamilyMembers> _members;

        public List<FamilyMembers> Members
        {
            get { return _members; }
            set { _members = value; }
        }

        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private DateTime _registerDate;

        public DateTime RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        #endregion

        /// <summary>
        /// این تابع اطلاعات خانوار را بر اساس کد کاربر بر می گرداند.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loadChildInfo"></param>
        /// <returns></returns>
        public static Family GetFamily(int userId, bool loadChildInfo)
        {
            Family family = null;
            var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@UserId", userId)
                    };

            DataTable dt = PublicSqlMethods.GetDataTable("GetFamilyByUserId", CommandType.StoredProcedure, parameters);
            if (dt.Rows.Count > 0)
            {
                family = new Family();
                family.FamilyId = Convert.ToInt32(dt.Rows[0]["FamilyId"]);
                family.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                family.BlockNumber = dt.Rows[0]["BlockNumber"].ToString();
                family.FamilyNumber = dt.Rows[0]["FamilyNumber"].ToString();
                family.BuildingNumber = dt.Rows[0]["BuildingNumber"].ToString();
                family.FamilyType = Convert.ToInt32(dt.Rows[0]["FamilyType"]);
                family.PopulationType = (dt.Rows[0]["PopulationType"] == DBNull.Value) ? -1 : Convert.ToInt32(dt.Rows[0]["PopulationType"]);
                family.PlaceId = Convert.ToInt32(dt.Rows[0]["PlaceId"]);
                family.RegionStatus = (dt.Rows[0]["RegionStatus"] == DBNull.Value) ? -1 : Convert.ToInt32(dt.Rows[0]["RegionStatus"]);
                family.OwnerShipStatus = Convert.ToInt32(dt.Rows[0]["OwnerShipStatus"]);
                family.PostalCode = dt.Rows[0]["PostalCode"].ToString();
                family.MobileNumber = dt.Rows[0]["MobileNumber"].ToString();
                family.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString();
                family.Address = dt.Rows[0]["Address"].ToString();
                family.Status = (Enumurations.Status)Convert.ToInt32(dt.Rows[0]["Status"]);
                family.Priority = Convert.ToDouble(dt.Rows[0]["Priority"]);
                family.RegisterDate = Convert.ToDateTime(dt.Rows[0]["RegisterDate"]);

                if (loadChildInfo)
                { 
                    family.PlaceInfo = Places.GetPlace(Convert.ToInt32(dt.Rows[0]["PlaceId"]), true);
                    family.Members = FamilyMembers.GetFamilyMembers(family.FamilyId);
                }
            }
            return family;
        }
        
        #region Save, Delete Family
        /// <summary>
        /// این تابع اطلاعات خانوار را ذخیره می کند.
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public bool Save(Family family)
        {
            bool success = false;
            FamilyId = PublicSqlMethods.NewID("Family", "FamilyId");

            family = (Family)PublicSqlMethods.ConvertToFarsi(family);

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@FamilyId", SqlDbType.Int) { Value = family.FamilyId},
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = family.UserId},
                    new SqlParameter("@BlockNumber", SqlDbType.VarChar, 50) { Value = family.BlockNumber},
                    new SqlParameter("@FamilyNumber", SqlDbType.VarChar, 50) { Value = family.FamilyNumber},
                    new SqlParameter("@BuildingNumber", SqlDbType.VarChar, 50) { Value = family.BuildingNumber},
                    new SqlParameter("@FamilyType", SqlDbType.Int) { Value = family.FamilyType},
                    new SqlParameter("@PopulationType", SqlDbType.Int) { Value = (family.PopulationType == -1) ? (object)DBNull.Value : family.PopulationType},
                    new SqlParameter("@PlaceId", SqlDbType.Int) { Value = family.PlaceId},
                    new SqlParameter("@RegionStatus", SqlDbType.Int) { Value = (family.RegionStatus == -1) ? (object)DBNull.Value : family.RegionStatus},
                    new SqlParameter("@OwnerShipStatus", SqlDbType.Int) { Value = family.OwnerShipStatus},
                    new SqlParameter("@PostalCode", SqlDbType.VarChar, 50) { Value = family.PostalCode},
                    new SqlParameter("@MobileNumber", SqlDbType.VarChar, 50) { Value = family.MobileNumber},
                    new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 50) { Value = family.PhoneNumber},
                    new SqlParameter("@Address", SqlDbType.NVarChar, -1) { Value = family.Address},
                    new SqlParameter("@Status", SqlDbType.Int) { Value = (int) family.Status},
                    new SqlParameter("@Priority", SqlDbType.Float) { Value = family.Priority},
                    new SqlParameter("@RegisterDate", SqlDbType.DateTime) {Value = DateTime.Now}
                };

                PublicSqlMethods.ExecuteNonQuery("InsertFamily",CommandType.StoredProcedure, parameters);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// این تابع اطلاعات خانوار مورد نظر را حذف می کند.
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        public bool Delete(int familyId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@FamilyId", familyId)
                    };
                PublicSqlMethods.ExecuteNonQuery("DeleteFamily",CommandType.StoredProcedure, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// این تابع اطلاعات خانوار و اعضای آن را ذخیره می کند.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (this.FamilyId != -1)
            {
                Delete(FamilyId);
            }

            var result = Save(this);

            if (result)
            {
                foreach (var member in this.Members)
                {
                    if (result)
                    {
                        if (FamilyType == 18)
                            member.RelationShip = -1;
                        member.FamilyId = this.FamilyId;
                        result = member.Save();
                    }
                }
            }
            return result;
        }
        #endregion
    }
}