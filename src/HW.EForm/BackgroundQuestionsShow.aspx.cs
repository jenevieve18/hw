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
	public partial class BackgroundQuestionsShow : System.Web.UI.Page
	{
		IQuestionRepository qr = AppContext.GetRepositoryFactory().CreateQuestionRepository();
		ILanguageRepository lr = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		protected BackgroundQuestion question;
		protected IList<Language> languages;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["BQID"]);
			question = qr.ReadBackgroundQuestion(id);
			languages = lr.FindAll();
		}
	}
}