using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.EForm
{
	public partial class LanguagesAdd : System.Web.UI.Page
	{
		ILanguageRepository r = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var l = new Language {
					Name = Request["Name"]
				};
				r.SaveOrUpdate(l);
			}
			Response.Redirect("Languages.aspx");
		}
	}
}