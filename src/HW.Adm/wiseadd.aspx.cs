using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

namespace HW.Adm
{
    public partial class wiseadd : System.Web.UI.Page
    {
        SqlLanguageRepository lr = new SqlLanguageRepository();
        SqlWiseRepository wr = new SqlWiseRepository();
        protected IList<Language> languages;

        protected void Page_Load(object sender, EventArgs e)
        {
            languages = lr.FindAll();
            foreach (var l in languages)
            {
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td><img align='right' src='img/langID_" + l.Id + ".gif'>Words of Wisdom</td><td>"));
                var q = new TextBox { ID = "textBoxWiseName" + l.Id };
                placeHolderLanguages.Controls.Add(q);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Author</td><td>"));
                var a = new TextBox { ID = "textBoxWiseBy" + l.Id, TextMode = TextBoxMode.MultiLine, Width = 300, Height = 200 };
                placeHolderLanguages.Controls.Add(a);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var f = new Wise { };
            var id = wr.SaveWise(f);
            foreach (var l in languages)
            {
                TextBox q = placeHolderLanguages.FindControl("textBoxWiseName" + l.Id) as TextBox;
                TextBox a = placeHolderLanguages.FindControl("textBoxWiseBy" + l.Id) as TextBox;
                var wl = new WiseLanguage
                {
                    Wise = new Wise { Id = id },
                    WiseName = q.Text,
                    WiseBy = a.Text,
                    Language = new Language { Id = l.Id }
                };
                wr.SaveWiseLanguage(wl);
            }
            Response.Redirect("wise.aspx");
        }
    }
}