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
    public partial class ManagerEdit : System.Web.UI.Page
    {
        protected Manager manager;
        ManagerService s = new ManagerService();

        protected void Page_Load(object sender, EventArgs e)
        {
        	manager = s.ReadManager(ConvertHelper.ToInt32(Request.QueryString["ManagerID"]));
        	if (manager != null) {
        		textBoxName.Text = manager.Name;
        		textBoxEmail.Text = manager.Email;
        		textBoxPhone.Text = manager.Phone;
        		foreach (var mpr in manager.ProjectRounds) {
        			dropDownListProjectRounds.Items.Add(mpr.ProjectRound.ToString());
        		}
        	}
        }
    }
}