using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class ReportEdit : System.Web.UI.Page
    {
        ReportService s = new ReportService();
        protected Report report;

        protected void Page_Load(object sender, EventArgs e)
        {
            report = s.ReadReport(ConvertHelper.ToInt32(Request.QueryString["ReportID"]));
            if (report != null)
            {
                textBoxInternal.Text = report.Internal;
            }
        }
    }
}