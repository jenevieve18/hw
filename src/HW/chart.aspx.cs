using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace HW
{
    public partial class chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sponsorID = 0;
            string sponsorKey = (HttpContext.Current.Request.QueryString["SK"] != null ? HttpContext.Current.Request.QueryString["SK"].ToString() : "");
            if (sponsorKey != "")
            {
                SqlDataReader rs = Db.rs("SELECT SponsorID FROM Sponsor WHERE LEFT(CAST(SponsorKey AS VARCHAR(64)),8) = '" + sponsorKey.Substring(0, 8).Replace("'", "") + "' AND SponsorID = " + Convert.ToInt32(sponsorKey.Substring(8)));
                if (rs.Read())
                {
                    sponsorID = rs.GetInt32(0);
                }
                rs.Close();
            }
            int LID = (HttpContext.Current.Request.QueryString["LID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LID"].ToString()) : 1);

            charts.Controls.Add(new LiteralControl(Db.rightNow(sponsorID, LID)));
        }
    }
}