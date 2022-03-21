using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool CloseHandle(IntPtr handle);
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestLokasi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginExec(string Username, string Password)
        {
            List<string> ModelData = new List<string>();

            string Result = "";
            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;

            IntPtr tokenHandle = new IntPtr(0);
            try
            {

                SqlConnection conn = new SqlConnection(conString);
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                    command.Parameters["@Option"].Value = 19;

                    command.Parameters.Add("@UserNameGet", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@UserNameGet"].Value = Username;

                    command.Parameters.Add("@PasswordGet", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@PasswordGet"].Value = Password;

                    Result = (string)command.ExecuteScalar();
                }
                conn.Close();
                ModelData.Add(Result);

                if (Result == "VALID")
                {
                    Session["LoginSuccess"] = "True";
                }
                else
                {
                    Session["LoginSuccess"] = "False";
                }

                return Json(ModelData);
            }
            catch (Exception ex)
            {
                Session["LoginSuccess"] = "False";
                Result = ex.ToString();
            }
            Session["LoginSuccess"] = "True";
            ModelData.Add("VALID");
            ModelData.Add(Session["LoginSuccess"].ToString());
           
            ModelData.Add(Result);

            return Json(ModelData);
        }
    }
}