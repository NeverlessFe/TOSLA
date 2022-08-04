using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CMSController : Controller
    {
        readonly ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];

        readonly DataTable DT = new DataTable();

        // GET: CMS
        public ActionResult CMS_EO()
        {
            return View();
        }

        public ActionResult CMS_Signature()
        {
            return View();
        }

        public ActionResult CMS_SPBList()
        {
            return View();
        }

        public ActionResult CMS_SPBApproval()
        {
            return View();
        }

        public ActionResult CMS_TOApproval()
        {
            return View();
        }

        public ActionResult MappingApprover()
        {
            return View();
        }

        public ActionResult CMS_TOList()
        {
            return View();
        }

        public ActionResult CMS_GetNamaEmployee(SignatureModel Model)
        {
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_Signature]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Option", System.Data.SqlDbType.VarChar);
                    command.Parameters["@Option"].Value = "Get Nama DDL";

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(DT);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DT.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DT.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }

            return Json(rows);
        }

        public ActionResult CMS_UploadFile()
        {
         
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            var path = "";
            string FileNameForDB;
            List<string> List = new List<string>();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                string UniqueID = Guid.NewGuid().ToString("N");

                var fileName = Path.GetFileName(UniqueID + "_" + file.FileName);

                path = Path.Combine(@"\\b7-drive.bintang7.com\File Upload Intranet\SPB\", fileName);

                file.SaveAs(path);
                List.Add(path);

                FileNameForDB = fileName;
            }

            
            return Json(path);
        }
        

    }
}