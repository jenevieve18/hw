using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HWgrp___Old
{
	public partial class managerSetup : System.Web.UI.Page
	{
		int sponsorAdminID = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) != 0)
			{
				SqlDataReader rs;

				Save.Click += new EventHandler(Save_Click);
				Cancel.Click += new EventHandler(Cancel_Click);

				if (!IsPostBack)
				{
					rs = Db.rs("SELECT ManagerFunctionID, ManagerFunction, Expl FROM ManagerFunction");
					while (rs.Read())
					{
						ManagerFunctionID.Items.Add(new ListItem(rs.GetString(1) + " (" + rs.GetString(2) + ")", rs.GetInt32(0).ToString()));
					}
					rs.Close();

					rs = Db.rs("SELECT SuperUser FROM SponsorAdmin WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
					if (rs.Read() && rs.IsDBNull(0))
					{
						SuperUser.Visible = false;
					}
					rs.Close();

					OrgTree.Text = "<TR><TD>" + HttpContext.Current.Session["Sponsor"] + "</TD></TR>";
					bool[] DX = new bool[8];
					rs = Db.rs("SELECT " +
						"d.Department, " +                              // 0
						"dbo.cf_departmentDepth(d.DepartmentID), " +    // 1
						"d.DepartmentID, " +                            // 2
						"(" +
							"SELECT COUNT(*) FROM Department x " +
							"WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL) " +
							"AND d.SponsorID = x.SponsorID " +
							"AND d.SortString < x.SortString" +
						"), " +		                                    // 3 - Number of departments on same level after this one
						(HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
						"ISNULL(sad.DepartmentID, sa.SuperUser), " +     // 4
						"d.DepartmentShort " +                          // 5
						"FROM Department d " +
						"INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
						"LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID AND sad.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " +
						"" : "1, d.DepartmentShort FROM Department d ") +
						"WHERE d.SponsorID = " + HttpContext.Current.Session["SponsorID"] + " " +
						"ORDER BY d.SortString");
					while (rs.Read())
					{
						int depth = rs.GetInt32(1);
						DX[depth] = (rs.GetInt32(3) > 0);

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
						OrgTree.Text += "</TD>" +
						"<TD VALIGN=\"MIDDLE\"><input" + (rs.IsDBNull(4) ? " disabled" : "") + " value=\"" + rs.GetInt32(2) + "\" name=\"DepartmentID\" type=\"checkbox\"/>&nbsp;" + rs.GetString(0) + "&nbsp;&nbsp;&nbsp;</TD>" +
						"</TR>" +
						"</TABLE>" +
						"</TD>" +
						"<TD STYLE=\"font-size:9px;\">&nbsp;" + (rs.IsDBNull(5) ? "" : rs.GetString(5)) + "</TD>" +
						"</TR>";
					}
					rs.Close();
				}

				if (HttpContext.Current.Request.QueryString["SAID"] != null)
				{
					rs = Db.rs("SELECT " +
						"SponsorAdminID, " +
						"Name, " +
						"Usr, " +
						"Email, " +
						"SuperUser, " +
						"ReadOnly " +
						"FROM SponsorAdmin " +
						"WHERE (SponsorAdminID <> " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " OR SuperUser = 1) " +
						"AND SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SAID"]) + " " +
						"AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
					if (rs.Read())
					{
						sponsorAdminID = rs.GetInt32(0);
						if (!IsPostBack)
						{
							ReadOnly.Checked = !rs.IsDBNull(5);
							SuperUser.Checked = !rs.IsDBNull(4);
							Name.Text = (rs.IsDBNull(1) ? rs.GetString(2) : rs.GetString(1));
							Usr.Text = rs.GetString(2);
							Pas.Attributes.Add("value", "Not shown");
							Email.Text = (rs.IsDBNull(3) ? "" : rs.GetString(3));

							SqlDataReader rs2 = Db.rs("SELECT ManagerFunctionID FROM SponsorAdminFunction WHERE SponsorAdminID = " + rs.GetInt32(0));
							while (rs2.Read())
							{
								if (ManagerFunctionID.Items.FindByValue(rs2.GetInt32(0).ToString()) != null)
								{
									ManagerFunctionID.Items.FindByValue(rs2.GetInt32(0).ToString()).Selected = true;
								}
							}
							rs2.Close();

							rs2 = Db.rs("SELECT DepartmentID FROM SponsorAdminDepartment WHERE SponsorAdminID = " + rs.GetInt32(0));
							while (rs2.Read())
							{
								OrgTree.Text = OrgTree.Text.Replace("value=\"" + rs2.GetInt32(0) + "\"", "value=\"" + rs2.GetInt32(0) + "\" checked");
							}
							rs2.Close();
						}
					}
					rs.Close();
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
			SqlDataReader rs;

			bool usrExist = (Usr.Text == "");
			if (!usrExist)
			{
				rs = Db.rs("SELECT SponsorAdminID FROM SponsorAdmin WHERE Usr = '" + Usr.Text.Replace("'", "") + "'" + (sponsorAdminID != 0 ? " AND SponsorAdminID != " + sponsorAdminID : ""));
				if (rs.Read())
				{
					usrExist = true;
				}
				rs.Close();
			}

			if (!usrExist)
			{
				if (sponsorAdminID != 0)
				{
					Db.exec("UPDATE SponsorAdmin SET ReadOnly = " + (ReadOnly.Checked ? "1" : "NULL") + ", Email = '" + Email.Text.Replace("'", "''") + "', Name = '" + Name.Text.Replace("'", "''") + "', Usr = '" + Usr.Text.Replace("'", "") + "'" + (Pas.Text != "Not shown" && Pas.Text != "" ? ", Pas = '" + Pas.Text.Replace("'", "''") + "'" : "") + ", SuperUser = " + (SuperUser.Checked ? "1" : "NULL") + " WHERE SponsorAdminID = " + sponsorAdminID + " AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
				}
				else
				{
					Db.exec("INSERT INTO SponsorAdmin (Email, Name, Usr, Pas, SponsorID, SuperUser, ReadOnly) VALUES ('" + Email.Text.Replace("'", "''") + "','" + Name.Text.Replace("'", "''") + "','" + Usr.Text.Replace("'", "") + "','" + Pas.Text.Replace("'", "''") + "'," + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "," + (SuperUser.Checked ? "1" : "NULL") + "," + (ReadOnly.Checked ? "1" : "NULL") + ")");
					rs = Db.rs("SELECT SponsorAdminID FROM SponsorAdmin WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " AND Usr = '" + Usr.Text.Replace("'", "") + "'");
					if (rs.Read())
					{
						sponsorAdminID = rs.GetInt32(0);
					}
					rs.Close();
				}
				Db.exec("DELETE FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminID);
				rs = Db.rs("SELECT ManagerFunctionID FROM ManagerFunction");
				while (rs.Read())
				{
					if (ManagerFunctionID.Items.FindByValue(rs.GetInt32(0).ToString()) != null)
					{
						if (ManagerFunctionID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
						{
							Db.exec("INSERT INTO SponsorAdminFunction (SponsorAdminID, ManagerFunctionID) VALUES (" + sponsorAdminID + "," + rs.GetInt32(0) + ")");
						}
					}
				}
				rs.Close();
				rs = Db.rs("SELECT " +
						"d.DepartmentID, " +                           // 0
						(HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
						"ISNULL(sad.DepartmentID,sa.SuperUser) " +     // 1
						"FROM Department d " +
						"INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " " +
						"LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID AND sad.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " +
						"" : "1 FROM Department d ") +
						"WHERE d.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
				while (rs.Read())
				{
					if (!rs.IsDBNull(1))
					{
						Db.exec("DELETE FROM SponsorAdminDepartment WHERE DepartmentID = " + rs.GetInt32(0) + " AND SponsorAdminID = " + sponsorAdminID);
						if (HttpContext.Current.Request.Form["DepartmentID"] != null)
						{
							if (("#" + HttpContext.Current.Request.Form["DepartmentID"].ToString().Replace(" ", "").Replace(",", "#") + "#").IndexOf("#" + rs.GetInt32(0) + "#") >= 0)
							{
								Db.exec("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES (" + sponsorAdminID + "," + rs.GetInt32(0) + ")");
							}
						}
					}
				}
				rs.Close();
				HttpContext.Current.Response.Redirect("managers.aspx", true);
			}
			else
			{
				ErrorMsg.Text = "<SPAN STYLE=\"color:#cc0000;\">Error! The username is invalid, please select a different one!</SPAN>";
			}
		}
	}
}