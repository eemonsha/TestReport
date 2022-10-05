using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestReport.Models;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace TestReport.Controllers
{
    public class RepController : Controller
    {
        private TestEntities _context = new TestEntities();

        
        public ActionResult Index()
        {
            return View();
        }

        public void pdfdemo(ReportDocument InputData)
        {

            var rprt = InputData;
            string filepath;
            Response.Clear();
            filepath = Server.MapPath("~/" + "demo.pdf");
            rprt.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);
            FileInfo fileinfo = new FileInfo(filepath);
            Response.AddHeader("Content-Disposition", "inline;filenam=demo.pdf");
            Response.ContentType = "application/pdf";
            Response.WriteFile(fileinfo.FullName);
        }


        //Qr
        public ActionResult QRtest(int ItemCode , string St)
        {
            

            var rrpt = new ReportDocument();
            rrpt.Load(Path.Combine(Server.MapPath("~/Reports/QRR.rpt")));
            SqlConnection con = new SqlConnection(@"Data Source=103.95.98.190,49170;Initial Catalog=Test;User ID=emon;Password=TestProject123;");
            SqlCommand cmd = new SqlCommand("Select * from Users ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            sda.Fill(dt);

            rrpt.SetDataSource(dt);
            rrpt.SetDatabaseLogon("emon", "TestProject123", "103.95.98.190,49170", "Test");
            rrpt.SetParameterValue("ItemCode", ItemCode);
            //rrpt.SetParameterValue("Status", St);


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + ItemCode + " " + DateTime.Now + " - _QRCodes.pdf");

            }
            catch(Exception ex)
            {
                throw;
            }
        }


        //sellReport
        public ActionResult SellsRep(int SaleID)
        {
            



            var rrpt = new ReportDocument();
            rrpt.Load(Path.Combine(Server.MapPath("~/Reports/Sales.rpt")));
            SqlConnection con = new SqlConnection(@"Data Source=103.95.98.190,49170;Initial Catalog=Test;User ID=emon;Password=TestProject123;");
            SqlCommand cmd = new SqlCommand("Select * from Users ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            sda.Fill(dt);

            rrpt.SetDataSource(dt);
            rrpt.SetDatabaseLogon("emon", "TestProject123", "103.95.98.190,49170", "Test");
            rrpt.SetParameterValue("SaleID", SaleID);
            


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + SaleID + " - " + "_sellrep.pdf");

            }
            catch(Exception ex)
            {
                throw;
            }
        }



        //scorewise student report
        public ActionResult StudentScore()
        {
            return View();
        }


        [HttpPost]
        public ActionResult StudentScore(int min, int max)
        {


            var rrpt = new ReportDocument();
            rrpt.Load(Path.Combine(Server.MapPath("~/Reports/Scorewisestudent.rpt")));
            SqlConnection con = new SqlConnection(@"Data Source=103.95.98.190,49170;Initial Catalog=Test;User ID=emon;Password=TestProject123;");
            SqlCommand cmd = new SqlCommand("Select * from Users ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            sda.Fill(dt);

            rrpt.SetDataSource(dt);
            rrpt.SetDatabaseLogon("emon", "TestProject123", "103.95.98.190,49170", "Test");
            rrpt.SetParameterValue("min", min);
            rrpt.SetParameterValue("max", max);


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream, "application/pdf", "" + min + " " + DateTime.Now + " - _QRCodes.pdf");

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        private string GetConStr()
        {
            var conn = @"Data Source=103.95.98.190,49170;Initial Catalog=Test;User ID=emon;Password=TestProject123";
            return conn;
        }



    }
}


