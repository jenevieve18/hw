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

public partial class tx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = 0; string fn = "";
        if (HttpContext.Current.Request.QueryString["FUID"] != null)
        {
            id = Convert.ToInt32(HttpContext.Current.Request.QueryString["FUID"]);
        }
        string org = ""; 
        SqlDataReader rs = Db.rs("SELECT FileUploadID, Filename, Organisation, Description FROM FileUpload ORDER BY Organisation");
        while (rs.Read())
        {
            if (rs.GetInt32(0) == id)
            {
                fn = rs.GetString(1);
            }
            if (org != (rs.IsDBNull(2) || rs.GetString(2) == "" ? "?" : rs.GetString(2)))
            {
                org = (rs.IsDBNull(2) || rs.GetString(2) == "" ? "?" : rs.GetString(2));
                list.Text += "<tr><td><h2 style=\"margin-top:20px;\">" + org + "</h2></td></tr>";
            }
            list.Text += "<tr><td><a href=\"tx.aspx?FUID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</a>" + (rs.IsDBNull(3) || rs.GetString(3) == "" ? "" : ", " + rs.GetString(3)) + "</td></tr>";
        }
        rs.Close();

        if (HttpContext.Current.Request.QueryString["FUID"] != null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //HttpContext.Current.Response.ContentType = "application/xml";
            HttpContext.Current.Response.WriteFile(System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\tx\\" + id);
            try
            {
                HttpContext.Current.Response.AddHeader("content-length", (new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\tx\\" + id)).Length.ToString());
            }
            catch (Exception) { }
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=\"" + fn + "\"");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}