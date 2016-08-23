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
    public partial class ProjectRoundUnitShow : System.Web.UI.Page
    {
    	protected ProjectRoundUnit projectRoundUnit;
    	ProjectService service = new ProjectService();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	Show(ConvertHelper.ToInt32(Request.QueryString["ProjectRoundUnitID"]));
        }
        
        public void Show(int projectRoundUnitID)
        {
        	projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
        }
    }
}