using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Service : System.Web.UI.Page
	{
		static SqlSponsorAdminRepository sponsorAdminRepo = new SqlSponsorAdminRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		[WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public static object ReadManagerExercise(int sponsorAdminExerciseID)
		{
			var e = sponsorAdminRepo.ReadSponsorAdminExercise(sponsorAdminExerciseID);
			if (e != null) {
				return e.ToObject();
			}
			return null;
		}
		
		[WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public static string Hello()
		{
			return "Hello World";
		}
		
		[WebMethod]
		public static string SaveManagerExercise(SponsorAdminExercise exercise)
		{
			if (exercise.Id > 0) {
				sponsorAdminRepo.UpdateSponsorAdminExercise(exercise, exercise.Id);
			} else {
				sponsorAdminRepo.SaveSponsorAdminExercise(exercise);
			}
			return "Success! Manager exercise saved successfully.";
		}
	}
}