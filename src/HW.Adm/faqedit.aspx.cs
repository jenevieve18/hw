using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

namespace HW.Adm
{
    public partial class faqedit : System.Web.UI.Page
    {
        SqlFAQRepository fr = new SqlFAQRepository();
        SqlLanguageRepository lr = new SqlLanguageRepository();
        protected IList<Language> languages;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
        	languages = lr.FindAll();
        	id = ConvertHelper.ToInt32(Request.QueryString["FAQID"]);
            
            foreach (var l in languages)
            {
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td><img align='right' src='img/langID_" + l.Id + ".gif'>Question</td><td>"));
                var q = new TextBox { ID = "textBoxQuestion" + l.Id };
                placeHolderLanguages.Controls.Add(q);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Answer</td><td>"));
                var a = new TextBox { ID = "textBoxAnswer" + l.Id, TextMode = TextBoxMode.MultiLine, Width = 300, Height = 200 };
                placeHolderLanguages.Controls.Add(a);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }

            if (!IsPostBack)
            {
                var f = fr.Read(id);
                if (f != null)
                {
                    textBoxName.Text = f.Name;

                    foreach (var l in languages)
                    {
                        TextBox q = placeHolderLanguages.FindControl("textBoxQuestion" + l.Id) as TextBox;
                        TextBox a = placeHolderLanguages.FindControl("textBoxAnswer" + l.Id) as TextBox;
                        var y = f.FindLanguage(l.Id);
                        if (y != null)
                        {
                            q.Text = y.Question;
                            a.Text = y.Answer;
                        }
                    }
                }
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var f = new FAQ { Id = id, Name = textBoxName.Text };
            fr.Update(f, id);

            foreach (var l in languages)
            {
                TextBox q = placeHolderLanguages.FindControl("textBoxQuestion" + l.Id) as TextBox;
                TextBox a = placeHolderLanguages.FindControl("textBoxAnswer" + l.Id) as TextBox;
                var fl = new FAQLanguage
                {
                    FAQ = f,
                    Question = q.Text,
                    Answer = a.Text,
                    Language = new Language { Id = l.Id }
                };
                var x = fr.ReadFAQLanguage(id, l.Id);
                if (x != null)
                {
                    fr.UpdateFAQLanguage(fl, x.Id);
                }
                else
                {
                    fr.SaveFAQLanguage(fl);
                }
            }
            Response.Redirect("faq.aspx");
        }
    }
}