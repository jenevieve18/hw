using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class CollaboratorAdd : System.Web.UI.Page
    {
    	SqlUserRepository ur = new SqlUserRepository();
    	SqlCompanyRepository cr = new SqlCompanyRepository();
        int companyId;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            var company = cr.Read(companyId);
            
            if (!IsPostBack) {
            	
                checkBoxListLinks.Items.Clear();
                foreach (var l in Link.GetLinks())
                {
                    if (l.ForSubscription && !company.HasSubscriber)
                    {
                    }
                    else
                    {
                        var li = new ListItem(l.Name, l.Id.ToString());
                        checkBoxListLinks.Items.Add(li);
                    }
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var u = new User
            {
                Username = textBoxUsername.Text,
                Name = textBoxName.Text,
                Password = textBoxPassword.Text,
                Color = textBoxColor.Text
            };
            var links = new List<Link>();
            foreach (ListItem l in checkBoxListLinks.Items)
            {
                if (l.Selected)
                {
                    links.Add(new Link { Id = ConvertHelper.ToInt32(l.Value) });
                }
            }
//            u.AddLinks(links);
            u.SelectedCompany = new Company { Id = companyId, Links = links };
            ur.Save(u);
            Response.Redirect("collaborators.aspx");
        }
    }
}