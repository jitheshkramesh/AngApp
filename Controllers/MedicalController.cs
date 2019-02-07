
using AngApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngApp.Controllers
{
    public class MedicalController : Controller
    {
        //private ILog _ILog;
        //public MedicalController()
        //{
        //    _ILog = Log.GetInstance;
        //}

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    _ILog.LogException(filterContext.Exception.ToString());
        //    filterContext.ExceptionHandled = true;
        //    this.View("Error").ExecuteResult(this.ControllerContext);
        //}

        // GET: Medical
        public ActionResult Index()
        {
            return RedirectToAction("GetDiagnosis");
        }

        public ActionResult GetDiagnosis()
        {
            OVODEntities5 _context = new OVODEntities5();
            return View(_context.DIAGNOSIS.ToList());
        }

        public ActionResult GetDiagnosisDetails(int id)
        {
            OVODEntities5 _context = new OVODEntities5();
            var model = _context.DIAGNOSIS
                .Where(x => x.ID == id)
                .FirstOrDefault();
            return View(model);
        }
             

        [HttpPost]
        public ActionResult Diagnosis(DIAGNOSI dIAGNOSI)
        {
            OVODEntities5 db = new OVODEntities5();
            var model = db.DIAGNOSIS.Where(x => x.ID.Equals(dIAGNOSI.ID)).FirstOrDefault();
            model.DIAGNOSIS = dIAGNOSI.DIAGNOSIS;
            db.SaveChanges();
            return RedirectToAction("GetDiagnosis");
        }

        public ActionResult GetDrug(int? id)
        {
            //throw new Exception("Something went wrong");
            OVODEntities5 _context = new OVODEntities5();
            var model = _context.DRUGs.ToList();
            if (id != null)
            {
                model = _context.DRUGs
                       .Where(x => x.DIG_ID == id)
                       .ToList();
            }
            return View(model);
        }

        public ActionResult DrugDetails(DRUG dRUG)
        {
            OVODEntities5 _context = new OVODEntities5();
            var model = _context.DRUGs
                .Where(x => x.ID == dRUG.ID)
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Drugs(DRUG dRUG)
        {
            OVODEntities5 db = new OVODEntities5();
            var model = db.DRUGs
                .Where(x => x.ID.Equals(dRUG.ID)).ToList();
            DRUG di = new DRUG()
            {
                DRUG1 = dRUG.DRUG1
            };
            di.ID.Equals(dRUG.ID);
            db.SaveChanges();
            return RedirectToAction("GetDrug");
        }
    }
}