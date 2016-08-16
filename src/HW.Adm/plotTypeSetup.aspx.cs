using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class plotTypeSetup : System.Web.UI.Page
    {
    	SqlLanguageRepository lr = new SqlLanguageRepository();
        PlotType plotType;
        SqlPlotTypeRepository pr = new SqlPlotTypeRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            plotType = pr.Read(ConvertHelper.ToInt32(Request.QueryString["ID"]));

            if (!IsPostBack)
            {
                if (plotType != null)
                {
                    textBoxName.Text = plotType.Name;
                    textBoxDescription.Text = plotType.Description;
                }
            }
        	
            foreach (var l in lr.FindAll())
            {
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td valign='top'><img align='right' src='img/langID_" + l.Id + ".gif'>Short Name</td><td>"));
                var s = new TextBox { ID = "textBoxShortName" + l.Id };
                placeHolderLanguages.Controls.Add(s);

                //placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td colspan='2'><hr></td></tr><tr><td valign='top'><img align='right' src='img/langID_" + l.Id + ".gif'>Name</td><td>"));
                placeHolderLanguages.Controls.Add(new LiteralControl("<tr><td valign='top'>Name</td><td>"));
                var b = new TextBox { ID = "textBoxName" + l.Id };
                placeHolderLanguages.Controls.Add(b);
                                
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr><td valign='top'>Description</td><td>"));
                var n = new TextBox { ID = "textBoxDescription" + l.Id, TextMode = TextBoxMode.MultiLine, Width = 200, Height = 100 };
                placeHolderLanguages.Controls.Add(n);
                placeHolderLanguages.Controls.Add(new LiteralControl("</td></tr>"));
            }

            if (plotType != null)
            {
                foreach (var l in pr.FindAllLanguages(plotType.Id))
                {
                    (placeHolderLanguages.FindControl("textBoxShortName" + (l.Language.Id - 1).ToString()) as TextBox).Text = l.ShortName;
                    (placeHolderLanguages.FindControl("textBoxName" + (l.Language.Id - 1).ToString()) as TextBox).Text = l.Name;
                    (placeHolderLanguages.FindControl("textBoxDescription" + (l.Language.Id - 1).ToString()) as TextBox).Text = l.Description;
                }
            }
            
            buttonSave.Click += new EventHandler(buttonSave_Click);
        }
        
        void buttonSave_Click(object sender, EventArgs e)
        {
            int plotTypeId;
            var t = new PlotType
            {
                Name = textBoxName.Text,
                Description = textBoxDescription.Text
            };
            if (plotType != null)
            {
                plotTypeId = plotType.Id;
                pr.Update(t, plotType.Id);
        	}
            else
            {
                plotTypeId = pr.Save(t);
        	}
            foreach (var p in pr.GetLanguagesWithPlotType(plotTypeId))
            {
                var x = new PlotTypeLanguage
                {
                    ShortName = (placeHolderLanguages.FindControl("textBoxShortName" + (p.Language.Id).ToString()) as TextBox).Text,
                    Name = (placeHolderLanguages.FindControl("textBoxName" + (p.Language.Id).ToString()) as TextBox).Text,
                    Description = (placeHolderLanguages.FindControl("textBoxDescription" + (p.Language.Id).ToString()) as TextBox).Text,
                    PlotType = new PlotType { Id = plotTypeId },
                    Language = new Core.Models.Language { Id = p.Language.Id + 1 }
                };
                if (p.Id > 0)
                {
                    pr.UpdateLanguage(x, p.Id);
                }
                else
                {
                    pr.SaveLanguage(x);
                }
            }
            Response.Redirect("plotType.aspx");
        }
    }
}