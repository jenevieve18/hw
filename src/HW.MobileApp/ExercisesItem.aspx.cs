using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class ExercisesItem : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.Exercise ex;
        string token = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            token = Session["token"].ToString();

            HtmlHelper.RedirectIf(Request.QueryString["varid"] == null, "Default.aspx");
            
            int lang = int.Parse(Session["languageId"].ToString());

            ex = service.ExerciseExec(token, int.Parse(Request.QueryString["varid"]),10);
            
        }
    }
}