using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AngApp.Models
{
    public class EmpDesignation
    {
        public int DG_ID { get; set; }
        [Required(ErrorMessage = "Code Required.")]
        [Display(Name = "Code :")]
        [StringLength(maximumLength: 8, MinimumLength = 1, ErrorMessage = "Code Must Be Max 8 & Min 1")]
        public string DG_CD { get; set; }
        [Required(ErrorMessage = "Description Required.")]
        [Display(Name = "Description :")]
        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "Description Must Be Max 150 & Min 1")]
        public string DG_DESCRIPTION { get; set; }
    }
}