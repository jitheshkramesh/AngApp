using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace AngApp.Reports
{
    public partial class ReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ReportDocument report = new ReportDocument();
            //crViewer.ToolPanereportlView = CrystalDecisions.Web.ToolPanelViewType.None;
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("~/Reports/rpt_EmployeeListing.rpt"));
            report.SetDataSource((DataTable)Session["ReportSource"]);
            crViewer.ReportSource = report;
            crViewer.DataBind();
        }
    }
}