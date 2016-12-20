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
    	SqlSponsorAdminRepository sponsorAdminRepo = new SqlSponsorAdminRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethodAttribute]
        public SponsorAdminExercise ReadManagerExercise(int sponsorAdminExerciseID)
        {
        	return sponsorAdminRepo.ReadSponsorAdminExercise(sponsorAdminExerciseID);
        }
        
        [WebMethod]
		[ScriptMethod(UseHttpGet = true)]
        public void SaveManagerExercise(SponsorAdminExercise exercise)
        {
        	sponsorAdminRepo.SaveSponsorAdminExercise(exercise);
        }
    }
}