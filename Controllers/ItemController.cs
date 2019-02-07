using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace AngApp.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(getAllItem());
        }

        IEnumerable<Item> getAllItem()
        {
            using (OVODEntities5 db = new OVODEntities5())
            {
                return db.Items.ToList();
            }
        }

        public ActionResult AddorEdit(int id=0)
        {
            Item item = new Item
            {
                Item_Image = "~/Files/Items/default.jpg"
            };
            if (id != 0) {
                using (OVODEntities5 db = new OVODEntities5()) {
                    item = db.Items.Where(x => x.Item_Id == id).FirstOrDefault<Item>(); 
                }
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult AddorEdit(Item item)
        {
            try
            {
                if (item.Upload_Image != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.Upload_Image.FileName);
                    string extension = Path.GetExtension(item.Upload_Image.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyMMddmmssfff") + extension;
                    item.Item_Image = "~/Files/Items/" + fileName;
                    item.Upload_Image.SaveAs(Path.Combine(Server.MapPath("~/Files/Items/"), fileName));
                }
                using (OVODEntities5 db = new OVODEntities5())
                {
                    if (item.Item_Id == 0)
                    {
                        db.Items.Add(item);
                        item.Item_Date = DateTime.Now;
                        item.Item_Expiry = DateTime.Now.AddYears(2);
                        db.SaveChanges();
                    }
                    else
                    {
                        item.Item_Date = DateTime.Now;
                        item.Item_Expiry = DateTime.Now.AddYears(2);
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                //ViewRenderer vw = new ViewRenderer();
                //return Json(new { success = true, html = vw.RenderPartialView("ViewAll", getAllItem()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString( this, "ViewAll", getAllItem()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (OVODEntities5 db = new OVODEntities5())
                {
                    Item item = db.Items.Where(x => x.Item_Id == id).FirstOrDefault();
                    db.Items.Remove(item);
                    db.SaveChanges();
                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", getAllItem()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}