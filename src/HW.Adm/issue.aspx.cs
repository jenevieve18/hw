using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

public partial class issue : System.Web.UI.Page
{
    SqlIssueRepository r = new SqlIssueRepository();
    protected IList<Issue> issues;

    protected void Page_Load(object sender, EventArgs e)
    {
        issues = r.FindAll();
        /*SqlDataReader rs = Db.rs("SELECT i.Title, i.Description, i.IssueDate, u.Email FROM Issue i LEFT OUTER JOIN [User] u ON i.UserID = u.UserID ORDER BY i.IssueDate DESC");
        while (rs.Read())
        {
            list.Text += "<tr><td>" + rs.GetString(0) + "</td><td>" + rs.GetString(1) + "</td><td>" + rs.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss") + "</td><td>" + (rs.IsDBNull(3) ? "Anonymous" : rs.GetString(3)) + "</td></tr>";
        }
        rs.Close();*/
    }
}