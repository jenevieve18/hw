using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
    public partial class code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { submit.Text = "Nästa"; break; }
                    case 2: { submit.Text = "Next"; break; }
                }

                if (HttpContext.Current.Request.QueryString["Code"] != null)
                {
                    Code.Text = HttpContext.Current.Request.QueryString["Code"].ToString();
                }
            }

            submit.Click += new EventHandler(submit_Click);
        }

        void submit_Click(object sender, EventArgs e)
        {
            if (Code.Text != "" && Code.Text.Length > 4)
            {
                int sponsorID = 0;
                int departmentID = 0;

                try
                {
                    SqlDataReader rs = Db.rs("SELECT SponsorID, DepartmentID FROM Department WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),DepartmentKey),'-',''),4) = '" + Code.Text.Substring(0, 4).Replace("'", "") + "' AND DepartmentID = " + Convert.ToInt32(Code.Text.Substring(4)));
                    if (rs.Read())
                    {
                        sponsorID = rs.GetInt32(0);
                        departmentID = rs.GetInt32(1);
                    }
                    rs.Close();
                }
                catch (Exception) { }

                if (sponsorID != 0 && departmentID != 0)
                {
                    int userID = Db.createAccount("AUTO_CREATED_GUEST_" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), Email.Text.Replace("'",""), "AUTO_CREATED_PASS_" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), sponsorID, departmentID.ToString(), 0, "");
                    Db.exec("INSERT INTO SponsorInvite (SponsorID,DepartmentID,Email,UserID,Sent,DupeCheck) VALUES (" +
                        sponsorID + "," +
                        departmentID + "," +
                        "'code." + userID + "@healthwatch.se'," +
                        userID + "," +
                        "GETDATE()," +
                        "'" + Db.HashMD5(userID.ToString()) + "'" +
                        ")");
                    Db.checkAndLogin(userID);
                }
            }
        }
    }
}