using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shahab.CensusRreport.Library
{
    public class Places
    {
        #region Properties
        private int _placeId;

        public int PlaceId
        {
            get { return _placeId; }
            set { _placeId = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        private double _pririty;

        public double Pririty
        {
            get { return _pririty; }
            set { _pririty = value; }
        }

        private Places _parentInfo;

        public Places ParentInfo
        {
            get { return _parentInfo; }
            set { _parentInfo = value; }
        }

        #endregion

        /// <summary>
        /// این تابع لیست تقسیمات کشوری را بر اساس کد پدرش می دهد.
        /// </summary>
        /// <param name="placeId"></param>
        /// <param name="loadParentInfo"></param>
        /// <returns></returns>
        public static Places GetPlace(int placeId, bool loadParentInfo)
        {
            var place = new Places();
            var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@PlaceId", placeId)
                    };

            DataTable dt = PublicSqlMethods.GetDataTable("GetPlace", CommandType.StoredProcedure, parameters);
            if (dt.Rows.Count > 0)
            {
                place.PlaceId = Convert.ToInt32(dt.Rows[0]["PlaceId"]);
                place.Name = dt.Rows[0]["Name"].ToString();
                place.ParentId = dt.Rows[0]["ParentId"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["ParentId"]) : -1;

                if (loadParentInfo)
                {
                    place.ParentInfo = place.ParentId != -1 ? GetPlace(Convert.ToInt32(dt.Rows[0]["ParentId"]), false) : null;
                    place.ParentInfo.ParentInfo = place.ParentInfo.ParentId != -1 ? GetPlace(Convert.ToInt32(place.ParentInfo.PlaceId), false) : null;
                }
            }
            return place;
        }
    }
}