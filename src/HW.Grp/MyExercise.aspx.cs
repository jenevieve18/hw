using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Grp
{
	public partial class MyExercise : System.Web.UI.Page
	{
		SqlExerciseRepository er = new SqlExerciseRepository();
		SqlUserRepository userRepository = new SqlUserRepository();
		protected IList<SponsorAdminExercise> exercises;
		protected int sponsorID;
		protected int sponsorAdminID;
		protected int lid;
		
		public MyExercise() : this(LanguageFactory.GetLanguageID(HttpContext.Current.Request))
		{
		}
		
		public MyExercise(int lid)
		{
			this.lid = lid;
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);

			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			
			SetLanguage(userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent));
			
			Show(er.FindBySponsorAdminExerciseHistory(lid - 1, sponsorAdminID));
		}
		
		public void SetLanguage(UserSession userSession)
		{
			if (userSession != null) {
				this.lid = userSession.Lang;
			}
		}
		
		public void Show(IList<SponsorAdminExercise> exercises)
		{
			this.exercises = exercises;
		}
	}
}