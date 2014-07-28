using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Db.checkAndLogin();

            if (!IsPostBack)
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: submit.Text = "Ladda upp"; break;
                    case 2: submit.Text = "Upload"; break;
                }
            }
            submit.Click += new EventHandler(submit_Click);
        }

        void submit_Click(object sender, EventArgs e)
        {
            if (File.PostedFile.ContentLength > 0)
            {
                Db.exec("INSERT INTO FileUpload (Filename,Organisation,Description) VALUES ('" + File.PostedFile.FileName.Replace("'", "''") + "','" + Organisation.Text.Replace("'", "''") + "','" + Description.Text.Replace("'", "''") + "')");
                File.PostedFile.SaveAs("C:\\inetpub\\wwwroot\\healthwatch.se\\tx\\" + Db.execScalar("SELECT TOP 1 FileUploadID FROM FileUpload ORDER BY FileUploadID DESC"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: thanks.Text = "Tack!"; break;
                    case 2: thanks.Text = "Thanks!"; break;
                }
            }
        }
    }
}