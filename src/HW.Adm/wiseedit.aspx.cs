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
    public partial class wiseedit : System.Web.UI.Page
    {
        SqlWiseRepository wr = new SqlWiseRepository();
        SqlLanguageRepository lr = new SqlLanguageRepository();
        protected IList<Language> languages;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            languages = lr.FindAll();
            id = ConvertHelper.ToInt32(Request.QueryString["WiseID"]);

            foreach (var l in languages)
            {
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td valign='top'><img align='right' src='img/langID_" + l.Id + ".gif'>Words of wisdom</td><td>"));
                var n = new TextBox { ID = "textBoxWiseName" + l.Id, TextMode = TextBoxMode.MultiLine, Width = 200, Height = 100 };
                placeHolderLanguages.Controls.Add(n);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Author</td><td>"));
                var b = new TextBox { ID = "textBoxWiseBy" + l.Id };
                placeHolderLanguages.Controls.Add(b);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }

            if (!IsPostBack)
            {
                var w = wr.Read(id);
                if (w != null)
                {
                    foreach (var l in languages)
                    {
                        TextBox n = placeHolderLanguages.FindControl("textBoxWiseName" + l.Id) as TextBox;
                        TextBox b = placeHolderLanguages.FindControl("textBoxWiseBy" + l.Id) as TextBox;
                        var y = w.FindLanguage(l.Id + 1);
                        if (y != null)
                        {
                            n.Text = y.WiseName;
                            b.Text = y.WiseBy;
                        }
                    }
                }
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var w = new Wise { Id = id };
            //wr.Update(w, id);

            foreach (var l in languages)
            {
                TextBox n = placeHolderLanguages.FindControl("textBoxWiseName" + l.Id) as TextBox;
                TextBox b = placeHolderLanguages.FindControl("textBoxWiseBy" + l.Id) as TextBox;
                var fl = new WiseLanguage
                {
                    Wise = w,
                    WiseName = n.Text,
                    WiseBy = b.Text,
                    Language = new Language { Id = l.Id + 1 }
                };
                var x = wr.ReadWiseLanguage(id, l.Id + 1);
                if (x != null)
                {
                    wr.UpdateWiseLanguage(fl, x.Id);
                }
                else
                {
                    wr.SaveWiseLanguage(fl);
                }
            }
            Response.Redirect("wise.aspx");
        }
    }
}