using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace HW.Grp
{
    public partial class metadata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var metadata = ConfigurationManager.AppSettings["metadata"];
            if (metadata != null)
            {
                byte[] data = Convert.FromBase64String(metadata);

                var meta = System.Text.Encoding.Default.GetString(data);

                Response.Clear();
                Response.ContentType = "text/xml";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(meta);
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}