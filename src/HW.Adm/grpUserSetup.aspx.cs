using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using System.Data.SqlClient;

namespace HW.Adm
{
    public partial class grpUserSetup : System.Web.UI.Page
    {
        int sponsorAdminID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            sponsorAdminID = (Request.QueryString["SAID"] != null ? Convert.ToInt32(Request.QueryString["SAID"]) : 0);

            if (!IsPostBack)
            {
                int sponsorID = 0;
                SqlDataReader rs;

                if (sponsorAdminID != 0)
                {
                    rs = Db.rs("SELECT Usr, SponsorID FROM SponsorAdmin WHERE SponsorAdminID = " + sponsorAdminID);
                    if (rs.Read())
                    {
                        Usr.Text = rs.GetString(0);
                        sponsorID = rs.GetInt32(1);
                    }
                    rs.Close();
                }

                rs = Db.rs("SELECT SponsorID, Sponsor FROM Sponsor " + (sponsorID != 0 ? "WHERE SponsorID = " + sponsorID + " " : "") + "ORDER BY Sponsor");
                while (rs.Read())
                {
                    SponsorID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();

                rs = Db.rs("SELECT ManagerFunctionID, ManagerFunction FROM ManagerFunction");
                while (rs.Read())
                {
                    AccessID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();

                populateDepartments();

                if (sponsorAdminID != 0)
                {
                    rs = Db.rs("SELECT DepartmentID FROM SponsorAdminDepartment WHERE SponsorAdminID = " + sponsorAdminID);
                    while (rs.Read())
                    {
                        DepartmentID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                    }
                    rs.Close();

                    rs = Db.rs("SELECT ManagerFunctionID FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminID);
                    while (rs.Read())
                    {
                        if (AccessID.Items.FindByValue(rs.GetInt32(0).ToString()) != null)
                            AccessID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                    }
                    rs.Close();
                }
            }

            Save.Click += new EventHandler(Save_Click);
            SponsorID.SelectedIndexChanged += new EventHandler(SponsorID_SelectedIndexChanged);
        }

        private void populateDepartments()
        {
            DepartmentID.Items.Clear();

            SqlDataReader rs = Db.rs("SELECT DepartmentID, Department, LEN(SortString) FROM Department WHERE SponsorID = " + SponsorID.SelectedValue + " ORDER BY SortString");
            while (rs.Read())
            {
                string indent = "";
                for (int i = 0; i < rs.GetInt32(2) / 8 - 1; i++)
                {
                    indent += (i == rs.GetInt32(2) / 8 - 2 ? "+ " : " ");
                }
                DepartmentID.Items.Add(new ListItem(indent + rs.GetString(1), rs.GetInt32(0).ToString()));
            }
            rs.Close();
        }

        void SponsorID_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateDepartments();
        }

        void Save_Click(object sender, EventArgs e)
        {
            SqlDataReader rs;

            if (sponsorAdminID != 0)
            {
                Db.exec("UPDATE SponsorAdmin SET Usr = '" + Usr.Text.Replace("'", "''") + "'" + (Pas.Text != "" ? ", Pas = '" + Pas.Text.Replace("'", "''") + "'" : "") + " WHERE SponsorAdminID = " + sponsorAdminID);
                Db.exec("DELETE FROM SponsorAdminDepartment WHERE SponsorAdminID = " + sponsorAdminID);
                Db.exec("DELETE FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminID);
            }
            else
            {
                Db.exec("INSERT INTO SponsorAdmin (SponsorID,Usr,Pas) VALUES (" + SponsorID.SelectedValue + ",'" + Usr.Text.Replace("'", "''") + "','" + Pas.Text.Replace("'", "''") + "')");
                rs = Db.rs("SELECT TOP 1 SponsorAdminID FROM SponsorAdmin WHERE SponsorID = " + SponsorID.SelectedValue + " ORDER BY SponsorAdminID DESC");
                if (rs.Read())
                {
                    sponsorAdminID = rs.GetInt32(0);
                }
                rs.Close();
            }

            rs = Db.rs("SELECT DepartmentID FROM Department WHERE SponsorID = " + SponsorID.SelectedValue);
            while (rs.Read())
            {
                if (DepartmentID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
                {
                    Db.exec("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES (" + sponsorAdminID + "," + rs.GetInt32(0) + ")");
                }
            }
            rs.Close();

            rs = Db.rs("SELECT ManagerFunctionID FROM ManagerFunction");
            while (rs.Read())
            {
                if (AccessID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
                {
                    Db.exec("INSERT INTO SponsorAdminFunction (SponsorAdminID,ManagerFunctionID) VALUES (" + sponsorAdminID + "," + rs.GetInt32(0) + ")");
                }
            }
            rs.Close();

            Response.Redirect("grpUser.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
        }
    }
}