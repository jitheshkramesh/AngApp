using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


    public partial class ItemPartial
    {
        public HttpPostedFileBase Upload_Image { get; set; }

        [Display(Name = "Code :")]
        public string Item_Code { get; set; }
        [Display(Name = "Description :")]
        public string Item_Desc { get; set; }
        [Display(Name = "Price :")]
        public Nullable<decimal> Item_Price { get; set; }
        [Display(Name = "Category :")]
        public string Item_Category { get; set; }

        [Display(Name = "Quantity :")]
        public Nullable<decimal> Item_Qty { get; set; }

        [Display(Name = "Manu.Date :")]
        public Nullable<System.DateTime> Item_Date { get; set; }

        [Display(Name = "Expiry Date :")]
        public Nullable<System.DateTime> Item_Expiry { get; set; }

        [Display(Name = "Image Path :")]
        public string Item_Image { get; set; }

        [Display(Name = "Inactive :")]
        public Nullable<bool> Item_Inactive { get; set; }
        public ItemPartial()
        {
            string Item_Image = "~/Files/Items/default.jpg";
        }
    }

namespace AngApp
{
    public partial class Item
    {
        public HttpPostedFileBase Upload_Image { get; internal set; }
    }
}