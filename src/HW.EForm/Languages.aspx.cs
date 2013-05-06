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
	public partial class Languages : System.Web.UI.Page
	{
		protected IList<Language> languages;
		ILanguageRepository r = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			languages = r.FindAll();
		}
	}
}