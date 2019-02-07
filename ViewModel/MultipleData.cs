using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngApp.Models;

namespace AngApp.ViewModel
{
    public class MultipleData
    {
        public IEnumerable<TTMAST> TTMaster { get; set; }
        public IEnumerable<EMPLOYEESAL> EmployeeSalary { get; set; }
        public IEnumerable<VW_EMPLOYEE> Employee { get; set; }
    }

    public class EmpDetailsViewModel
    {
        public IEnumerable<VW_EMPLOYEE> Employee { get; set; }

        public IEnumerable<EMPLOYEESAL> Salary { get; set; }
    }
}