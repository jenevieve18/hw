using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
	public partial class managerSetup : System.Web.UI.Page
	{
		int sponsorAdminID = 0;

        IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
        IManagerFunctionRepository managerRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
        ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) != 0)
			{
//				SqlDataReader rs;

				Save.Click += new EventHandler(Save_Click);
				Cancel.Click += new EventHandler(Cancel_Click);

				if (!IsPostBack)
				{
//					rs = Db.rs("SELECT ManagerFunctionID, ManagerFunction, Expl FROM ManagerFunction");
//					while (rs.Read())
					foreach (var f in managerRepository.FindAll())
					{
//						ManagerFunctionID.Items.Add(new ListItem(rs.GetString(1) + " (" + rs.GetString(2) + ")", rs.GetInt32(0).ToString()));
						ManagerFunctionID.Items.Add(new ListItem(f.Function + " (" + f.Expl + ")", f.Id.ToString()));
					}
//					rs.Close();

//					rs = Db.rs("SELECT SuperUser FROM SponsorAdmin WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
					sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorAdminID);
//					if (rs.Read() && rs.IsDBNull(0))
					if (a != null && !a.SuperUser)
					{
						SuperUser.Visible = false;
					}
//					rs.Close();

					OrgTree.Text = "<TR><TD>" + HttpContext.Current.Session["Sponsor"] + "</TD></TR>";
					bool[] DX = new bool[8];
//					rs = Db.rs("SELECT " +
//					           "d.Department, " +                              // 0
//					           "dbo.cf_departmentDepth(d.DepartmentID), " +    // 1
//					           "d.DepartmentID, " +                            // 2
//					           "(" +
//					           "SELECT COUNT(*) FROM Department x " +
//					           "WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL) " +
//					           "AND d.SponsorID = x.SponsorID " +
//					           "AND d.SortString < x.SortString" +
//					           "), " +		                                    // 3 - Number of departments on same level after this one
//					           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//					            "ISNULL(sad.DepartmentID, sa.SuperUser), " +     // 4
//					            "d.DepartmentShort " +                          // 5
//					            "FROM Department d " +
//					            "INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//					            "LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID AND sad.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " +
//					            "" : "1, d.DepartmentShort FROM Department d ") +
//					           "WHERE d.SponsorID = " + HttpContext.Current.Session["SponsorID"] + " " +
//					           "ORDER BY d.SortString");
//					while (rs.Read())
					int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
					foreach (var d in departmentRepository.a(sponsorID, sponsorAdminID))
					{
//						int depth = rs.GetInt32(1);
						int depth = d.Department.Depth;
//						DX[depth] = (rs.GetInt32(3) > 0);
						DX[depth] = (d.Department.Siblings > 0);

						OrgTree.Text += "<TR><TD><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>";
						for (int i = 1; i <= depth; i++)
						{
							if (i == depth)
							{
								OrgTree.Text += "<IMG SRC=\"img/" + (DX[i] ? "T" : "L") + ".gif\" width=\"19\" height=\"20\"/>";
							}
							else
							{
								OrgTree.Text += "<IMG SRC=\"img/" + (DX[i] ? "I" : "null") + ".gif\" width=\"19\" height=\"20\"/>";
							}
						}
//						OrgTree.Text += "</TD>" +
//							"<TD VALIGN=\"MIDDLE\"><input" + (rs.IsDBNull(4) ? " disabled" : "") + " value=\"" + rs.GetInt32(2) + "\" name=\"DepartmentID\" type=\"checkbox\"/>&nbsp;" + rs.GetString(0) + "&nbsp;&nbsp;&nbsp;</TD>" +
//							"</TR>" +
//							"</TABLE>" +
//							"</TD>" +
//							"<TD STYLE=\"font-size:9px;\">&nbsp;" + (rs.IsDBNull(5) ? "" : rs.GetString(5)) + "</TD>" +
//							"</TR>";
						OrgTree.Text += "</TD>" +
							"<TD VALIGN=\"MIDDLE\"><input" + (d.Admin.SuperUser ? " disabled" : "") + " value=\"" + d.Id + "\" name=\"DepartmentID\" type=\"checkbox\"/>&nbsp;" + d.Department.Name + "&nbsp;&nbsp;&nbsp;</TD>" +
							"</TR>" +
							"</TABLE>" +
							"</TD>" +
							"<TD STYLE=\"font-size:9px;\">&nbsp;" + d.Department.ShortName + "</TD>" +
							"</TR>";
					}
//					rs.Close();
				}

				if (HttpContext.Current.Request.QueryString["SAID"] != null)
				{
//					rs = Db.rs("SELECT " +
//					           "SponsorAdminID, " +
//					           "Name, " +
//					           "Usr, " +
//					           "Email, " +
//					           "SuperUser, " +
//					           "ReadOnly " +
//					           "FROM SponsorAdmin " +
//					           "WHERE (SponsorAdminID <> " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " OR SuperUser = 1) " +
//					           "AND SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SAID"]) + " " +
//					           "AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
					int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
					int SAID = Convert.ToInt32(HttpContext.Current.Request.QueryString["SAID"]);
					var a = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID, SAID);
//					if (rs.Read())
					if (a !=  null)
					{
//						sponsorAdminID = rs.GetInt32(0);
						sponsorAdminID = a.Id;
						if (!IsPostBack)
						{
//							ReadOnly.Checked = !rs.IsDBNull(5);
							ReadOnly.Checked = a.ReadOnly;
//							SuperUser.Checked = !rs.IsDBNull(4);
							SuperUser.Checked = a.SuperUser;
//							Name.Text = (rs.IsDBNull(1) ? rs.GetString(2) : rs.GetString(1));
							Name.Text = (a.Name == "" ? a.Usr : a.Name);
//							Usr.Text = rs.GetString(2);
							Usr.Text = a.Usr;
							Pas.Attributes.Add("value", "Not shown");
//							Email.Text = (rs.IsDBNull(3) ? "" : rs.GetString(3));
							Email.Text = a.Email;

//							SqlDataReader rs2 = Db.rs("SELECT ManagerFunctionID FROM SponsorAdminFunction WHERE SponsorAdminID = " + rs.GetInt32(0));
//							while (rs2.Read())
							foreach (var f in sponsorRepository.FindAdminFunctionBySponsorAdmin(a.Id))
							{
//								if (ManagerFunctionID.Items.FindByValue(rs2.GetInt32(0).ToString()) != null)
								if (ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()) != null)
								{
//									ManagerFunctionID.Items.FindByValue(rs2.GetInt32(0).ToString()).Selected = true;
									ManagerFunctionID.Items.FindByValue(f.Function.Id.ToString()).Selected = true;
								}
							}
//							rs2.Close();

//							rs2 = Db.rs("SELECT DepartmentID FROM SponsorAdminDepartment WHERE SponsorAdminID = " + rs.GetInt32(0));
//							while (rs2.Read())
							foreach (var d in sponsorRepository.FindAdminDepartmentBySponsorAdmin(a.Id))
							{
//								OrgTree.Text = OrgTree.Text.Replace("value=\"" + rs2.GetInt32(0) + "\"", "value=\"" + rs2.GetInt32(0) + "\" checked");
								OrgTree.Text = OrgTree.Text.Replace("value=\"" + d.Department.Id + "\"", "value=\"" + d.Department.Id + "\" checked");
							}
//							rs2.Close();
						}
					}
//					rs.Close();
				}
			}
			else
			{
				HttpContext.Current.Response.Redirect("default.aspx", true);
			}
		}

		void Cancel_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("managers.aspx", true);
		}

		void Save_Click(object sender, EventArgs e)
		{
//			SqlDataReader rs;

			/*bool usrExist = (Usr.Text == "");
			if (!usrExist)
			{
//				rs = Db.rs("SELECT SponsorAdminID FROM SponsorAdmin WHERE Usr = '" + Usr.Text.Replace("'", "") + "'" + (sponsorAdminID != 0 ? " AND SponsorAdminID != " + sponsorAdminID : ""));
				var a = sponsorRepository.ReadSponsorAdmin2(sponsorAdminID, Usr.Text);
//				if (rs.Read())
				if (a != null)
				{
					usrExist = true;
				}
//				rs.Close();
			}*/

//			if (!usrExist)
			if (!sponsorRepository.SponsorAdminExists(sponsorAdminID, Usr.Text))
			{
				if (sponsorAdminID != 0)
				{
//					Db.exec("UPDATE SponsorAdmin SET ReadOnly = " + (ReadOnly.Checked ? "1" : "NULL") + ", Email = '" + Email.Text.Replace("'", "''") + "', Name = '" + Name.Text.Replace("'", "''") + "', Usr = '" + Usr.Text.Replace("'", "") + "'" + (Pas.Text != "Not shown" && Pas.Text != "" ? ", Pas = '" + Pas.Text.Replace("'", "''") + "'" : "") + ", SuperUser = " + (SuperUser.Checked ? "1" : "NULL") + " WHERE SponsorAdminID = " + sponsorAdminID + " AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
					int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
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
				}
				else
				{
//					Db.exec("INSERT INTO SponsorAdmin (Email, Name, Usr, Pas, SponsorID, SuperUser, ReadOnly) VALUES ('" + Email.Text.Replace("'", "''") + "','" + Name.Text.Replace("'", "''") + "','" + Usr.Text.Replace("'", "") + "','" + Pas.Text.Replace("'", "''") + "'," + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "," + (SuperUser.Checked ? "1" : "NULL") + "," + (ReadOnly.Checked ? "1" : "NULL") + ")");
					int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
					var a = new SponsorAdmin {
						Email = Email.Text,
						Name = Name.Text,
						Usr = Usr.Text,
						Password = Pas.Text,
						Sponsor = new Sponsor { Id = sponsorID },
						SuperUser = SuperUser.Checked,
						ReadOnly = ReadOnly.Checked
					};
					sponsorRepository.InsertSponsorAdmin(a);
//					rs = Db.rs("SELECT SponsorAdminID FROM SponsorAdmin WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND Usr = '" + Usr.Text.Replace("'", "") + "'");
					sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					a = sponsorRepository.ReadSponsorAdmin(sponsorAdminID, Usr.Text);
//					if (rs.Read())
					if (a != null)
					{
//						sponsorAdminID = rs.GetInt32(0);
						sponsorAdminID = a.Id;
					}
//					rs.Close();
				}
//				Db.exec("DELETE FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminID);
				sponsorRepository.DeleteSponsorAdmin(sponsorAdminID);
//				rs = Db.rs("SELECT ManagerFunctionID FROM ManagerFunction");
//				while (rs.Read())
				foreach (var f in managerRepository.FindAll())
				{
//					if (ManagerFunctionID.Items.FindByValue(rs.GetInt32(0).ToString()) != null)
					if (ManagerFunctionID.Items.FindByValue(f.Id.ToString()) != null)
					{
//						if (ManagerFunctionID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
						if (ManagerFunctionID.Items.FindByValue(f.Id.ToString()).Selected)
						{
//							Db.exec("INSERT INTO SponsorAdminFunction (SponsorAdminID, ManagerFunctionID) VALUES (" + sponsorAdminID + "," + rs.GetInt32(0) + ")");
							var x = new SponsorAdminFunction {
								Admin = new SponsorAdmin { Id = sponsorAdminID },
								Function = new ManagerFunction { Id = f.Id }
							};
							sponsorRepository.InsertSponsorAdminFunction(x);
						}
					}
				}
//				rs.Close();
//				rs = Db.rs("SELECT " +
//				           "d.DepartmentID, " +                           // 0
//				           (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//				            "ISNULL(sad.DepartmentID,sa.SuperUser) " +     // 1
//				            "FROM Department d " +
//				            "INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
//				            "LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID AND sad.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " +
//				            "" : "1 FROM Department d ") +
//				           "WHERE d.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
//				while (rs.Read())
				sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
				int sponsorID2 = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
				foreach (var d in departmentRepository.b(sponsorID2, sponsorAdminID))
				{
//					if (!rs.IsDBNull(1))
					if (!d.Admin.SuperUser)
					{
//						Db.exec("DELETE FROM SponsorAdminDepartment WHERE DepartmentID = " + rs.GetInt32(0) + " AND SponsorAdminID = " + sponsorAdminID);
						departmentRepository.DeleteSponsorAdminDepartment(sponsorAdminID, d.Department.Id);
						if (HttpContext.Current.Request.Form["DepartmentID"] != null)
						{
//							if (("#" + HttpContext.Current.Request.Form["DepartmentID"].ToString().Replace(" ", "").Replace(",", "#") + "#").IndexOf("#" + rs.GetInt32(0) + "#") >= 0)
							if (("#" + HttpContext.Current.Request.Form["DepartmentID"].ToString().Replace(" ", "").Replace(",", "#") + "#").IndexOf("#" + d.Department.Id + "#") >= 0)
							{
//								Db.exec("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES (" + sponsorAdminID + "," + rs.GetInt32(0) + ")");
								var x = new SponsorAdminDepartment {
									Id = sponsorAdminID,
									Department = new Department { Id = d.Department.Id }
								};
								departmentRepository.InsertSponsorAdminDepartment(x);
							}
						}
					}
				}
//				rs.Close();
				HttpContext.Current.Response.Redirect("managers.aspx", true);
			}
			else
			{
				ErrorMsg.Text = "<SPAN STYLE=\"color:#cc0000;\">Error! The username is invalid, please select a different one!</SPAN>";
			}
		}
	}
}