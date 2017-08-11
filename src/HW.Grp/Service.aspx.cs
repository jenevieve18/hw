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
using GrpWS = HW.Grp.WebService.Soap;
using HW.Grp.WebService;

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

        [WebMethod]
        public static string UpdateManagerExerciseComments(int sponsorAdminExerciseID, string comments)
        {
            sponsorAdminRepo.UpdateSponsorAdminExerciseComments(sponsorAdminExerciseID, comments);
            return "Success! Manager exercise comments saved successfully.";
        }



        #region
        // static GrpWS grpWSs = new GrpWS();
        static HW.Grp.WebService.Models.User grpModel = new HW.Grp.WebService.Models.User();

        [WebMethod]
        public static List<HW.Grp.WebService.Models.User> GetAllUsersInDepartment(string token, int departmentID, int sponsorID, int expirationMinutes)
        {
            var soap = new GrpWS();
            var getAllUsers = soap.GetUsersInDepartment(token, sponsorID, departmentID, 20);
            if (getAllUsers != null)
            {
                return getAllUsers;
            }
            return null;
        }
        #endregion
    }
}