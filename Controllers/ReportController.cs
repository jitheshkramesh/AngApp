using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngApp.Models;
using System.Data;
using System.Data.SqlClient;


namespace AngApp.Controllers
{
    public class ReportController : Controller
    {
        public CodeDb db = new CodeDb();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult reportView()
        {
            DataTable DT = new DataTable();
            DT = new DataTable();
            DT = db.SqlDatatbl("SELECT * FROM VW_EMPLOYEE");
            Session["ReportSource"] = DT;
            Session["ReportName"] = "rpt_EmployeeListing.rpt";
            Response.Redirect("~/Reports/ReportView.aspx");
            return View();
        }

    }
}