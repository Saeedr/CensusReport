using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shahab.CensusRreport.Library
{

    public class BaseInfo
    {
        #region Properties
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private int _parentId;

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
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

        private BaseInfo _parentInfo;

        public BaseInfo ParentInfo
        {
            get { return _parentInfo; }
            set { _parentInfo = value; }
        }

        #endregion

        /// <summary>
        /// این تابع اطلاعات پایه را بر اساس کد آن بر می گرداند.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loadParentInfo"></param>
        /// <returns></returns>
        public static BaseInfo GetBaseInfo(int id, bool loadParentInfo)
        {
            try
            {
                var baseInfo = new BaseInfo();
                
                var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@BaseInfoId", id)
                    };

                DataTable dt = PublicSqlMethods.GetDataTable("GetBaseInfo", CommandType.StoredProcedure, parameters);
                if (dt.Rows.Count > 0)
                {
                    baseInfo.Id = Convert.ToInt32(dt.Rows[0]["BaseInfoId"]);
                    baseInfo.Value = dt.Rows[0]["Value"].ToString();
                    baseInfo.ParentId = dt.Rows[0]["ParentId"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["ParentId"]) : -1;

                    if (loadParentInfo)
                        baseInfo.ParentInfo = baseInfo.ParentId != -1 ? GetBaseInfo(Convert.ToInt32(dt.Rows[0]["ParentId"]), false) : null;
                }
                return baseInfo;
            }
            catch { return null; }
        }
    }
}