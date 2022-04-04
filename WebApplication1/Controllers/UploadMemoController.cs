using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UploadMemoController : Controller
    {
        // GET: UploadMemo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadMemoProcess()
        {
            return View();
        }

        public ActionResult UploadAttachment(string Periode,string TypePembayaran)
        {
            List<string> ModelData = new List<string>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            string Result;
            var path = "";
            string FileNameForDB;
            string URLAttachment;
            DataTable dt = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["dbReserveDiscount"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    var fileName = Path.GetFileName("TO_" + Periode + "_" + file.FileName);

                    //path = Path.Combine(@"\\kalbox-b7.bintang7.com\Intranetportal\Intranet Attachment\TO\", fileName);
                    path = Path.Combine(@"\\kalbox-b7.bintang7.com\Intranetportal\Intranet Attachment\TO\DEV", fileName);
                    //var path = Path.Combine("//10.167.1.78/File Sharing B7/Intranetportal/Intranet Attachment/CCC/AttachmentCC/CAPA/", fileName);

                    file.SaveAs(path);

                    FileNameForDB = fileName;
                }

                List<string> List = new List<string>();

                String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", path);
                //Create Connection to Excel work book 
                string sql = string.Format("Select [No],[Reg],[Cabang],[ShipToID],[Base],[Nama],[Subdist],[KlaimTO],[NoPA],'','','"+ Periode + "' from [{0}]", "Sheet1$");
                OleDbConnection oledbconn = new OleDbConnection(excelConnString);
                OleDbCommand oledbcmd = new OleDbCommand(sql, oledbconn);

                string ResultETL = "";

                using (oledbconn)
                {
                    oledbconn.Open();
                    DbDataReader dr2 = oledbcmd.ExecuteReader();
                    SqlBulkCopy bulkInsert = new SqlBulkCopy(conString);
                    bulkInsert.DestinationTableName = "tblR_tempUploadTO";
                    bulkInsert.WriteToServer(dr2);
                    oledbconn.Close();
                }

                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                    command.Parameters["@Option"].Value = 1;

                    command.Parameters.Add("@UserNameGet", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@UserNameGet"].Value = "Admin";

                    command.Parameters.Add("@TypePembayaran", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@TypePembayaran"].Value = TypePembayaran;

                    Result = (string)command.ExecuteScalar();
                }
                conn.Close();

                if(Result != "UNVALID")
                {
                    Session["NoMemo"] = Result;
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                        command.Parameters["@Option"].Value = 2;

                        command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                        command.Parameters["@NoMemoGet"].Value = Result;

                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(dt);
                    }
                    conn.Close();
                    List<DataRow> objAR = dt.AsEnumerable().ToList();

                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    Dictionary<string, object> row;
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    return Json(rows);
                }
            }
            catch(Exception ex)
            {

            }
            return Json(rows);
        }
        public ActionResult KonfirmasiUpload(string NoMemo)
        {
            List<string> ModelData = new List<string>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            string Result;
            var path = "";
            string FileNameForDB;
            string URLAttachment;
            DataTable dt = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["dbReserveDiscount"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 4;

                command.Parameters.Add("@NoMemoGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoMemoGet"].Value = Session["NoMemo"].ToString();

                command.Parameters.Add("@UserNameGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@UserNameGet"].Value = "Admin";

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();

            return Json(ModelData);
        }
        public ActionResult GetPeriod()
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
            string conString = mySetting.ConnectionString;
            SqlConnection conn = new SqlConnection(conString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 3;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();
            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            List<DataRow> DataRow = new List<DataRow>();
            int n = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ModelData.Add(dr[0].ToString());
            }

            return Json(ModelData);
        }
   
        public ActionResult GetListMasterSubdist()
        {
            DataTable dt = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["dbReserveDiscount"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString); 
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[STP_TOSLA]", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                    command.Parameters["@Option"].Value = 22;

                    SqlDataAdapter dataAdapt = new SqlDataAdapter();
                    dataAdapt.SelectCommand = command;

                    dataAdapt.Fill(dt);
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }

            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
    
    }
}