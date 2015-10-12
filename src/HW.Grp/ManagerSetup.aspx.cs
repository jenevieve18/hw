using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class ManagerSetup : System.Web.UI.Page
	{
		protected IList<SponsorAdminDepartment> sponsorAdminDepartments;
		protected string errorMessage = string.Empty;
        protected string message = "";
		int sponsorAdminID = 0;
		int sponsorID = 0;
		protected int lid;

		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		SqlManagerFunctionRepository managerRepository = new SqlManagerFunctionRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlSponsorAdminRepository sponsorAdminRepository = new SqlSponsorAdminRepository();

		bool HasSAID {
			get { return Request.QueryString["SAID"] != null; }
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Cancel.Text = R.Str(lid, "cancel", "Cancel");
			Save.Text = R.Str(lid, "save", "Save");
			buttonSend.Text = R.Str(lid, "send.credentials", "Send credentials");
			SuperUser.Text = R.Str(lid, "role.user.super", "Super user (can administer its own manager account, including all units)");
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
            sponsorID = Convert.ToInt32(Session["SponsorID"]);
            lid = ConvertHelper.ToInt32(Session["lid"], 2);

			if (sponsorID != 0) {
				Save.Click += new EventHandler(Save_Click);
				Cancel.Click += new EventHandler(Cancel_Click);

				if (!IsPostBack) {
					foreach (var f in managerRepository.FindAll(lid)) {
						ManagerFunctionID.Items.Add(new ListItem(f.Function + " (" + f.Expl + ")", f.Id.ToString()));
					}
					sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
					
					HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Managers), "default.aspx", true);
					
					var a = sponsorAdminRepository.ReadSponsor(sponsorAdminID);
					if (a != null && !a.SuperUser) {
						SuperUser.Visible = false;
					}

					OrgTree.Text = string.Format("<tr><td>{0}</td></tr>", Session["Sponsor"]);
					Dictionary<int, bool> DX = new Dictionary<int, bool>();
					sponsorAdminDepartments = departmentRepository.a(sponsorID, sponsorAdminID);
					foreach (var d in sponsorAdminDepartments) {
						int depth = d.Department.Depth;
						DX[depth] = (d.Department.Siblings > 0);

						OrgTree.Text += @"
<tr>
	<td>
		<table border='0' cellspacing='0' cellpadding='0'>
			<tr>
				<td>";
						for (int i = 1; i <= depth; i++) {
							if (!DX.ContainsKey(i)) {
								DX[i] = false;
							}
							if (i == depth) {
								OrgTree.Text += string.Format("<img src='img/{0}.gif' width='19' height='20'/>", (DX[i] ? "T" : "L"));
							} else {
								OrgTree.Text += string.Format("<img src='img/{0}.gif' width='19' height='20'/>", (DX[i] ? "I" : "null"));
							}
						}
						OrgTree.Text += string.Format(
							@"
				</td>
				<td valign='middle'>
					<input{0} value='{1}' name='DepartmentID' type='checkbox'/>&nbsp;{2}&nbsp;&nbsp;&nbsp;
				</td>
			</tr>
		</table>
	</td>
	<td style='font-size:9px;'>&nbsp;{3}</td>
</tr>",
							(!d.Admin.SuperUser ? " disabled" : ""),
							d.Department.Id,
							d.Department.Name,
							d.Department.ShortName
						);
					}
				} else {
					int tempSponsorAdminID = sponsorRepository.SponsorAdminExists(Convert.ToInt32(Session["SponsorAdminID"]), Usr.Text);
					//if (tempSponsorAdminID >= 0) {
						foreach (var d in departmentRepository.a(sponsorID, tempSponsorAdminID)) {
							bool hasDepartmentID = Request.Form["DepartmentID"] != null;
							if (hasDepartmentID) {
								string departmentID = Request.Form["DepartmentID"];
								string ids = string.Format("#{0}#", departmentID.Replace(" ", "").Replace(",", "#"));
								bool deptIDInIds = ids.IndexOf("#" + d.Department.Id + "#") >= 0;
								if (deptIDInIds) {
									OrgTree.Text = OrgTree.Text.Replace("value='" + d.Department.Id + "'", "value='" + d.Department.Id + "' checked");
								}
							}
						}
					//}
				}

				if (!HasSAID) {
                    panelUserName.Visible = false;
                } else {
					buttonSend.Visible = true;
					
					int SAID = Convert.ToInt32(Request.QueryString["SAID"]);
					sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, SAID);
					if (a != null) {
						sponsorAdminID = a.Id;
						if (!IsPostBack) {
							ReadOnly.Checked = a.ReadOnly;
							SuperUser.Checked = a.SuperUser;
                            Name.Text = (a.Name == "" ? a.Usr : a.Name);
                            Usr.Text = a.Usr;
                            if (a.Usr == null || a.Usr == "")
                            {
                                panelUserName.Visible = false;
                            }
							Email.Text = a.Email;
							LastName.Text = a.LastName;
							PermanentlyDeleteUsers.Checked = a.PermanentlyDeleteUsers;

							foreach (var f in sponsorRepository.FindAdminFunctionBySponsorAdmin(a.Id)) {
								if (ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()) != null) {
									ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()).Selected = true;
								}
							}
							foreach (var d in sponsorRepository.FindAdminDepartmentBySponsorAdmin(a.Id)) {
								OrgTree.Text = OrgTree.Text.Replace("value='" + d.Department.Id + "'", "value='" + d.Department.Id + "' checked");
							}
						} else {
							foreach (var d in departmentRepository.b(sponsorID, sponsorAdminID)) {
								bool hasDepartmentID = Request.Form["DepartmentID"] != null;
								if (hasDepartmentID) {
									string departmentID = Request.Form["DepartmentID"];
									string ids = string.Format("#{0}#", departmentID.Replace(" ", "").Replace(",", "#"));
									bool deptIDInIds = ids.IndexOf("#" + d.Department.Id + "#") >= 0;
									if (deptIDInIds) {
										OrgTree.Text = OrgTree.Text.Replace("value='" + d.Department.Id + "'", "value='" + d.Department.Id + "' checked");
									}
								}
							}
						}
					}
				}
			} else {
				Response.Redirect("default.aspx", true);
			}
		}

		void Cancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("managers.aspx", true);
		}

		void Save_Click(object sender, EventArgs e)
		{
			int tempSponsorAdminID = sponsorRepository.SponsorAdminExists2(sponsorAdminID, Usr.Text);
			if (tempSponsorAdminID <= 0) {
				var a = new SponsorAdmin {
					Id = sponsorAdminID,
					ReadOnly = ReadOnly.Checked,
					Email = Email.Text,
					Name = Name.Text,
					Usr = Usr.Text,
					SuperUser = SuperUser.Checked,
					Sponsor = new Sponsor { Id = sponsorID },
					LastName = LastName.Text,
					PermanentlyDeleteUsers = PermanentlyDeleteUsers.Checked
				};
				a.Validate();
                a.AddErrorIf(a.Name == "", R.Str(lid, "manager.name.required", "Sponsor admin name is required."));
                a.AddErrorIf(a.Email == "", R.Str(lid, "manager.email.required", "Email address name is required."));
                a.AddErrorIf(sponsorAdminRepository.SponsorAdminEmailExists(a.Email, sponsorAdminID), R.Str(lid, "manager.email.notunique", "Please choose a unique email address!"));
				if (!a.HasErrors) {
					if (sponsorAdminID != 0) {
						sponsorRepository.UpdateSponsorAdmin(a);
					} else {
						sponsorRepository.SaveSponsorAdmin(a);
						a = sponsorRepository.ReadSponsorAdmin(sponsorID, Usr.Text, Email.Text);
						if (a != null) {
							sponsorAdminID = a.Id;
						}
					}
					sponsorRepository.DeleteSponsorAdminFunction(sponsorAdminID);
					foreach (var f in managerRepository.FindAll()) {
						if (ManagerFunctionID.Items.FindByValue(f.Id.ToString()) != null) {
							if (ManagerFunctionID.Items.FindByValue(f.Id.ToString()).Selected) {
								var x = new SponsorAdminFunction {
									Admin = new SponsorAdmin { Id = sponsorAdminID },
									Function = new ManagerFunction { Id = f.Id }
								};
								sponsorRepository.SaveSponsorAdminFunction(x);
							}
						}
					}
					foreach (var d in departmentRepository.b(sponsorID, sponsorAdminID)) {
						departmentRepository.DeleteSponsorAdminDepartment(sponsorAdminID, d.Department.Id);
						bool hasDepartmentID = Request.Form["DepartmentID"] != null;
						if (hasDepartmentID) {
							string departmentID = Request.Form["DepartmentID"];
							string ids = string.Format("#{0}#", departmentID.Replace(" ", "").Replace(",", "#"));
							bool deptIDInIds = ids.IndexOf("#" + d.Department.Id + "#") >= 0;
							if (deptIDInIds) {
								var x = new SponsorAdminDepartment {
									Id = sponsorAdminID,
									Department = new Department { Id = d.Department.Id }
								};
								departmentRepository.SaveSponsorAdminDepartment(x);
							}
						}
					}
                    Response.Redirect(string.Format("managersetup.aspx?SAID={0}", sponsorAdminID));
				} else {
					errorMessage = a.Errors.ToHtmlUl();
				}
			} else {
				errorMessage = R.Str(lid, "manager.invalid", "Error! The username is invalid, please select a different one!");
			}
		}

        protected void buttonSend_Click(object sender, EventArgs e)
        {
        	string uid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        	sponsorAdminRepository.UpdateUniqueKey(uid, sponsorAdminID);
        	new PasswordActivationLink().Send(Email.Text, uid, Name.Text, Usr.Text);
        	
            message = R.Str(lid, "password.activate.sent", "Password activation link sent!");
        }
	}
}