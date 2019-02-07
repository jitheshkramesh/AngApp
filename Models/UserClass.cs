using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AngApp.Models
{
    public class UserClass
    {
        [Required(ErrorMessage = "Enter Employee Code !")]
        [Display(Name = "Employee Code :")]
        [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "Employee Code Must Be Max 10 & Min 3")]
        public string EM_CODE { get; set; }

        [Required(ErrorMessage = "Enter Employee Name !")]
        [Display(Name = "Employee Name :")]
        [StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "Employee Name Must Be Max 100 & Min 3")]
        public string EM_NAME { get; set; }

        [Required(ErrorMessage = "Enter Employee Mail ID !")]
        [Display(Name = "Mail ID :")]
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "Employee Mail ID Must Be Max 150 & Min 5")]
        public string EM_MAIL { get; set; }

        [Required(ErrorMessage = "Enter Employee Phone No. !")]
        [Display(Name = "Phone No. :")]
        [StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "Employee Phone Must Be Max 50 & Min 8")]
        public string EM_PHONE { get; set; }

        [Required(ErrorMessage = "Enter Department !")]
        [Display(Name = "Department :")]
        [StringLength(maximumLength: 4, MinimumLength = 1, ErrorMessage = "Department Must Be Max 4 & Min 1")]
        public string EM_DEPT { get; set; }

        [Required(ErrorMessage = "Enter Designation !")]
        [Display(Name = "Designation :")]
        [StringLength(maximumLength: 8, MinimumLength = 1, ErrorMessage = "Designation Must Be Max 8 & Min 1")]
        public string EM_DESG { get; set; }

        [Required(ErrorMessage = "Enter Employee Date of Birth !")]
        [Display(Name = "Date of Birth :")]
        //[StringLength(maximumLength: 10, MinimumLength = 8, ErrorMessage = "Date of Birth Must Be Max 10 & Min 8")]
        public DateTime EM_DOB { get; set; }

        [Required(ErrorMessage = "Enter Employee Date of Join !")]
        [Display(Name = "Date of Join :")]
        //[StringLength(maximumLength: 10, MinimumLength = 8, ErrorMessage = "Date of Join Must Be Max 10 & Min 8")]
        public DateTime EM_DOJ { get; set; }

        [Required(ErrorMessage = "Enter Employee User Name !")]
        [Display(Name = "User Name :")]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "User Name Must Be Max 100 & Min 8")]
        public string EM_USERNAME { get; set; }

        [Required(ErrorMessage = "Enter Employee Password !")]
        [Display(Name = "Password :")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Password Must Be Max 100 & Min 8")]
        public string EM_PASSWORD { get; set; }

        [Required(ErrorMessage = "Enter Employee Re-Password !")]
        [Display(Name = "Re-Password :")]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Re-Password Must Be Max 100 & Min 8")]
        [DataType(DataType.Password)]
        [Compare("EM_PASSWORD")]
        public string EM_RE_PASSWORD { get; set; }

        [Required(ErrorMessage = "Enter Gender !")]
        [Display(Name = "Gender :")]
        public EM_GEN EM_GEN { get; set; }

        //[Required(ErrorMessage = "Upload Profile Image !")]
        [Display(Name = "Profile Image :")]
        public string EM_PHOTO { get; set; }

        [Display(Name = "Last Name :")]
        public string EM_LASTNAME { get; set; }

        [Display(Name = "Address :")]
        public string EM_ADDRESS { get; set; }


        [Display(Name = "Country :")]
        public string EM_COUNTRY { get; set; }

        [Display(Name = "Is Active ? :")]
        [ValidateCheckbox(ErrorMessage = "Please select Active")]
        public bool EM_ACTIVE { get; set; }

        [Display(Name = "Salary Information :")]
        public IEnumerable<EmpSal> Salary { get; set; }
    }

    public class ValidateCheckbox : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            return (bool)value;
        }
    }

    public enum EM_GEN
    {
        M, F
    }

    public class EmpSal
    {
        public int SL_ID { get; set; }
        public Nullable<int> EM_ID { get; set; }
        public Nullable<int> TT_ID { get; set; }
        public Nullable<decimal> SL_AMT { get; set; }
        public string SL_TYPE { get; set; }
        public string EM_CODE { get; set; }
        public string EM_NAME { get; set; }
        public string TT_DESC { get; set; }
        public string TT_GROUP { get; set; }

    }

    
}