using AngApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngApp.ViewModel
{
    public class Doc_HistoryVM
    {
        public int Id { get; set; }
        public int Doc_Id { get; set; }
        public string Doc_no { get; set; }
        public DateTime Update_date { get; set; }
        public DateTime Doc_Issue_Date { get; set; }
        public DateTime Doc_Expiry_Date { get; set; }
        public int Emp_id { get; set; }
        public IEnumerable<TTMAST> TTMASTs { get; set; }
        public IEnumerable<ANG_EMPLOYEE> ANG_EMPLOYEEs { get; set; }
        public byte[] Doc_File { get; set; }
    }
}