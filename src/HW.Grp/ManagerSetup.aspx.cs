using System;
using System.Collections.Generic;
using System.Configuration;
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

		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
		int sponsorAdminID = 0;
		int sponsorID = 0;
		
		string sponsorAdminEmail = "";
		
		SqlUserRepository userRepo = new SqlUserRepository();

		SqlDepartmentRepository departmentRepo = new SqlDepartmentRepository();
		SqlManagerFunctionRepository managerRepo = new SqlManagerFunctionRepository();
		SqlSponsorRepository sponsorRepo = new SqlSponsorRepository();
		SqlSponsorAdminRepository sponsorAdminRepo = new SqlSponsorAdminRepository();
		

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

			var userSession = userRepo.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}

			if (sponsorID != 0) {
				Save.Click += new EventHandler(Save_Click);
				Cancel.Click += new EventHandler(Cancel_Click);

				if (!IsPostBack) {
					foreach (var f in managerRepo.FindAll(lid)) {
						ManagerFunctionID.Items.Add(new ListItem(f.Function + " (" + f.Expl + ")", f.Id.ToString()));
					}
					sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
					
					HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Managers), "default.aspx", true);
					
					var a = sponsorAdminRepo.ReadSponsor(sponsorAdminID);
					if (a != null && !a.SuperUser) {
						SuperUser.Visible = false;
					}

					OrgTree.Text = string.Format("<tr><td>{0}</td></tr>", Session["Sponsor"]);
					Dictionary<int, bool> DX = new Dictionary<int, bool>();
					sponsorAdminDepartments = departmentRepo.a(sponsorID, sponsorAdminID);
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
								OrgTree.Text += string.Format("<img src='assets/img/{0}.gif' width='19' height='20'/>", (DX[i] ? "T" : "L"));
							} else {
								OrgTree.Text += string.Format("<img src='assets/img/{0}.gif' width='19' height='20'/>", (DX[i] ? "I" : "null"));
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
					int tempSponsorAdminID = sponsorRepo.SponsorAdminExists(Convert.ToInt32(Session["SponsorAdminID"]), Usr.Text);
					//if (tempSponsorAdminID >= 0) {
					foreach (var d in departmentRepo.a(sponsorID, tempSponsorAdminID)) {
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
					var a = sponsorRepo.ReadSponsorAdmin(sponsorID, sponsorAdminID, SAID);
					if (a != null) {
                        sponsorAdminID = a.Id;

                        sponsorAdminEmail = a.Email;

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

							foreach (var f in sponsorRepo.FindAdminFunctionBySponsorAdmin(a.Id)) {
								if (ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()) != null) {
									ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()).Selected = true;
								}
							}
							foreach (var d in sponsorRepo.FindAdminDepartmentBySponsorAdmin(a.Id)) {
								OrgTree.Text = OrgTree.Text.Replace("value='" + d.Department.Id + "'", "value='" + d.Department.Id + "' checked");
							}
						} else {
							foreach (var d in departmentRepo.b(sponsorID, sponsorAdminID)) {
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
		
		bool SponsorAdminEmailHasDuplicate(string email, string previousEmail, int sponsorAdminID)
		{
//			return sponsorAdminRepo.SponsorAdminEmailExists(email, sponsorAdminID);
			
			if (email == previousEmail) {
				return false;
			} else {
				var sponsorAdminEmailExists = sponsorAdminRepo.SponsorAdminEmailExists(email, sponsorAdminID);
				return sponsorAdminEmailExists;
			}
		}

		void Save_Click(object sender, EventArgs e)
		{
			int tempSponsorAdminID = sponsorRepo.SponsorAdminExists2(sponsorAdminID, Usr.Text);
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
				a.AddErrorIf(SponsorAdminEmailHasDuplicate(a.Email, sponsorAdminEmail, sponsorAdminID), R.Str(lid, "manager.email.notunique", "Please choose a unique email address!"));
				if (!a.HasErrors) {
					if (sponsorAdminID != 0) {
						sponsorRepo.UpdateSponsorAdmin(a);
					} else {
						sponsorRepo.SaveSponsorAdmin(a);
						a = sponsorRepo.ReadSponsorAdmin(sponsorID, Usr.Text, Email.Text);
						if (a != null) {
							sponsorAdminID = a.Id;
						}
					}
					sponsorRepo.DeleteSponsorAdminFunction(sponsorAdminID);
					foreach (var f in managerRepo.FindAll()) {
						if (ManagerFunctionID.Items.FindByValue(f.Id.ToString()) != null) {
							if (ManagerFunctionID.Items.FindByValue(f.Id.ToString()).Selected) {
								var x = new SponsorAdminFunction {
									Admin = new SponsorAdmin { Id = sponsorAdminID },
									Function = new ManagerFunction { Id = f.Id }
								};
								sponsorRepo.SaveSponsorAdminFunction(x);
							}
						}
					}
					foreach (var d in departmentRepo.b(sponsorID, sponsorAdminID)) {
						departmentRepo.DeleteSponsorAdminDepartment(sponsorAdminID, d.Department.Id);
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
								departmentRepo.SaveSponsorAdminDepartment(x);
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
			sponsorAdminRepo.UpdateUniqueKey(uid, sponsorAdminID);
			
//			new PasswordActivationLink().Send(Email.Text, uid, Name.Text, Usr.Text);
			string subject = R.Str(lid, "password.activate.subject", "Your HealthWatch Group Administration account");
			string body = R.Str(lid, "password.activate.body", @"Dear <NAME>,

A manager account has been set up for you to the HealthWatch group administration interface. Please click the link below to choose a password.

<LINK>PasswordActivation.aspx?KEY=<KEY>

<USERNAME>");
			
			body = body.Replace("<NAME>", Name.Text);
			body = body.Replace("<LINK>", ConfigurationManager.AppSettings["grpURL"]);
			body = body.Replace("<KEY>", uid);
			string username = Usr.Text != "" ? R.Str(lid, "password.activate.username", "Your username is ") + Usr.Text : "";
			body = body.Replace("<USERNAME>", username);
			
			new PasswordActivationLink().Send(Email.Text, uid, Name.Text, Usr.Text, subject, body);
			
			message = R.Str(lid, "password.activate.sent", "Password activation link sent!");
		}
	}
}