using AngApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngApp.Controllers
{
    public class HelpController : Controller
    {
        OVODEntities5 db = new OVODEntities5();
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployeeCode(string term)
        {
            
            return Json(db.EMPLOYEEs.Where(c => c.EM_NAME.Contains(term) || c.EM_CODE.Contains(term) || c.EM_ID.ToString().Contains(term))
                .Select(a => new { label = a.EM_ID + " / " + a.EM_CODE + " / " + a.EM_NAME, id = a.EM_CODE, value = a.EM_NAME }),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeName(string term)
        {
            return Json(db.EMPLOYEEs.Where(c => c.EM_NAME.Contains(term) || c.EM_CODE.Contains(term) || c.EM_ID.ToString().Contains(term))
                .Select(a => new { label = a.EM_ID + " / " + a.EM_CODE + " / " + a.EM_NAME, id = a.EM_NAME, value = a.EM_CODE }),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployee(string term)
        {
            return Json(db.EMPLOYEEs.Where(c => c.EM_NAME.Contains(term) || c.EM_CODE.Contains(term) || c.EM_ID.ToString().Contains(term))
                .Select(a => new { label = a.EM_ID + " / " + a.EM_CODE + " / " + a.EM_NAME, id = a.EM_ID, value = a.EM_CODE,desc=a.EM_NAME }),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTT(string term)
        {
            return Json(db.TTMASTs.Where(c => c.TT_CODE.Contains(term) || c.TT_DESC.Contains(term) || c.TT_ID.ToString().Contains(term))
                .Select(a => new { label = a.TT_ID + " / " + a.TT_CODE + " / " + a.TT_DESC, id = a.TT_ID, value = a.TT_CODE, desc = a.TT_DESC }),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTTLve(string term)
        {
            return Json(db.TTMASTs
                .Where(c => (c.TT_CODE.Contains(term) 
                || c.TT_DESC.Contains(term) 
                || c.TT_ID.ToString().Contains(term))&& c.TT_GROUP.ToString().ToUpper()=="L")
                .Select(a => new { label = a.TT_ID + " / " + a.TT_CODE + " / " + a.TT_DESC, id = a.TT_ID, value = a.TT_CODE, desc = a.TT_DESC }),
                JsonRequestBehavior.AllowGet);
        }

    }
}