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
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using System.Collections.Generic;

public partial class wise : System.Web.UI.Page
{
    SqlWiseRepository r = new SqlWiseRepository();
    protected IList<WiseLanguage> words;

    protected void Page_Load(object sender, EventArgs e)
    {
    	words = r.FindLanguages();
//    	SqlDataReader rs = Db.rs("SELECT wl.Wise, wl.WiseBy, w.LastShown FROM Wise w INNER JOIN WiseLang wl ON w.WiseID = wl.WiseID ORDER BY w.LastShown DESC");
//    	while (rs.Read())
//    	{
//    		Wisdom.Text += "<tr><td width=\"600\">" + (rs.IsDBNull(0) ? "" : rs.GetString(0)) + "</td><td valign=\"top\">" + (rs.IsDBNull(1) ? "" : rs.GetString(1)) + "</td><td valign=\"top\">" + (rs.IsDBNull(2) ? "" : rs.GetDateTime(2).ToString("yyyy-MM-dd")) + "</td></tr>";
//    	}
//    	rs.Close();
    }
}