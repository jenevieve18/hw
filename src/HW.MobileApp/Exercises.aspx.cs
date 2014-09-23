using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Exercises : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.ExerciseArea[] exerciseArea;
        
        protected string token = "";
        protected int lang;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();

            lang = int.Parse(Session["languageId"].ToString());
            exerciseArea = service.ExerciseAreaEnum(new HWService.ExerciseAreaEnumRequest(token,0,lang,10)).ExerciseAreaEnumResult;
            
        }

    }
}