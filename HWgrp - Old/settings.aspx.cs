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

public partial class settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Save.Click += new EventHandler(Save_Click);
        if (Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) <= 0)
        {
            Message.Text = "Super administrators cannot change password. Please contact support@healthwatch.se!";
            Save.Visible = false;
            Password.Visible = false;
            Txt.Visible = false;
        }
    }

    void Save_Click(object sender, EventArgs e)
    {
        if (Password.Text.Length > 1)
        {
            Db2.exec("UPDATE SponsorAdmin SET Pas = '" + Password.Text.Replace("'", "''") + "' WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]));
            Message.Text = "New password saved!";
        }
        else
        {
            Message.Text = "Password too short!";
        }
    }
}