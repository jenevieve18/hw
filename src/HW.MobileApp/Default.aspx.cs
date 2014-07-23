using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Default : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["splash"] != null)
                {
                    if (Request.Cookies["splash"].Value != null)
                        Response.Redirect("Login.aspx");
                }

                var i = service.TodaysWordsOfWisdom(2);
                lblWordsOfWisdom.Text = i.words;
                lblAuthor.Text = i.author;
                cbSplash.Checked = true;
                cbSplash_CheckedChanged(sender, e);
            }
                       
        }

        protected void cbSplash_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSplash.Checked)
            {
                Response.Cookies["splash"].Value = null;
                Response.Cookies["splash"].Expires = DateTime.Now.AddDays(-1);
            }
            else
            {
                Response.Cookies["splash"].Value = "1";
                Response.Cookies["splash"].Expires = DateTime.Now.AddMonths(5);
            }

        }
    }
}