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
    	protected IList<SponsorAdminExercise> exercises;
    	protected int sponsorID;
    	protected int sponsorAdminID;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);

            sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
            sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
            int lid = ConvertHelper.ToInt32(Session["lid"], 2);

        	exercises = er.FindBySponsorAdminHistory(lid - 1, sponsorAdminID);
        }
    }
}