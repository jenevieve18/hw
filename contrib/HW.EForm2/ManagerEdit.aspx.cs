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
        ManagerService managerService = new ManagerService();
        ProjectService projectService = new ProjectService();

        protected void Page_Load(object sender, EventArgs e)
        {
        	manager = managerService.ReadManager(ConvertHelper.ToInt32(Request.QueryString["ManagerID"]));
            if (!IsPostBack)
            {
                if (manager != null)
                {
                    textBoxName.Text = manager.Name;
                    textBoxEmail.Text = manager.Email;
                    textBoxPhone.Text = manager.Phone;
                    foreach (var pr in projectService.FindAllProjectRounds())
                    {
                        dropDownListProjectRounds.Items.Add(new ListItem(pr.ToString(), pr.ProjectRoundID.ToString()));
                    }

                }
            }
        }

        protected void dropDownListProjectRounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var pru in projectService.FindProjectRoundUnitsByProjectRound(ConvertHelper.ToInt32(dropDownListProjectRounds.SelectedValue)))
            {
                checkBoxListUnits.Items.Add(new ListItem(pru.ProjectRound.Internal + " >> " + pru.Unit, pru.ProjectRoundUnitID.ToString()));
            }
        }
    }
}