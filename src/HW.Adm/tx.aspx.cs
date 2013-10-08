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
    public partial class tx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = 0; string fn = "";
            if (Request.QueryString["FUID"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["FUID"]);
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

            if (Request.QueryString["FUID"] != null)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                //Response.ContentType = "application/xml";
                Response.WriteFile(System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\tx\\" + id);
                try
                {
                    Response.AddHeader("content-length", (new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\tx\\" + id)).Length.ToString());
                }
                catch (Exception) { }
                Response.AppendHeader("content-disposition", "attachment; filename=\"" + fn + "\"");
                Response.Flush();
                Response.End();
            }
        }
    }
}