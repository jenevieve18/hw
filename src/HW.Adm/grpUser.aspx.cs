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
    public partial class grpUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int cx = 0;
                SqlDataReader rs = Db.rs("SELECT u.SponsorAdminID, u.Usr, s.Sponsor, LEFT(REPLACE(CONVERT(VARCHAR(255),u.SponsorAdminKey),'-',''),8), s.SponsorID FROM Sponsor s INNER JOIN SponsorAdmin u ON s.SponsorID = u.SponsorID ORDER BY s.Sponsor, u.Usr");
                while (rs.Read())
                {
                    List.Text += "<TR" + (cx++ % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + "><TD><A HREF=\"grpUserSetup.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&SAID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>&nbsp;&nbsp;&nbsp;</TD><TD>" + rs.GetString(2) + "&nbsp;&nbsp;</TD><TD><A HREF=\"" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/default.aspx?SAKEY=" + rs.GetString(3) + rs.GetInt32(4).ToString() + "\" TARGET=\"_blank\">Log in</A></TD></TR>";
                }
                rs.Close();
            }
        }
    }
}