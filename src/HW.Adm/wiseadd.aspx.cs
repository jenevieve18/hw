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
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td valign='top'><img align='right' src='img/langID_" + l.Id + ".gif'>Words of Wisdom</td><td>"));
                var n = new TextBox { ID = "textBoxWiseName" + l.Id, TextMode = TextBoxMode.MultiLine, Width = 200, Height = 100 };
                placeHolderLanguages.Controls.Add(n);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Author</td><td>"));
                var b = new TextBox { ID = "textBoxWiseBy" + l.Id };
                placeHolderLanguages.Controls.Add(b);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var f = new Wise { };
            var id = wr.SaveWise(f);
            foreach (var l in languages)
            {
                TextBox n = placeHolderLanguages.FindControl("textBoxWiseName" + l.Id) as TextBox;
                TextBox b = placeHolderLanguages.FindControl("textBoxWiseBy" + l.Id) as TextBox;
                var wl = new WiseLanguage
                {
                    Wise = new Wise { Id = id },
                    WiseName = n.Text,
                    WiseBy = b.Text,
                    Language = new Language { Id = l.Id + 1 }
                };
                wr.SaveWiseLanguage(wl);
            }
            Response.Redirect("wise.aspx");
        }
    }
}