using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class ReportIssue : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected int language;
        string token;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();
            language = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
            textBoxDescription.Attributes["placeholder"] = R.Str(language, "diary.notes.default");

        }

        
        protected void saveBtn_Click(object sender, EventArgs e)
        {
            if (textBoxDescription.Text!=""&&textBoxTitle.Text!="")
            {
                if (service.ReportIssue(token, 10, textBoxTitle.Text, textBoxDescription.Text))
                {
                    errormsg.Text = R.Str(language, "issue.success");
                    textBoxDescription.Text = "";
                    textBoxTitle.Text = "";
                }
            }
            else
            {
                errormsg.Text = R.Str(language, "issue.empty");
            }
        }
    }
}