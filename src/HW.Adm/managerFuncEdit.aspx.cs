using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class managerFuncEdit : System.Web.UI.Page
    {
        SqlLanguageRepository lr = new SqlLanguageRepository();
        protected IList<Language> languages;
        SqlManagerFunctionRepository fr = new SqlManagerFunctionRepository();
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            languages = lr.FindAll();
            id = ConvertHelper.ToInt32(Request.QueryString["ManagerFunctionID"]);

            foreach (var l in languages)
            {
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td valign='top'><img align='right' src='img/langID_" + l.Id + ".gif'>Manager Function</td><td>"));
                var f = new TextBox { ID = "textBoxManagerFunction" + l.Id };
                placeHolderLanguages.Controls.Add(f);
                
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>URL</td><td>"));
                var u = new TextBox { ID = "textBoxURL" + l.Id };
                placeHolderLanguages.Controls.Add(u);
                
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Explanation</td><td>"));
                var x = new TextBox { ID = "textBoxExpl" + l.Id };
                placeHolderLanguages.Controls.Add(x);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }

            if (!IsPostBack)
            {
                var m = fr.Read(id);
                if (m != null)
                {
                    textBoxUrl.Text = m.URL;
                    foreach (var l in languages)
                    {
                        TextBox f = placeHolderLanguages.FindControl("textBoxManagerFunction" + l.Id) as TextBox;
                        TextBox u = placeHolderLanguages.FindControl("textBoxURL" + l.Id) as TextBox;
                        TextBox x = placeHolderLanguages.FindControl("textBoxExpl" + l.Id) as TextBox;
                        var y = m.FindLanguage(l.Id + 1);
                        if (y != null)
                        {
                            f.Text = y.Function;
                            u.Text = y.URL;
                            x.Text = y.Expl;
                        }
                    }
                }
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var w = new ManagerFunction { Id = id, URL = textBoxUrl.Text };
            fr.Update(w, id);

            foreach (var l in languages)
            {
                TextBox f = placeHolderLanguages.FindControl("textBoxManagerFunction" + l.Id) as TextBox;
                TextBox u = placeHolderLanguages.FindControl("textBoxURL" + l.Id) as TextBox;
                TextBox x = placeHolderLanguages.FindControl("textBoxExpl" + l.Id) as TextBox;
                var fl = new ManagerFunctionLang
                {
                    ManagerFunction = w,
                    Function = f.Text,
                    URL = u.Text,
                    Expl = x.Text,
                    Language = new Language { Id = l.Id + 1 }
                };
                var temp = fr.ReadManagerFunctionLanguage(id, l.Id + 1);
                if (temp != null)
                {
                    fr.UpdateManagerFunctionLanguage(fl, temp.Id);
                }
                else
                {
                    fr.SaveManagerFunctionLanguage(fl);
                }
            }
            Response.Redirect("managerFunc.aspx");
        }
    }
}