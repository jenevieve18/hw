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
	public partial class LanguagesEdit : System.Web.UI.Page
	{
		ILanguageRepository r = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		protected Language language;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["LangID"]);
			language = r.Read(id);
			
			if (Request["Submit"] != null) {
				language.Name = Request["Name"];
				r.SaveOrUpdate(language);
				Response.Redirect("Languages.aspx");
			}
		}
	}
}