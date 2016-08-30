using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
    public partial class ProjectRoundShow : System.Web.UI.Page
    {
    	ProjectService service = new ProjectService();
    	protected ProjectRound projectRound;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["ManagerID"] == null, "default.aspx");
        	Show(ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]));
        }
        
        public void Show(int projectRoundID)
        {
        	projectRound = service.ReadProjectRound(projectRoundID, ConvertHelper.ToInt32(Session["ManagerID"]));
        }
    }
}