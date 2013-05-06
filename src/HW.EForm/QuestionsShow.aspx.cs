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
	public partial class QuestionsShow : System.Web.UI.Page
	{
		IQuestionRepository r = AppContext.GetRepositoryFactory().CreateQuestionRepository();
		protected Question question;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			question = r.Read(Convert.ToInt32(Request["QuestionID"]));
		}
	}
}