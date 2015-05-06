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

public partial class bq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataReader rs = Db.rs("SELECT BQID, Internal, Type, Comparison, Variable FROM BQ ORDER BY Internal");
        if (rs.Read())
        {
			BQ.Text = "<TR><TD><B>Question</B>&nbsp;&nbsp;</TD><TD><B>Variable</B>&nbsp;&nbsp;</TD><TD><B>Type</B>&nbsp;&nbsp;</TD><TD><B>Comparison</B>&nbsp;&nbsp;</TD></TR>";
            do
            {
                BQ.Text += "<TR><TD><A HREF=\"bqSetup.aspx?BQID=" + rs.GetInt32(0) + "\">" + (rs.GetString(1).Length > 30 ? rs.GetString(1).Substring(0,27) + "..." : rs.GetString(1)) + "</A>&nbsp;&nbsp;</TD><TD>" + (rs.IsDBNull(4) ? "" : rs.GetString(4)) + "&nbsp;&nbsp;<TD>";
                switch (rs.GetInt32(2))
                {
                    case 1: BQ.Text += "Select one, radio-style"; break;
                    case 2: BQ.Text += "Text"; break;
                    case 3: BQ.Text += "Date"; break;
                    case 4: BQ.Text += "Numeric"; break;
                    case 7: BQ.Text += "Select one, drop-down-style"; break;
                    case 9: BQ.Text += "VAS"; break;
                }
                BQ.Text += "&nbsp;&nbsp;</TD><TD>" + (rs.IsDBNull(3) ? "" : "X") + "&nbsp;&nbsp;</TD></TR>";
            }
            while (rs.Read());
        }
        rs.Close();
    }
}
