using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;

namespace HW.Adm
{
    public partial class superadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Submit.Click += new EventHandler(Submit_Click);
            if (!IsPostBack)
            {
                SqlDataReader rs;

                if (HttpContext.Current.Request.QueryString["SuperAdminID"] != null)
                {
                    int superAdminID = Convert.ToInt32(HttpContext.Current.Request.QueryString["SuperAdminID"]);
                    SponsorAdminChange.Visible = true;

                    rs = Db.rs("SELECT s.SponsorID, s.Sponsor FROM Sponsor s WHERE s.Deleted IS NULL AND s.Closed IS NULL ORDER BY s.Sponsor");
                    while (rs.Read())
                    {
                        SponsorID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                    }
                    rs.Close();

                    if (superAdminID != 0)
                    {
                        rs = Db.rs("SELECT Username, Password FROM SuperAdmin WHERE SuperAdminID = " + superAdminID);
                        if (rs.Read())
                        {
                            Username.Text = rs.GetString(0);
                            Password.Text = rs.GetString(1);
                        }
                        rs.Close();

                        rs = Db.rs("SELECT SponsorID FROM SuperAdminSponsor WHERE SuperAdminID = " + superAdminID);
                        while (rs.Read())
                        {
                            if (SponsorID.Items.FindByValue(rs.GetInt32(0).ToString()) != null)
                            {
                                SponsorID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
                            }
                        }
                        rs.Close();
                    }
                }
                else
                {
                    SuperAdminList.Visible = true;

                    rs = Db.rs("SELECT sa.SuperAdminID, sa.Username, (SELECT COUNT(*) FROM SuperAdminSponsor sas INNER JOIN Sponsor s ON sas.SponsorID = s.SponsorID WHERE s.Closed IS NULL AND s.Deleted IS NULL AND sa.SuperAdminID = sas.SuperAdminID) FROM SuperAdmin sa ORDER BY REVERSE(sa.Username)");
                    while (rs.Read())
                    {
                        Superadmins.Text += "<tr><td><a href=\"superadmin.aspx?SuperAdminID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</a></td><td>" + rs.GetInt32(2) + "</td></tr>";
                    }
                    rs.Close();
                }
            }
        }

        void Submit_Click(object sender, EventArgs e)
        {
            SqlDataReader rs;
            int superAdminID = Convert.ToInt32(HttpContext.Current.Request.QueryString["SuperAdminID"]);

            if (superAdminID == 0)
            {
                Db.exec("INSERT INTO SuperAdmin (Username,Password) VALUES ('" + Username.Text.Replace("'", "") + "','" + Password.Text.Replace("'", "''") + "')");
                superAdminID = Db.getInt32("SELECT TOP 1 SuperAdminID FROM SuperAdmin ORDER BY SuperAdminID DESC");
            }
            else
            {
                Db.exec("DELETE FROM SuperAdminSponsor WHERE SuperAdminID = " + superAdminID);
            }
            rs = Db.rs("SELECT s.SponsorID FROM Sponsor s WHERE s.Deleted IS NULL AND s.Closed IS NULL");
            while (rs.Read())
            {
                if (SponsorID.Items.FindByValue(rs.GetInt32(0).ToString()) != null && SponsorID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
                {
                    Db.exec("INSERT INTO SuperAdminSponsor (SuperAdminID,SponsorID) VALUES (" + superAdminID + "," + rs.GetInt32(0) + ")");
                }
            }
            rs.Close();

            HttpContext.Current.Response.Redirect("superadmin.aspx", true);
        }
    }
}