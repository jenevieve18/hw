﻿using System;
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
    	ProjectService s = new ProjectService();
    	protected ProjectRound projectRound;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	Show(ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]));
        }
        
        public void Show(int projectRoundID)
        {
        	projectRound = s.ReadProjectRound(projectRoundID);
        }
    }
}