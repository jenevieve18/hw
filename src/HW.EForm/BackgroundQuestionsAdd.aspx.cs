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
	public partial class BackgroundQuestionsAdd : System.Web.UI.Page
	{
		IQuestionRepository r = AppContext.GetRepositoryFactory().CreateQuestionRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var q = new BackgroundQuestion {
					Internal = Request["Internal"]
				};
				r.SaveOrUpdateBackgroundQuestion(q);
				Response.Redirect("BackgroundQuestions.aspx");
			}
		}
	}
}