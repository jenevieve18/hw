using HW.Core.Models;
using HW.Core.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.Grp
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected Sponsor sponsor;
        protected IList<ManagerFunctionLang> functions;
        protected int sponsorAdminID;
        protected string sponsorName;
        protected bool super;
        SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
        SqlManagerFunctionRepository managerFunctionRepository = new SqlManagerFunctionRepository();
        SqlUserRepository userRepository = new SqlUserRepository();
        protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

        protected void Page_Load(object sender, EventArgs e)
        {
            int sponsorID = Convert.ToInt32(Session["SponsorID"]);
            sponsor = sponsorRepository.ReadSponsor2(sponsorID);

            sponsorName = Session["Name"] != null ? Session["Name"].ToString() : "";
            sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;

            super = Request.Url.AbsolutePath.Contains("super");

            var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
            if (userSession != null)
            {
                lid = userSession.Lang;
            }
            functions = managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID, lid);
        }
    }
}