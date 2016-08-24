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
    public partial class Profile : System.Web.UI.Page
    {
    	protected Manager manager;
    	ManagerService s = new ManagerService();
        int managerID;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	//HtmlHelper.RedirectIf(Session["ManagerID"] == null, "default.aspx");
            managerID = ConvertHelper.ToInt32(Session["ManagerID"]);
            manager = s.ReadManager(managerID);
            if (!IsPostBack)
            {
                if (manager != null)
                {
                    textBoxUsername.Text = manager.Name;
                    textBoxEmail.Text = manager.Email;
                    textBoxPhone.Text = manager.Phone;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var m = new Manager {
        		Name = textBoxUsername.Text,
        		Email = textBoxEmail.Text,
        		Phone = textBoxPhone.Text,
                Password = textBoxPassword.Text == "" ? manager.Password : textBoxPassword.Text
        	};
        	s.UpdateManager(m, managerID);
        	Response.Redirect("dashboard.aspx");
        }
    }
}