using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Repositories;

namespace HW.EForm
{
	public partial class LanguagesDelete : System.Web.UI.Page
	{
		ILanguageRepository r = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["LangID"]);
			r.Delete(r.Read(id));
			Response.Redirect("Languages.aspx");
		}
	}
}