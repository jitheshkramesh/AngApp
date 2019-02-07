using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngApp.Models;
using System.Net.Http;

namespace AngApp.Controllers
{
    public class DesignationController : Controller
    {
        // GET: Designation
        public ActionResult Index()
        {
            IEnumerable<EmpDesignation> EmpDesg;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("api/DESIGNATIONs").Result;
            //response.EnsureSuccessStatusCode();

            //var foo = response.Content.ReadAsAsync<string>().Result;
            EmpDesg = response.Content.ReadAsAsync<IEnumerable<EmpDesignation>>().Result;
            return View(EmpDesg);
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new EmpDesignation());
            }
            else {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("api/DESIGNATIONs/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EmpDesignation>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddorEdit(EmpDesignation des)
        {
            if (ModelState.IsValid)
            {
                if (des.DG_ID == 0)
                {
                    HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("api/DESIGNATIONs", des).Result;
                    TempData["SaveMessage"] = "Saved Successfully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("api/DESIGNATIONs/" + des.DG_ID.ToString(), des).Result;
                    TempData["SaveMessage"] = "Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            else {
                return View();
            }
            
        }

        public ActionResult Delete(int id) {
            HttpResponseMessage response = GlobalVariables.webApiClient.DeleteAsync("api/DESIGNATIONs/" + id.ToString()).Result;
            TempData["SaveMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}