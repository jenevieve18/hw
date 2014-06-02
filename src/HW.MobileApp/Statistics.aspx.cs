using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Statistics : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected List<HWService.Question> questions = new List<HWService.Question>();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");

            string token = Session["token"] == null ? "" : Session["token"].ToString();
            var forms = service.FormEnum(new HWService.FormEnumRequest(token, 1, 10)).FormEnumResult;
            foreach (var f in forms)
            {
                var q = service.FormQuestionEnum(new HWService.FormQuestionEnumRequest(token, 1, f.formKey, 10)).FormQuestionEnumResult;
                questions.AddRange(q);
            }
        }
    }
}