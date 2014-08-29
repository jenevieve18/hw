using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class SecuritySettings : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        
        protected string token;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            token = Session["token"].ToString();
            if (!Page.IsPostBack)
            {
                HttpCookie cToken = Request.Cookies["token"];
                if (cToken == null)
                {
                    cbLogin.Checked = false;
                }else if (!string.IsNullOrEmpty(cToken.Value))
                {
                    cbLogin.Checked = true;
                }

                HttpCookie splash = Request.Cookies["splash"];
                if (splash == null)
                {
                    cbSplash.Checked = true;
                }
                else if (!string.IsNullOrEmpty(splash.Value))
                {
                    cbSplash.Checked = false;
                }
            }
           
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            cbLogin.Text = R.Str("settings.security.login");
            cbSplash.Text = R.Str("settings.security.welcome");
        }

        protected void saveBtnClick(object sender, EventArgs e)
        {
            if (cbLogin.Checked)
            {
                HttpCookie cToken = new HttpCookie("token");
                cToken.Value = token;
                cToken.Expires = DateTime.Now.AddMonths(5);
                Response.Cookies.Add(cToken);
            }
            else
            {
                Response.Cookies["token"].Value = null;
                Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1);
            }

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

            Response.Redirect("Settings.aspx");
        }

        
    }
}