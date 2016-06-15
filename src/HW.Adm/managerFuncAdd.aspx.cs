using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class managerFuncAdd : System.Web.UI.Page
    {
        SqlLanguageRepository lr = new SqlLanguageRepository();
        SqlManagerFunctionRepository fr = new SqlManagerFunctionRepository();
        protected IList<Language> languages;

        protected void Page_Load(object sender, EventArgs e)
        {
            languages = lr.FindAll();
            foreach (var l in languages)
            {
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td valign='top'><img align='right' src='img/langID_" + l.Id + ".gif'>Manager Function</td><td>"));
                var f = new TextBox { ID = "textBoxManagerFunction" + l.Id };
                placeHolderLanguages.Controls.Add(f);
                
                //placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>URL</td><td>"));
                //var u = new TextBox { ID = "textBoxURL" + l.Id };
                //placeHolderLanguages.Controls.Add(u);
                
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Explanation</td><td>"));
                var x = new TextBox { ID = "textBoxExpl" + l.Id };
                placeHolderLanguages.Controls.Add(x);
                
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var mf = new ManagerFunction { URL = textBoxUrl.Text };
            var id = fr.SaveManagerFunction(mf);
            foreach (var l in languages)
            {
                TextBox f = placeHolderLanguages.FindControl("textBoxManagerFunction" + l.Id) as TextBox;
                //TextBox u = placeHolderLanguages.FindControl("textBoxURL" + l.Id) as TextBox;
                TextBox x = placeHolderLanguages.FindControl("textBoxExpl" + l.Id) as TextBox;
                var fl = new ManagerFunctionLang
                {
                    ManagerFunction = new ManagerFunction { Id = id },
                    Function = f.Text,
                    //URL = u.Text,
                    Expl = x.Text,
                    Language = new Language { Id = l.Id + 1 }
                };
                fr.SaveManagerFunctionLanguage(fl);
            }
            Response.Redirect("managerFunc.aspx");
        }
    }
}