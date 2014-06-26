using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Form : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        public HW.MobileApp.HWService.Question[] question = null;
        public int questNo = 0;
        private string formKey = "";
        private string token = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            token = Session["token"].ToString();


            int lang = int.Parse(Session["languageId"].ToString());
            
            try{
            formKey = service.FormEnum(new HW.MobileApp.HWService.FormEnumRequest(token, lang, 10)).FormEnumResult[0].formKey;
            }catch(Exception ex){ Response.Redirect("Default.aspx"); };

            Session.Add("formKey",formKey);
            
            question = service.FormQuestionEnum(new HW.MobileApp.HWService.FormQuestionEnumRequest(token,lang,formKey,10)).FormQuestionEnumResult;
            questNo = question.Count();
            
        }

        protected void saveBtnClick(object sender, EventArgs e)
        {
            string data = answers.Value;
            string[] pageAnswers = data.Split('x');
            
            HWService.FormQuestionAnswer[] formAnswers = new HWService.FormQuestionAnswer[questNo];
            
            for(int i = 0; i < questNo;i++)
            {
                formAnswers[i] = new HWService.FormQuestionAnswer();
                formAnswers[i].optionID = question[i].OptionID;
                formAnswers[i].questionID = question[i].QuestionID;
                formAnswers[i].answer = pageAnswers[i];
            }

            string fik = service.UserSetFormInstance(new HWService.UserSetFormInstanceRequest(token, formKey, formAnswers, 10)).UserSetFormInstanceResult;
            if(fik != null || fik != "")
            {
                Session.Add("formInstanceKey",fik);
                Response.Redirect("Statistics.aspx");
            }
        }

        
    }
}