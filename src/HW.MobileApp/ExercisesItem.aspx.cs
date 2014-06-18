using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class ExercisesItem : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.Exercise ex;
        string token = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else token = Session["token"].ToString();

            if (Request.QueryString["varid"] == null) Response.Redirect("Login.aspx");

            
            int lang = int.Parse(Session["languageId"].ToString());

            ex = service.ExerciseExec(token, int.Parse(Request.QueryString["varid"]),10);
            
        }
    }
}