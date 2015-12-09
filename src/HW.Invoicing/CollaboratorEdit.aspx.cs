using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
	public partial class CollaboratorEdit : System.Web.UI.Page
	{
		int id;
//		SqlUserRepository ur = new SqlUserRepository();
//		SqlCompanyRepository cr = new SqlCompanyRepository();
		UserService service = new UserService();
		int companyId;
        protected Company company;

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
//			var company = cr.Read(companyId);
			company = service.ReadCompany(companyId);

			id = ConvertHelper.ToInt32(Request.QueryString["UserID"]);
			if (!IsPostBack)
			{
//				var u = ur.Read(id);
				var u = service.ReadUser(id);

				checkBoxListLinks.Items.Clear();
				foreach (var l in Link.GetLinks())
				{
					if (l.ForSubscription && !company.HasSubscriber)
					{
					}
					else
					{
						var li = new ListItem(l.Name, l.Id.ToString());
						li.Selected = u.HasAccess(l, companyId);
						checkBoxListLinks.Items.Add(li);
					}
				}
				if (u != null)
				{
					textBoxUsername.Text = u.Username;
					textBoxName.Text = u.Name;
					textBoxColor.Text = u.Color;
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
			u.SelectedCompany = new Company { Id = companyId, Links = links };
//			ur.Update(u, id);
			service.UpdateUser(u, id);
			Response.Redirect("collaborators.aspx");
		}
	}
}