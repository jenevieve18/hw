using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.ReportGenerator
{
    public partial class Projects : System.Web.UI.Page
    {
        protected IList<Project> projects;
        ProjectService service;
        
        public Projects() : this(new ProjectService(new SqlProjectRepository()))
        {
        }
        
        public Projects(ProjectService service)
        {
        	this.service = service;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
        	SetProjects(service.FindAllProjects());
        }
        
        public void SetProjects(IList<Project> projects)
        {
            this.projects = projects;
        }
    }
}