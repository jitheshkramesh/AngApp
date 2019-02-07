using AngApp.Models;
using AngApp.ViewModel;
using CrystalDecisions.CrystalReports.Engine;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace AngApp.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        OVODEntities5 _context = new OVODEntities5();
        public CodeDb db = new CodeDb();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddNumber(FormCollection frmcollect)
        {
            int c = 0;
            int Fst = Convert.ToInt32(frmcollect["txtNum1"]);
            int Sec = Convert.ToInt32(frmcollect["txtNum2"]);
            // c = a + b;
            CalculatorService.CalculatorSoapClient Client = new CalculatorService.CalculatorSoapClient();
            c = Client.Add(Fst, Sec);
            ViewBag.var = c;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(Models.CodeDb user)
        {
            DataTable dt = new DataTable();
            if (ModelState.IsValid)
            {
                //if (user.IsGrid(user.INV_NUM))
                dt = db.IsValid(user.UserName, user.Password);
                if (dt.Rows.Count > 0)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    Session["username"] = user.UserName;
                    Session["EmpName"] = dt.Rows[0]["EmpName"].ToString();
                    Session["ImagePath"] = dt.Rows[0]["FilePath"].ToString();
                    return RedirectToAction("Index", "App");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        //[Authorize]
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut;
            Session["username"] = null;
            //return View("Login");
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult CountryMaster(Models.CodeDb.Country country)
        {
            return View(country);
        }

        [HttpPost]
        public ActionResult CountryMaster(FormCollection formCollection)
        {
            string Code = formCollection["txtCode"];
            string Desc = formCollection["txtName"];
            Int64 CurrId = Convert.ToInt64(formCollection["txtCurrency"]);
            if (Code != "")
            {
                db.con.Open();
                string Qry = "";
                Qry = "INSERT INTO COUNTRY (CN_CD,CN_NAME,CN_CURR) VALUES ('" + Code + "','" + Desc + "','" + CurrId + "')";
                SqlCommand myCommand = new SqlCommand(Qry, db.con);
                myCommand.ExecuteNonQuery();
                db.con.Close();
            }
            return RedirectToAction("CountryMaster", "Home");
        }

        [HttpGet]
        public ActionResult CountryList()
        {
            DataTable DT = new DataTable();
            DT = new DataTable();
            DT = db.SqlDatatbl("SELECT CN_CD,CN_NAME FROM COUNTRY WHERE CN_CD LIKE '%' ORDER BY CN_CD");
            ViewData["CountryList"] = DT;

            List<CodeDb.Country> Smd = new List<Models.CodeDb.Country>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Country
                {
                    CN_CD = Convert.ToString(dr["CN_CD"]),
                    CN_NAME = Convert.ToString(dr["CN_NAME"])
                });

            }

            return View(Smd);
        }

        [HttpGet]
        public ActionResult EmployeeDetails()
        {
            DataTable DT = new DataTable();
            DT = new DataTable();
            DT = db.SqlDatatbl("SELECT EM_CODE,EM_NAME,EM_ADDRESS FROM VW_EMPLOYEE ORDER BY EM_CODE");
            ViewData["EmpDetails"] = DT;

            List<CodeDb.Employee> Smd = new List<Models.CodeDb.Employee>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Employee
                {
                    EM_CODE = Convert.ToString(dr["EM_CODE"]),
                    EM_NAME = Convert.ToString(dr["EM_NAME"]),
                    EM_ADDRESS = Convert.ToString(dr["EM_ADDRESS"])
                });

            }

            return View(Smd);
        }

        [HttpGet]
        public ActionResult DepartmentMaster(Models.CodeDb.Department Department)
        {
            return View(Department);
        }

        [HttpPost]
        public ActionResult DepartmentMaster(FormCollection formCollection)
        {
            string Code = formCollection["txtCode"];
            string Desc = formCollection["txtName"];
            if (Code != "")
            {
                db.con.Open();
                string Qry = "";
                Qry = "INSERT INTO DEPARTMENT (DP_CD,DP_NAME) VALUES ('" + Code + "','" + Desc + "')";
                SqlCommand myCommand = new SqlCommand(Qry, db.con);
                myCommand.ExecuteNonQuery();
                db.con.Close();
            }
            return RedirectToAction("DepartmentMaster", "Home");
        }

        [HttpGet]
        public ActionResult DepartmentList()
        {
            DataTable DT = new DataTable();
            DT = new DataTable();
            DT = db.SqlDatatbl("SELECT DP_CD,DP_NAME FROM DEPARTMENT WHERE DP_CD LIKE '%' ORDER BY DP_CD");
            ViewData["DepartmentList"] = DT;

            List<CodeDb.Department> Smd = new List<Models.CodeDb.Department>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Department
                {
                    DP_CD = Convert.ToString(dr["DP_CD"]),
                    DP_NAME = Convert.ToString(dr["DP_NAME"])
                });

            }

            return View(Smd);
        }

        [HttpPost]
        public ActionResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumndir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pagesize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;
            OVODEntities5 oe = new OVODEntities5();
            var v = (from a in oe.DEPARTMENTs
                     orderby a.DP_CD
                     select a
                     );


            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumndir)))
            //{
            //    v = v.OrderBy(sortColumn + " " + sortColumndir);
            //}
            totalRecords = v.Count();
            var data = v.Skip(skip).Take(pagesize).ToList();
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DesignationMaster(Models.CodeDb.Designation Designation)
        {
            return View(Designation);
        }

        [HttpGet]
        public ActionResult Employee(string id = null)
        {
            if (id is null)
            {
                id = "%";
            }
            DataTable DT = new DataTable();
            DT = new DataTable();
            string strSql = "";
            strSql = @"SELECT TOP 1 EM_CODE,EM_NAME,EM_LASTNAME,EM_ADDRESS,EM_PHONE,EM_GENDER,EM_DOB, " +
            @" EM_DOJ,EM_PHOTO,EM_COUNTRY,EM_ACTIVE FROM EMPLOYEE WHERE EM_CODE LIKE '" + id + "' ORDER BY EM_CODE ";

            DT = db.SqlDatatbl(strSql);
            ViewData["EmployeeList"] = DT;
            if (DT.Rows.Count > 0)
            {
                //List<CodeDb.Employee> Smd = new List<Models.CodeDb.Employee>();
                foreach (DataRow dr in DT.Rows)
                {
                    //    Smd.Add(new CodeDb.Employee
                    //    {
                    ViewBag.EM_CODE = Convert.ToString(dr["EM_CODE"]);
                    ViewBag.EM_NAME = Convert.ToString(dr["EM_NAME"]);
                    ViewBag.EM_LASTNAME = Convert.ToString(dr["EM_LASTNAME"]);
                    ViewBag.EM_ADDRESS = Convert.ToString(dr["EM_ADDRESS"]);
                    ViewBag.EM_PHONE = Convert.ToString(dr["EM_PHONE"]);
                    ViewBag.EM_GENDER = Convert.ToString(dr["EM_GENDER"]);
                    ViewBag.EM_DOB = Convert.ToDateTime(dr["EM_DOB"]);
                    ViewBag.EM_DOJ = Convert.ToDateTime(dr["EM_DOJ"]);
                    ViewBag.EM_PHOTO = Convert.ToString(dr["EM_PHOTO"]);
                    ViewBag.EM_COUNTRY = Convert.ToString(dr["EM_COUNTRY"]);
                    ViewBag.EM_ACTIVE = Convert.ToBoolean(dr["EM_ACTIVE"]);
                    //});

                }
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employee(FormCollection formCollection, HttpPostedFileBase uploadFile, string uploadText, string ABC)
        {
            string EM_CODE = formCollection["txtCode"];
            string EM_NAME = formCollection["txtFName"];
            string EM_LASTNAME = formCollection["txtLName"];
            string EM_ADDRESS = formCollection["txtAddress"];

            string EM_PHONE = formCollection["txtPhone"];
            string EM_GENDER = formCollection["txtGender"];
            DateTime EM_DOB = Convert.ToDateTime(formCollection["txtDob"]);
            DateTime EM_DOJ = Convert.ToDateTime(formCollection["txtDoj"]);

            string EM_PHOTO = formCollection["txtPhoto"];
            string EM_COUNTRY = formCollection["txtCountry"];
            Boolean EM_ACTIVE = formCollection["chkActive"] != null ? true : false;
            string COMPCD = "001";
            //string FileName = "";
            //string extn = "";
            string relativePath = "";
            string relpath = "";
            string physicalPath = "";

            if (uploadFile.ContentLength > 0)
            {
                relativePath = "~/Files/Images/" + Path.GetFileName(uploadFile.FileName);
                relpath = "~/Files/Images/" + EM_CODE.ToString() + ".jpg";
                physicalPath = Server.MapPath(relpath);
                uploadFile.SaveAs(physicalPath);
            }

            //FileName = Path.GetFileNameWithoutExtension(uploadFile.FileName);
            //extn = Path.GetExtension(uploadFile.FileName);

            EM_PHOTO = relpath;
            Int64 CurrId = Convert.ToInt64(formCollection["txtCurrency"]);
            if (EM_CODE != "")
            {
                db.con.Open();
                SqlCommand cmd = new SqlCommand("EMPLOYEE_SAVE", db.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@COMPCD", COMPCD));
                cmd.Parameters.Add(new SqlParameter("@EM_CODE", EM_CODE));
                cmd.Parameters.Add(new SqlParameter("@EM_NAME", EM_NAME));
                cmd.Parameters.Add(new SqlParameter("@EM_LASTNAME", EM_LASTNAME));
                cmd.Parameters.Add(new SqlParameter("@EM_ADDRESS", EM_ADDRESS));
                cmd.Parameters.Add(new SqlParameter("@EM_PHONE", EM_PHONE));
                cmd.Parameters.Add(new SqlParameter("@EM_GENDER", EM_GENDER));
                cmd.Parameters.Add(new SqlParameter("@EM_DOB", EM_DOB));
                cmd.Parameters.Add(new SqlParameter("@EM_DOJ", EM_DOJ));
                cmd.Parameters.Add(new SqlParameter("@EM_PHOTO", EM_PHOTO));
                cmd.Parameters.Add(new SqlParameter("@EM_COUNTRY", EM_COUNTRY));
                cmd.Parameters.Add(new SqlParameter("@EM_ACTIVE", EM_ACTIVE));
                cmd.ExecuteNonQuery();
                db.con.Close();
            }
            return RedirectToAction("EmployeeList", "Home");
        }

        [HttpGet]
        public ActionResult EmployeeList()
        {
            DataTable DT = new DataTable();
            DT = new DataTable();
            string strSql = "";
            strSql = @"SELECT EM_CODE,EM_NAME,EM_LASTNAME,EM_ADDRESS,EM_PHONE,EM_GENDER,EM_DOB, " +
            @" EM_DOJ,EM_PHOTO,EM_COUNTRY,EM_ACTIVE FROM EMPLOYEE WHERE EM_CODE LIKE '%' ORDER BY EM_CODE ";

            DT = db.SqlDatatbl(strSql);
            ViewData["EmployeeList"] = DT;

            List<CodeDb.Employee> Smd = new List<Models.CodeDb.Employee>();
            foreach (DataRow dr in DT.Rows)
            {
                Smd.Add(new CodeDb.Employee
                {
                    EM_CODE = Convert.ToString(dr["EM_CODE"]),
                    EM_NAME = Convert.ToString(dr["EM_NAME"]),
                    EM_LASTNAME = Convert.ToString(dr["EM_LASTNAME"]),
                    EM_ADDRESS = Convert.ToString(dr["EM_ADDRESS"]),
                    EM_PHONE = Convert.ToString(dr["EM_PHONE"]),
                    EM_GENDER = Convert.ToString(dr["EM_GENDER"]),
                    EM_DOB = Convert.ToDateTime(dr["EM_DOB"]),
                    EM_DOJ = Convert.ToDateTime(dr["EM_DOJ"]),
                    EM_PHOTO = Convert.ToString(dr["EM_PHOTO"]),
                    EM_COUNTRY = Convert.ToString(dr["EM_COUNTRY"]),
                    EM_ACTIVE = Convert.ToBoolean(dr["EM_ACTIVE"]),
                });

            }

            //var Smd = DT;

            return View(Smd);
            //return View();
        }

        public ActionResult UploadData()
        {
            return View();
        }
        string COMPCD = "001";

        public ActionResult EmployeeSave(string Code = "")
        {
            return View();
        }

        public ActionResult MasterEmployeeDetails(string Code = "")
        {
            OVODEntities5 db = new OVODEntities5();
            List<EMPLOYEE> EmpDetals = db.EMPLOYEEs.ToList();
            return View(EmpDetals);
        }

        public ActionResult MastEmpDetSave(string Code, string name, EMPSAL[] salary)
        {
            int EmpId;
            OVODEntities5 db = new OVODEntities5();
            string result = "Error! Salary Creation Is Not Completed.";
            if (Code != null || name != null || salary != null)
            {
                //EMPLOYEE model = new EMPLOYEE();
                //model.EM_CODE = Code;
                //model.EM_NAME = name;
                //model.EM_DOJ = DateTime.Now;
                //db.EMPLOYEEs.Add(model);

                EmpId = (db.EMPLOYEEs.SingleOrDefault(EMPLOYEEs => EMPLOYEEs.EM_CODE == Code).EM_ID);
                if (EmpId > 0)
                {
                    foreach (var item in salary)
                    {
                        EMPSAL s = new EMPSAL();
                        s.EM_ID = EmpId;
                        s.EM_CODE = Code;
                        s.TT_ID = item.TT_ID;
                        s.SL_AMT = item.SL_AMT;
                        s.SL_TYPE = item.SL_TYPE;
                        db.EMPSALs.Add(s);
                    }
                    db.SaveChanges();
                    result = "Success! Salary Creation Completed.";
                }
                else
                {
                    result = "Invalid Code!";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EmployeeSave(UserClass uc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //uc.EM_DOB =Convert.ToDateTime( "1990/01/01");
                //uc.EM_DOJ =Convert.ToDateTime( "2019/01/01");
                db.con.Open();
                SqlCommand cmd = new SqlCommand("EMPLOYEE_SAVE", db.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@COMPCD", COMPCD.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_CODE", uc.EM_CODE.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_NAME", uc.EM_NAME.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_LASTNAME", uc.EM_LASTNAME.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_ADDRESS", uc.EM_ADDRESS.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_PHONE", uc.EM_PHONE.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_GEN", uc.EM_GEN.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_DOB", Convert.ToDateTime(uc.EM_DOB)));
                cmd.Parameters.Add(new SqlParameter("@EM_DOJ", Convert.ToDateTime(uc.EM_DOJ)));

                cmd.Parameters.Add(new SqlParameter("@EM_COUNTRY", uc.EM_COUNTRY.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_ACTIVE", uc.EM_ACTIVE.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_DEPT", uc.EM_DEPT.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_DESG", uc.EM_DESG.ToString()));

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string imgpath = Path.Combine(Server.MapPath("~/Files/Images/"), uc.EM_CODE + fileName);
                    file.SaveAs(imgpath);
                    uc.EM_PHOTO = uc.EM_CODE + file.FileName;
                }
                else
                {
                    uc.EM_PHOTO = "";
                }


                cmd.Parameters.Add(new SqlParameter("@EM_PHOTO", uc.EM_PHOTO.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_MAIL", uc.EM_MAIL.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_USERNAME", uc.EM_USERNAME.ToString()));
                cmd.Parameters.Add(new SqlParameter("@EM_PASSWORD", uc.EM_PASSWORD.ToString()));
                cmd.ExecuteNonQuery();
                db.con.Close();
                //ViewData["Message"] = "Employee " + uc.EM_CODE + " Saved Successfully !";
                ViewBag.Message = "Employee " + uc.EM_CODE + " Saved Successfully !";
            }
            return View();

        }

        public List<VW_EMPLOYEE> GetEmployees(string search, string sort, string sortdir, int skip, int pagesize, out int totalRecord)
        {
            using (OVODEntities5 oe = new OVODEntities5())
            {
                var v = (from a in oe.VW_EMPLOYEE
                         where a.EM_NAME.Contains(search) ||
                         a.EM_LASTNAME.Contains(search) ||
                         a.EM_MAIL.Contains(search) ||
                         a.EM_USERNAME.Contains(search) ||
                         a.EM_MAIL.Contains(search) ||
                         a.EM_CODE.Contains(search) ||
                         a.EM_ID.ToString().Contains(search)
                         select a
                         );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pagesize > 0)
                {
                    v = v.Skip(skip).Take(pagesize);
                }
                return v.ToList();
                //var item = oe.VW_EMPLOYEE.ToList();
                //return View(item);
            }
        }

        public ActionResult EmpList(int page = 1, string sort = "EM_NAME", string sortdir = "asc", string search = "")
        {
            int pagesize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pagesize) - pagesize;
            var data = GetEmployees(search, sort, sortdir, skip, pagesize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);

            //using (OVODEntities1 oe = new OVODEntities1())
            //{
            //                    var item = oe.VW_EMPLOYEE.ToList();
            //    return View(item);
            //}
        }

        public JsonResult GetCountries(string term)
        {
            OVODEntities5 db = new OVODEntities5();
            //List<string> COUNTRY;
            return Json(db.VW_COUNTRY1.Where(c => c.CN_NAME.StartsWith(term))
                .Select(a => new { label = a.CN_CD + " / " + a.CN_NAME, id = a.CN_CD }),
                JsonRequestBehavior.AllowGet);
            //COUNTRY = oe.VW_COUNTRY1.Where(x => x.CN_NAME.StartsWith(term))
            //    //.Select(y => y.CN_CD + " " + y.CN_NAME).ToList();
            //    .Select(a => new { label = a.CN_CD + " " + a.CN_NAME, id = a.CN_CD }).ToList();


            //return Json(COUNTRY, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepartments(string term)
        {
            OVODEntities5 db = new OVODEntities5();
            return Json(db.DEPARTMENTs.Where(c => c.DP_NAME.StartsWith(term))
                .Select(a => new { label = a.DP_CD + " / " + a.DP_NAME, id = a.DP_CD }),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDesignations(string term)
        {
            OVODEntities5 db = new OVODEntities5();
            return Json(db.DESIGNATIONs.Where(c => c.DG_DESCRIPTION.StartsWith(term))
                .Select(a => new { label = a.DG_CD + " / " + a.DG_DESCRIPTION, id = a.DG_CD }),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Export_EmployeeLst()
        {
            List<VW_EMPLOYEE> Emp = new List<VW_EMPLOYEE>();
            using (OVODEntities5 oe = new OVODEntities5())
            {
                Emp = oe.VW_EMPLOYEE.ToList();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_EmployeeListing.rpt"));

            DataTable DT = new DataTable();
            DT = new DataTable();
            DT = db.SqlDatatbl("SELECT * FROM VW_EMPLOYEE");
            rd.SetDataSource(DT);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "rpt_EmployeeListing.pdf");
        }

        public ActionResult MulitpleData()
        {
            OVODEntities5 obj = new OVODEntities5();
            var myModel = new MultipleData();
            myModel.TTMaster = obj.TTMASTs.ToList();
            myModel.EmployeeSalary = obj.EMPLOYEESALs.ToList();
            return View(myModel);
        }

        public void ExportToExcel()
        {
            OVODEntities5 db = new OVODEntities5();
            List<EmployeeModel> employees = db.VW_EMPLOYEE.Select(x => new EmployeeModel
            {
                EM_ID = x.EM_ID,
                EM_CODE = x.EM_CODE,
                EM_NAME = x.EM_NAME,
                EM_PHONE = x.EM_PHONE,
                EM_MAIL = x.EM_MAIL,
                EM_DOB = x.EM_DOB,
                EM_DOJ = x.EM_DOJ,
                EM_DOBC = x.EM_DOBC,
                EM_DOJC = x.EM_DOJC
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "Employee Details";

            ws.Cells["A1"].Value = "Employee ID";
            ws.Cells["A2"].Value = "Employee Code";
            ws.Cells["A3"].Value = "Employee Name";
            ws.Cells["A4"].Value = "Employee Phone";
            ws.Cells["A5"].Value = "Employee Email";
            ws.Cells["A6"].Value = "Employee DoB";
            ws.Cells["A7"].Value = "Employee DoJ";

            int RowStart = 3;
            foreach (var item in employees)
            {
                ws.Cells[string.Format("A{0}", RowStart)].Value = item.EM_ID;
                ws.Cells[string.Format("B{0}", RowStart)].Value = item.EM_CODE;
                ws.Cells[string.Format("C{0}", RowStart)].Value = item.EM_NAME;
                ws.Cells[string.Format("D{0}", RowStart)].Value = item.EM_PHONE;
                ws.Cells[string.Format("E{0}", RowStart)].Value = item.EM_MAIL;
                ws.Cells[string.Format("F{0}", RowStart)].Value = (item.EM_DOBC);
                ws.Cells[string.Format("G{0}", RowStart)].Value = item.EM_DOJC;
                RowStart += 1;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            HttpContext.Response.AddHeader("", "");
            HttpContext.Response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
            HttpContext.Response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
            HttpContext.Response.AddHeader("content-disposition", "attachment;  filename=Report.xlsx");
            HttpContext.Response.ContentType = "application/text";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            HttpContext.Response.BinaryWrite(pck.GetAsByteArray());
            HttpContext.Response.End();
        }

        public ActionResult GetEmployeeLstJQuery()
        {
            return View();
        }

        public ActionResult EmpJqList() {
            List<VW_EMPLOYEE> Emp = new List<VW_EMPLOYEE>();
            using (OVODEntities5 oe = new OVODEntities5())
            {
                Emp = oe.VW_EMPLOYEE.ToList();
                return Json(new { data = Emp }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EmpDashBoard() {
            return View();
        }

        public ActionResult jsDashBoard() {
            List<int> repartitions = new List<int>();
            OVODEntities5 oe = new OVODEntities5();
            var EmpLst = oe.VW_EMPLOYEE.ToList();
            var Country = EmpLst.Select(x => x.EM_COUNTRY).Distinct();
            foreach (var item in Country) {
                repartitions.Add(EmpLst.Count(x => x.EM_COUNTRY == item));
            }
            var rep = repartitions;
            ViewBag.Cnt = Country;
            ViewBag.rep = repartitions.ToList();
            return View();
        }

        public ActionResult Designation()
        {
            IEnumerable<EmpDesignation> EmpDesg;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Designations").Result;
            EmpDesg = response.Content.ReadAsAsync<IEnumerable<EmpDesignation>>().Result;
            return View(EmpDesg);
        }

        public ActionResult Doc_Emp_details(string SearchBy, string search,int?Page)
        {
            //Doc_Emp_DetailsVM model = new Doc_Emp_DetailsVM();
            //model.Doc_History = _context.Doc_History.ToList();
            //return View(model);
            if (SearchBy == "EmpId")
            {
                Doc_Emp_DetailsVM model = new Doc_Emp_DetailsVM
                {
                    Doc_History = _context.Doc_History
                    .Where(z => z.emp_Id.ToString()
                    .Contains(search) || search == null)
                    .ToList().ToPagedList(Page ?? 1, 3)
                };
                return View(model);
            }
            else
            {
                Doc_Emp_DetailsVM model = new Doc_Emp_DetailsVM
                {
                    Doc_History = _context.Doc_History
                    .Where(z => z.doc_no
                    .Contains(search) || search == null)
                    .ToList().ToPagedList(Page ?? 1, 3)
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Doc_Emp_details(Doc_Emp_DetailsVM model, HttpPostedFileBase doc_file)
        {
            var updDate = DateTime.Now;
            var _context = new OVODEntities5();
            if (model.emp_id > 0)
            {
                Emp_Doc_History emp = new Emp_Doc_History()
                {
                    emp_id = model.emp_id,
                    doc_id = model.doc_id,
                    doc_no = model.doc_no,
                    doc_issue_date = model.doc_issue_date,
                    doc_expiry_date = model.doc_expiry_date,
                    update_date = updDate
                };

                Doc_History doc = new Doc_History()
                {
                    emp_Id = model.emp_id,
                    doc_id = model.doc_id,
                    update_date = updDate,
                    doc_no = model.doc_no,
                    doc_issue_date = model.doc_issue_date,
                    doc_expiry_date = model.doc_expiry_date
                };
                if (doc_file != null)
                {
                    emp.dof_file = new byte[doc_file.ContentLength];
                    doc_file.InputStream.Read(emp.dof_file, 0, doc_file.ContentLength);
                    doc.dof_file = emp.dof_file;
                }
                _context.Emp_Doc_History.Add(emp);
                _context.Doc_History.Add(doc);
                _context.SaveChanges();
                ViewBag.message = "Record Saved Successfully";
            }

            //return View("Doc_Emp_details");
            return RedirectToAction("Doc_Emp_details", "Home");
        }

        //public ActionResult EmpDocSearch(string SearchBy, string search)
        //{
        //    if (SearchBy == "EmpId")
        //    {
        //        Doc_Emp_DetailsVM model = new Doc_Emp_DetailsVM();
        //        model.Doc_History = _context.Doc_History
        //            .Where(z => z.emp_Id.ToString().Contains(search))
        //            .ToList();
        //        return View(model);
        //    }
        //    else {
        //        Doc_Emp_DetailsVM model = new Doc_Emp_DetailsVM();
        //        model.Doc_History = _context.Doc_History
        //            .Where(z=>z.doc_no.Contains(search))
        //            .ToList();
        //        return View(model);
        //    }
        //}

        [HttpPost]
        public JsonResult AutoCompleteEmployee(string prefix)
        {
            if (prefix == null)
                prefix = "";

            prefix = prefix.ToLower();
            var employees = (from employee in _context.ANG_EMPLOYEE
                             where (employee.name.ToString().ToLower().Contains(prefix) || employee.id.ToString().Contains(prefix))
                             select new
                             {
                                 label = employee.id.ToString() + " / " + employee.name,
                                 val = employee.id.ToString()
                             }).ToList();

            return Json(employees);
        }

        public JsonResult Ang_Employeedetailasjson(int id)
        {
            return Json(_context.ANG_EMPLOYEE.Where(e => e.id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

    }
}