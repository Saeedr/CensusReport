using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Shahab.CensusRreport.Library
{
    public class PublicSqlMethods
    {
        public static string DefaultConnectionString
        {
            get
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

                if (!String.IsNullOrEmpty(connectionString))
                    return connectionString;
                else
                    return string.Empty;
            }
        }
        /// <summary>
        /// این تابع آخرین کد وارد شده از جدول مورد نظر می دهد.
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strField"></param>
        /// <returns></returns>
        public static int NewID(string strTable, string strField)
        {
            var sqlCn = new SqlConnection { ConnectionString = DefaultConnectionString };
            sqlCn.Open();
            var sqlCmd = new SqlCommand
            {
                Connection = sqlCn,
                CommandText = String.Format(" SELECT MAX({0}) AS MAX_ID FROM {1} ",strField, strTable)
            };
            SqlDataReader sqlDrId = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            int newId = 0;
            sqlDrId.Read();
            if (sqlDrId.GetValue(0) == DBNull.Value)
            {
                newId++;
            }
            else
            {
                newId = Convert.ToInt32(sqlDrId.GetValue(sqlDrId.GetOrdinal("MAX_ID")));
                newId++;
            }
            sqlDrId.Close();
            sqlCn.Close();
            sqlCmd.Dispose();
            sqlCn.Dispose();

            return newId;
        }

        public static DataTable GetDataTable(string selectCommand,CommandType commandType, List<SqlParameter> parameters)
        {
            return GetDataTable(selectCommand, commandType, DefaultConnectionString, parameters);
        }
        public static DataTable GetDataTable(string selectCommand, List<SqlParameter> parameters)
        {
            return GetDataTable(selectCommand, CommandType.Text, DefaultConnectionString, parameters);
        }

        public static DataTable GetDataTable(string selectCommand, CommandType commandType, string connectionString,
                                             List<SqlParameter> parameters)
        {
            var dt = new DataTable();
            var sqlCn = new SqlConnection(connectionString);
            var da = new SqlDataAdapter("", sqlCn) { SelectCommand = { CommandTimeout = 600 } };
            try
            {
                if (sqlCn.State != ConnectionState.Open)
                    sqlCn.Open();
                da.SelectCommand.CommandText = selectCommand;
                da.SelectCommand.CommandType = commandType;
                if ((parameters != null) && (parameters.Count > 0))
                    da.SelectCommand.Parameters.AddRange(parameters.ToArray());
                da.Fill(dt);
            }
            finally
            {
                da.SelectCommand.Dispose();
                da.Dispose();
                sqlCn.Close();
            }
            return dt;
        }

        public static int ExecuteNonQuery(string strCommand)
        {
            return ExecuteNonQuery(strCommand, DefaultConnectionString);
        }
        public static int ExecuteNonQuery(string strCommand, string connectionString)
        {
            int result;
            var sqlCn = new SqlConnection(connectionString);
            try
            {
                sqlCn.Open();
                var sqlCmd = new SqlCommand(strCommand, sqlCn);
                result = sqlCmd.ExecuteNonQuery();
            }
            finally
            {
                sqlCn.Close();
            }
            return result;
        }
        public static int ExecuteNonQuery(string command, List<SqlParameter> parameters)
        {
            return ExecuteNonQuery(command, CommandType.Text, parameters, DefaultConnectionString);
        }

        public static int ExecuteNonQuery(string command, CommandType commandType, List<SqlParameter> parameters)
        {
            int result;

            var sqlCn = new SqlConnection(DefaultConnectionString);
            try
            {
                sqlCn.Open();
                var sqlCmd = new SqlCommand(command, sqlCn);
                sqlCmd.CommandType = commandType;
                sqlCmd.Parameters.AddRange(parameters.ToArray());
                result = sqlCmd.ExecuteNonQuery();
            }
            finally
            {
                sqlCn.Close();
            }
            return result;
        }
        public static int ExecuteNonQuery(string command, CommandType commandType, List<SqlParameter> parameters, string connectionString)
        {
            int result;

            var sqlCn = new SqlConnection(connectionString);
            try
            {
                sqlCn.Open();
                var sqlCmd = new SqlCommand(command, sqlCn);
                sqlCmd.CommandType = commandType;
                sqlCmd.Parameters.AddRange(parameters.ToArray());
                result = sqlCmd.ExecuteNonQuery();
            }
            finally
            {
                sqlCn.Close();
            }
            return result;
        }
        /// <summary>
        /// پر کردن لیست کشویی بر اساس نام جدول و فیلد متنی و فیلد مقدار
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldId"></param>
        /// <param name="fieldCaption"></param>
        /// <param name="extraCondition"></param>
        /// <param name="lc"></param>
        /// <param name="defaultValue"></param>
        /// <param name="fieldOrderBy"></param>
        /// <param name="selectedIndex"></param>
        public static void FillDDL(string tableName, string fieldId, string fieldCaption, string extraCondition,
                                   ListControl lc,
                                   string defaultValue, string fieldOrderBy, int selectedIndex)
        {
            try
            {
                var command = new StringBuilder();
                command.Append(fieldId != fieldCaption
                                   ? String.Format(" SELECT {0}, {1} FROM {2} ", fieldId, fieldCaption, tableName)
                                   : String.Format(" SELECT {0} FROM {1} ", fieldId, tableName));

                if (extraCondition != "")
                    command.AppendFormat(" WHERE {0} ", extraCondition);

                if (fieldOrderBy != "")
                    command.AppendFormat(" ORDER BY {0} ", fieldOrderBy);

                FillDDL(command.ToString(), fieldId, fieldCaption, lc, defaultValue, selectedIndex);
            }
            catch
            {
            }
        }

        public static void FillDDL(string selectQuery, string fieldId, string fieldCaption, ListControl lc,
                                   string defaultValue,
                                   int selectedIndex)
        {
            var sqlCn = new SqlConnection(DefaultConnectionString);
            try
            {
                sqlCn.Open();
                var sqlAdpList = new SqlDataAdapter(selectQuery, sqlCn);
                var dtList = new DataTable();
                sqlAdpList.Fill(dtList);

                lc.DataTextField = fieldCaption;
                lc.DataValueField = fieldId;
                lc.DataSource = dtList;
                lc.DataBind();

                if (defaultValue != "")
                    lc.Items.Insert(0, new ListItem(defaultValue, "-1"));

                if ((selectedIndex > -1) && (lc.Items.Count > selectedIndex))
                    lc.SelectedIndex = selectedIndex;
            }
            catch
            {
            }
            finally
            {
                sqlCn.Close();
            }
        }
        /// <summary>
        /// این تابع حروف عربی رشته ورودی را به فارسی تبدیل می کند
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToFarsi(string s)
        {
            const char arabicYeh = '\u064A';
            const char arabicKaf = '\u0643';
            const char farsiYeh = '\u06CC';
            const char farsiKaf = '\u06A9';

            return s.Replace(arabicYeh, farsiYeh).Replace(arabicKaf, farsiKaf);
        }
        /// <summary>
        /// این تابع حروف عربی شی مورد نظر را به فارسی تبدیل می کند
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ConvertToFarsi(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] p = type.GetProperties();
            foreach (PropertyInfo t in p)
            {
                if (t.PropertyType.Name.ToLower() == "string")
                    t.SetValue(obj, ConvertToFarsi(Convert.ToString(t.GetValue(obj))));
            }
            return obj;

        }
        /// <summary>
        /// این تابع معتبر بودن تاریخ ورودی را بررسی می کند.
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <returns></returns>
        public static bool IsValidDate(string date)
        {
            try
            {
                var select = new StringBuilder();
                select.Append(" SELECT [dbo].[IsValid] ('@Date')");
                var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@Date", date)
                    };

                DataTable dt = PublicSqlMethods.GetDataTable(select.ToString(), parameters);
                return Convert.ToBoolean(dt.Rows[0][0]);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// این تابع تاریخ شمسی را به میلادی تبدیل می کند.
        /// </summary>
        /// <param name="shamsiDate">تاریخ شمسی</param>
        /// <returns></returns>
        public static DateTime ShamsiToMiladi(string shamsiDate)
        {
            string sqlCommand = "SELECT [dbo].[SHD2MD] (@ShamsiDate)";
            using (SqlConnection conn = new SqlConnection(DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.Add("@ShamsiDate", SqlDbType.VarChar , 10).Value = shamsiDate;
                try
                {
                    conn.Open();
                    var date = cmd.ExecuteScalar().ToString();

                    return date == null ? DateTime.MinValue : Convert.ToDateTime(date);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }
        /// <summary>
        /// این تابع تاریخ میلادی را به شمسی تبدیل می کند.
        /// </summary>
        /// <param name="miladiDate">تاریخ میلادی</param>
        /// <returns></returns>
        public static string MiladiToShamsi(DateTime miladiDate)
        {
            string sqlCommand = " SELECT [dbo].[MD2SHD] (@miladiDate)";
            using (SqlConnection conn = new SqlConnection(DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.Add("@miladiDate", SqlDbType.VarChar, 10).Value = String.Format("{0:yyyy/MM/dd}", miladiDate);
                try
                {
                    conn.Open();
                    var date = cmd.ExecuteScalar().ToString();
                    return date == null ? string.Empty : date;
                }
                catch
                {
                    return string.Empty;
                }
            }

        }
    }
}