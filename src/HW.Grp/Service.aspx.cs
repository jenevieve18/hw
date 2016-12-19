using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
    public partial class Service : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public SponsorAdminExercise ReadAdminexercise(int sponsorAdminExerciseID)
        {
        	var r = new SqlSponsorRepository();
        	return r.ReadSponsorAdminExercise();
        }
    }
}