using AngApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngApp.ViewModel
{
    public class Doc_Emp_DetailsVM
    {
        public int id { get; set; }

        [Display(Name ="Employee Id")]
        public int emp_id { get; set; }

        [Display(Name = "Employee Name")]
        public string emp_name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Doc. Id")]
        public int doc_id { get; set; }

        [Display(Name = "Doc. Desc.")]
        public string doc_desc { get; set; }

        [DataType(DataType.Date)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Update Date")]
        public DateTime update_date { get; set; }

        public HttpPostedFileBase  doc_file { get; set; }
        [Display(Name = "Image")]
        public string doc_no { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Issue Date")]
        public DateTime doc_issue_date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime doc_expiry_date { get; set; }

        public IEnumerable<Doc_History> Doc_History { get; set; }
        public IEnumerable<TTMAST> tTMASTs { get; set; }
        public IEnumerable<ANG_EMPLOYEE> aNG_EMPLOYEEs { get; set; }
    }
}