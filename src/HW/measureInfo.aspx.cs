using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
    public partial class measureInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader rs = Db.rs("SELECT Measure, MoreInfo FROM Measure WHERE MeasureID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["MID"]));
            if (rs.Read())
            {
                Contents.Text = "<TABLE STYLE=\"margin:20px;\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD ALIGN=\"LEFT\"><H2>" + rs.GetString(0) + "</H2>" + rs.GetString(1).Replace("\r\n", "<br/>") + "</TD></TR></TABLE>";
            }
            rs.Close();
        }
    }
}