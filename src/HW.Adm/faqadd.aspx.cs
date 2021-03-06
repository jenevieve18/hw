﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

namespace HW.Adm
{
    public partial class faqadd : System.Web.UI.Page
    {
        SqlLanguageRepository r = new SqlLanguageRepository();
        SqlFAQRepository fr = new SqlFAQRepository();
        protected IList<Language> languages;

        protected void Page_Load(object sender, EventArgs e)
        {
            languages = r.FindAll();
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
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var f = new FAQ { Name = textBoxName.Text };
            var id = fr.SaveFAQ(f);
            foreach (var l in languages)
            {
                TextBox q = placeHolderLanguages.FindControl("textBoxQuestion" + l.Id) as TextBox;
                TextBox a = placeHolderLanguages.FindControl("textBoxAnswer" + l.Id) as TextBox;
                var fl = new FAQLanguage
                {
                    FAQ = new FAQ { Id = id },
                    Question = q.Text,
                    Answer = a.Text,
                    Language = new Language { Id = l.Id }
                };
                fr.SaveFAQLanguage(fl);
            }
            Response.Redirect("faq.aspx");
        }
    }
}