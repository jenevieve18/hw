﻿using System;
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
	public partial class BackgroundQuestions : System.Web.UI.Page
	{
		IQuestionRepository r = AppContext.GetRepositoryFactory().CreateQuestionRepository();
		protected IList<BackgroundQuestion> questions;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			questions = r.FindAllBackgroundQuestions();
		}
	}
}