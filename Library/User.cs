using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Shahab.CensusRreport.Library
{
    public class User
    {
        #region Propeties

        private int _userId; 
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
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

        private string _nationalCode;

        public string NationalCode
        {
            get { return _nationalCode; }
            set { _nationalCode = value; }
        }

        private string _mobileNumber;

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private DateTime _registerDate;

        public DateTime RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        private DateTime _lastLoginDate;

        public DateTime LastLoginDate
        {
            get { return _lastLoginDate; }
            set { _lastLoginDate = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
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

        #endregion

        /// <summary>
        /// این تابع اطلاعات کاربر را ذخیره می کند
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Save(User user)
        {
            bool success = false;
            user.UserId = UserId = PublicSqlMethods.NewID("Users", "UserId");

            user = (User)PublicSqlMethods.ConvertToFarsi(user);

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = user.UserId},
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 250) { Value = user.FirstName},
                    new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = user.LastName},
                    new SqlParameter("@NationalCode", SqlDbType.VarChar, 50) { Value = user.NationalCode},
                    new SqlParameter("@MobileNumber", SqlDbType.VarChar, 50) { Value = user.MobileNumber},
                    new SqlParameter("@Password", SqlDbType.NVarChar, 4000) { Value = user.Password},
                    new SqlParameter("@RegisterDate", SqlDbType.DateTime) { Value = user.RegisterDate},
                    new SqlParameter("@LastLoginDate", SqlDbType.DateTime) { Value = DBNull.Value},
                    new SqlParameter("@Email", SqlDbType.VarChar, 250) { Value = user.Email},
                    new SqlParameter("@Status", SqlDbType.Int) { Value = (int) user.Status},
                    new SqlParameter("@Priority", SqlDbType.Float) { Value = user.Priority},
                    new SqlParameter("@Flag", SqlDbType.Bit) { Value = false}
                };

                PublicSqlMethods.ExecuteNonQuery("InsertUser",CommandType.StoredProcedure, parameters);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// این تابع اطلاعات کاربر را بروزرسانی می کند.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(User user)
        {
            bool success = false;

            user = (User)PublicSqlMethods.ConvertToFarsi(user);

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = user.UserId});
                parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 250) { Value = user.FirstName});
                parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = user.LastName});
                parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.VarChar, 50) { Value = user.MobileNumber});
                if (user.Password != string.Empty)
                    parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 4000) { Value = user.Password});
                parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 250) { Value = user.Email});

                PublicSqlMethods.ExecuteNonQuery("UpdateUser", CommandType.StoredProcedure, parameters);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// این تابع چک می کند که آیا کد ملی وارد شده یکتا هست.
        /// </summary>
        /// <param name="nationalCode">کد ملی</param>
        /// <returns></returns>
        public static bool CheckUniqueNationalCode(string nationalCode)
        {
            string sqlCommand = " SELECT [dbo].[CheckUniqueNationalCodeInUsers] (@NationalCode)";
            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.Add("@NationalCode", SqlDbType.VarChar, 50).Value = nationalCode;
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
        /// <summary>
        /// این تابع اطلاعات کاربر را بر اساس نام کاربری و رمز عبور بر می گرداند.
        /// </summary>
        /// <param name="username">نام کاربری</param>
        /// <param name="password">رمز عبور</param>
        /// <returns></returns>
        public static User GetUser(string username, string password)
        {
            User user = null;
            var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@NationalCode", username),
                        new SqlParameter("@Password", password)
                    };

            DataTable dt = PublicSqlMethods.GetDataTable("GetUserByUsernamePass", CommandType.StoredProcedure, parameters);
            if (dt.Rows.Count > 0)
            {
                user = new User();
                user.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                user.FirstName = dt.Rows[0]["FirstName"].ToString();
                user.LastName = dt.Rows[0]["LastName"].ToString();
                user.NationalCode = dt.Rows[0]["NationalCode"].ToString();
                user.MobileNumber = dt.Rows[0]["MobileNumber"].ToString();
                user.Password = dt.Rows[0]["Password"].ToString();
                user.RegisterDate = Convert.ToDateTime(dt.Rows[0]["RegisterDate"]);
                user.LastLoginDate = dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue;
                user.Email = dt.Rows[0]["Email"].ToString();
                user.Status = (Enumurations.Status)Convert.ToInt32(dt.Rows[0]["Status"]);
                user.Priority = Convert.ToDouble(dt.Rows[0]["Priority"]);
            }
            return user;
        }
        /// <summary>
        /// این تابع اطلاعات کاربر را بر اساس کد بر می گزداند.
        /// </summary>
        /// <param name="userId">کد کاربر</param>
        /// <returns></returns>
        public static User GetUser(int userId)
        {
            User user = null;
            var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@UserId", userId)
                    };

            DataTable dt = PublicSqlMethods.GetDataTable("GetUser", CommandType.StoredProcedure, parameters);
            if (dt.Rows.Count > 0)
            {
                user = new User();
                user.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                user.FirstName = dt.Rows[0]["FirstName"].ToString();
                user.LastName = dt.Rows[0]["LastName"].ToString();
                user.NationalCode = dt.Rows[0]["NationalCode"].ToString();
                user.MobileNumber = dt.Rows[0]["MobileNumber"].ToString();
                user.Password = dt.Rows[0]["Password"].ToString();
                user.RegisterDate = Convert.ToDateTime(dt.Rows[0]["RegisterDate"]);
                user.LastLoginDate = dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue;
                user.Email = dt.Rows[0]["Email"].ToString();
                user.Status = (Enumurations.Status)Convert.ToInt32(dt.Rows[0]["Status"]);
                user.Priority = Convert.ToDouble(dt.Rows[0]["Priority"]);
            }
            return user;
        }
        /// <summary>
        /// بررسی یکتا بودن شناسه و رمز ورود 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckUniqueUser(string username, string password)
        {
            var existUser = CheckExistUser(username);

            if (!existUser)
                return true;
            else
            {
                var insertFamily = CheckInsertFamily(username);
                if (!insertFamily)
                    return false;
                else
                {
                    var supervisorFamily = CheckSupervisorFamily(username);
                    if (!supervisorFamily)
                        return false;
                    else
                    {
                        var existUserPass = CheckExistUser(username, password);
                        if (!existUserPass)
                            return true;
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// بررسی وجود خانوار برای کاربر بر اساس نام کاربری
        /// </summary>
        /// <param name="username">نام کاربری</param>
        /// <returns></returns>
        private bool CheckInsertFamily(string username)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT count(NationalCode) from Users where NationalCode = @UserName and Flag = 0");

            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;

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
        /// <summary>
        /// بررسی وجود کاربر بر اساس نام کاربری
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool CheckExistUser(string username)
        {
            string sqlCommand = " SELECT count(NationalCode) from Users WHERE NationalCode = @UserName";
            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    return result == "0" ? false : true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool CheckExistUser(string username, string password)
        {
            string sqlCommand = " SELECT [dbo].[ExistUser](@UserName, @Password, @UserId)";
            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = username;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    return result == "0" ? false : true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// بررسی اینکه این کاربر قبلا به عنوان سرپرست ثبت شده است یا خیر؟
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool CheckSupervisorFamily(string username)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT count(NationalCode) from FamilyMembers where NationalCode = @UserName and (RelationShip = @Supervisor OR RelationShip IS NULL)");

            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = username;
                cmd.Parameters.Add("@Supervisor", SqlDbType.Int).Value = Configurations.SupervisorValue;

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    return result == "0" ? false : true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// بررسی یکسان بودن رمز عبور ورودی با رمز عبور قدیمی
        /// </summary>
        /// <param name="oldPassword">رمز عبور</param>
        /// <returns></returns>
        public bool CheckEqualOldPassword(string oldPassword)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT Count(UserId) from Users where Password = HashBytes('MD5', @Password) and UserId = @UserId");

            using (SqlConnection conn = new SqlConnection(PublicSqlMethods.DefaultConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = oldPassword;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    return result == "0" ? false : true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// برررسی login بودن کاربر
        /// </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            return (HttpContext.Current.Session["authUserId"] != null);
        }
        /// <summary>
        /// خارج شده از سامانه
        /// </summary>
        public static void LogOut()
        {
            HttpContext.Current.Session["authUserId"] = null;
        }
        /// <summary>
        /// این تابع آخرین ورود کاربر به سامانه را بروز رسانی می کند.
        /// </summary>
        /// <param name="userId"></param>
        public void UpdateLastLogin(int userId)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append(" UPDATE [dbo].[Users] SET [LastLoginDate] = @LastLoginDate WHERE [UserId] = @UserId ");

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = userId},
                    new SqlParameter("@LastLoginDate", SqlDbType.DateTime) { Value = DateTime.Now}
                };

                PublicSqlMethods.ExecuteNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
            }
        }
        public static void UpdateFlag(int userId)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append(" UPDATE [dbo].[Users] SET [Flag] = 1 WHERE [UserId] = @UserId ");

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserId", SqlDbType.Int) { Value = userId}
                };

                PublicSqlMethods.ExecuteNonQuery(query.ToString(), parameters);
            }
            catch (Exception ex)
            {
            }
        }

    }
}