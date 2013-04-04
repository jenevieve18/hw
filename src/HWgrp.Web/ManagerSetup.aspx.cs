using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HWgrp.Web
{
	public partial class ManagerSetup : System.Web.UI.Page
	{
		int sponsorAdminID = 0;
		int sponsorID = 0;

		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IManagerFunctionRepository managerRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		
		bool HasSAID {
			get { return Request.QueryString["SAID"] != null; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
			sponsorID = Convert.ToInt32(Session["SponsorID"]);
			
			if (sponsorID != 0) {
				buttonSave.Click += new EventHandler(Save_Click);
				buttonCancel.Click += new EventHandler(Cancel_Click);

				if (!IsPostBack) {
					foreach (var f in managerRepository.FindAll()) {
						ManagerFunctionID.Items.Add(new ListItem(f.Function + " (" + f.Expl + ")", f.Id.ToString()));
					}
					var a = sponsorRepository.ReadSponsorAdmin(sponsorAdminID);
					if (a != null && !a.SuperUser) {
						SuperUser.Visible = false;
					}

					OrgTree.Text = "<TR><TD>" + Session["Sponsor"] + "</TD></TR>";
					bool[] DX = new bool[8];
					foreach (var d in departmentRepository.a(sponsorID, sponsorAdminID)) {
						int depth = d.Department.Depth;
						DX[depth] = (d.Department.Siblings > 0);

						OrgTree.Text += "<TR><TD><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>";
						for (int i = 1; i <= depth; i++) {
							if (i == depth) {
								OrgTree.Text += "<IMG SRC=\"img/" + (DX[i] ? "T" : "L") + ".gif\" width=\"19\" height=\"20\"/>";
							} else {
								OrgTree.Text += "<IMG SRC=\"img/" + (DX[i] ? "I" : "null") + ".gif\" width=\"19\" height=\"20\"/>";
							}
						}
						OrgTree.Text += "</TD>" +
							"<TD VALIGN=\"MIDDLE\"><input" + (d.Admin.SuperUser ? " disabled" : "") + " value=\"" + d.Id + "\" name=\"DepartmentID\" type=\"checkbox\"/>&nbsp;" + d.Department.Name + "&nbsp;&nbsp;&nbsp;</TD>" +
							"</TR>" +
							"</TABLE>" +
							"</TD>" +
							"<TD STYLE=\"font-size:9px;\">&nbsp;" + d.Department.ShortName + "</TD>" +
							"</TR>";
					}
				}

				if (HasSAID) {
					int SAID = Convert.ToInt32(Request.QueryString["SAID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, SAID);
					if (a !=  null) {
						sponsorAdminID = a.Id;
						if (!IsPostBack) {
							ReadOnly.Checked = a.ReadOnly;
							SuperUser.Checked = a.SuperUser;
							Name.Text = (a.Name == "" ? a.Usr : a.Name);
							Usr.Text = a.Usr;
							Pas.Attributes.Add("value", "Not shown");
							Email.Text = a.Email;

							foreach (var f in sponsorRepository.FindAdminFunctionBySponsorAdmin(a.Id)) {
								if (ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()) != null) {
									ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()).Selected = true;
								}
							}
							foreach (var d in sponsorRepository.FindAdminDepartmentBySponsorAdmin(a.Id)) {
								OrgTree.Text = OrgTree.Text.Replace("value=\"" + d.Department.Id + "\"", "value=\"" + d.Department.Id + "\" checked");
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
			if (!sponsorRepository.SponsorAdminExists(sponsorAdminID, Usr.Text)) {
				if (sponsorAdminID != 0) {
					var a = new SponsorAdmin {
						ReadOnly = ReadOnly.Checked,
						Email = Email.Text,
						Name = Name.Text,
						Usr = Usr.Text,
						Password = Pas.Text,
						SuperUser = SuperUser.Checked,
						Sponsor = new Sponsor { Id = sponsorID }
					};
					sponsorRepository.UpdateSponsorAdmin(a);
				} else {
					var a = new SponsorAdmin {
						Email = Email.Text,
						Name = Name.Text,
						Usr = Usr.Text,
						Password = Pas.Text,
						Sponsor = new Sponsor { Id = sponsorID },
						SuperUser = SuperUser.Checked,
						ReadOnly = ReadOnly.Checked
					};
					sponsorRepository.SaveSponsorAdmin(a);
					sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
					a = sponsorRepository.ReadSponsorAdmin(sponsorAdminID, Usr.Text);
					if (a != null) {
						sponsorAdminID = a.Id;
					}
				}
				sponsorRepository.DeleteSponsorAdmin(sponsorAdminID);
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
				sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
				foreach (var d in departmentRepository.b(sponsorID, sponsorAdminID)) {
					if (!d.Admin.SuperUser) {
						departmentRepository.DeleteSponsorAdminDepartment(sponsorAdminID, d.Department.Id);
						bool hasDepartmentID = Request.Form["DepartmentID"] != null;
						if (hasDepartmentID) {
							string departmentID = Request.Form["DepartmentID"];
							if (("#" + departmentID.Replace(" ", "").Replace(",", "#") + "#").IndexOf("#" + d.Department.Id + "#") >= 0) {
								var x = new SponsorAdminDepartment {
									Id = sponsorAdminID,
									Department = new Department { Id = d.Department.Id }
								};
								departmentRepository.SaveSponsorAdminDepartment(x);
							}
						}
					}
				}
				Response.Redirect("managers.aspx", true);
			} else {
				ErrorMsg.Text = "<SPAN STYLE=\"color:#cc0000;\">Error! The username is invalid, please select a different one!</SPAN>";
			}
		}
	}
}