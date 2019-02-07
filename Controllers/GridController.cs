using AngApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
//using MvcApplication1.Models;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Configuration;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
using static AngApp.Models.CodeDb;

namespace AngApp.Controllers
{
    public class GridController : Controller
    {
        public CodeDb db = new CodeDb();
        // GET: Grid
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Sales()
        {

            DataTable DT = new DataTable();
            DT = db.SqlDatatbl("SELECT INV_NUM,INV_DATE,LOC_CODE,LOC_NAME,CUST_NAME,NETAMT FROM CREDIT_INVOICE  order by inv_date desc");
            List<CodeDb.Sales> Smd = new List<CodeDb.Sales>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Sales
                {

                    INV_NUM = Convert.ToString(dr["INV_NUM"]), // adding data from dataset row in to list<modeldata>
                    INV_DATE = Convert.ToDateTime(dr["INV_DATE"]),
                    LOC_CODE = Convert.ToString(dr["LOC_CODE"]),
                    LOC_NAME = Convert.ToString(dr["LOC_NAME"]),
                    CUST_NAME = Convert.ToString(dr["CUST_NAME"]),
                    NETAMT = Convert.ToDecimal(dr["NETAMT"])

                });

            }
            //DT = new DataTable();
            //DT = db.SqlDatatbl("select bnk_code,bnk_name from bank_type");
            //ViewData["bankDetails"] = DT;

            return View(Smd);
        }

        public ActionResult SyncSales()
        {

            DataTable DT = new DataTable();
            DT = db.SqlDatatbl("SELECT INV_NUM,INV_DATE,LOC_CODE,LOC_NAME,CUST_NAME,NETAMT FROM CREDIT_INVOICE  order by inv_date desc");
            List<CodeDb.Sales> Smd = new List<Models.CodeDb.Sales>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Sales
                {

                    INV_NUM = Convert.ToString(dr["INV_NUM"]), // adding data from dataset row in to list<modeldata>
                    INV_DATE = Convert.ToDateTime(dr["INV_DATE"]),
                    LOC_CODE = Convert.ToString(dr["LOC_CODE"]),
                    LOC_NAME = Convert.ToString(dr["LOC_NAME"]),
                    CUST_NAME = Convert.ToString(dr["CUST_NAME"]),
                    NETAMT = Convert.ToDecimal(dr["NETAMT"])

                });

            }
            DT = new DataTable();
            DT = db.SqlDatatbl("select bnk_code,bnk_name from bank_type");
            ViewData["bankDetails"] = DT;

            return View(Smd);
        }

        public JsonResult dtgJson(string id = null)
        {
            string invnum = id;
            DataTable dt = new DataTable();
            dt = db.SqlDatatbl("select item_code,item_name,units,qtyperunit,item_rate,item_amt from credit_invoice_tran where inv_num='" + invnum + "'");
            // return view(dt);

            //List<CodeDb.CodeDbs> lmd = new List<CodeDb.CodeDbs>();  // creating list of model.

            //DataSet ds = new DataSet();



            //foreach (DataRow dr in dt.Rows) // loop for adding add from dataset to list<modeldata>
            //{
            //    lmd.Add(new CodeDb.CodeDbs
            //    {
            //        ACCT_CODE = Convert.ToString(dr["item_code"]), // adding data from dataset row in to list<modeldata>
            //        ACCT_DESC = Convert.ToString(dr["item_name"]),
            //        OPN_BAL = Convert.ToString(dr["units"]),
            //        CLS_BAL = Convert.ToDecimal(dr["qtyperunit"]),
            //        TOTAL_CR = Convert.ToDecimal(dr["item_rate"]),
            //        TOTAL_DR = Convert.ToDecimal(dr["item_amt"])
            //    });
            //}

            DataTable DT = new DataTable();
            DT = db.SqlDatatbl("SELECT INV_NUM,INV_DATE,LOC_CODE,LOC_NAME,CUST_NAME,NETAMT FROM CREDIT_INVOICE  order by inv_date desc");
            List<CodeDb.Sales> Smd = new List<Models.CodeDb.Sales>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Sales
                {

                    INV_NUM = Convert.ToString(dr["INV_NUM"]), // adding data from dataset row in to list<modeldata>
                    INV_DATE = Convert.ToDateTime(dr["INV_DATE"]),
                    LOC_CODE = Convert.ToString(dr["LOC_CODE"]),
                    LOC_NAME = Convert.ToString(dr["LOC_NAME"]),
                    CUST_NAME = Convert.ToString(dr["CUST_NAME"]),
                    NETAMT = Convert.ToDecimal(dr["NETAMT"])

                });

            }

            DataTable ddt = new DataTable();
            ddt = db.SqlDatatbl("select inv_num,inv_date,loc_code,loc_name,cust_name,netamt from credit_invoice where inv_num='" + invnum + "'");
            string INV1 = ddt.Rows[0]["inv_num"].ToString();
            string IND = ddt.Rows[0]["inv_date"].ToString();
            ViewData["invnum"] = INV1;
            ViewData["invdate"] = IND;
            return Json(Smd, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportToExcel(DataTable dt)
        {
            var gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
        }

    }
}