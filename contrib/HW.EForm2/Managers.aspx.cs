using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class Managers : System.Web.UI.Page
    {
    	protected IList<Manager> managers;
    	ManagerService s = new ManagerService();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	managers = s.FindAllManagers();
        }
    }
}