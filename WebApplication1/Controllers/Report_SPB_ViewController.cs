using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class Report_SPB_ViewController : Controller
    {
        // GET: Report_SPB_View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Report_SPB_Memo()
        {
            return View();
        }
        public ActionResult Rpt_SPB(string No_Surat)
        {
            ViewBag.No_Surat = No_Surat;
            return View();
        }
    }
}