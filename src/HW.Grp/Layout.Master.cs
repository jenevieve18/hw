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
        //		protected int lid;
        protected int sponsorAdminID;
        protected string sponsorName;
        protected bool super;
        SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
        SqlManagerFunctionRepository managerFunctionRepository = new SqlManagerFunctionRepository();
        SqlUserRepository userRepository = new SqlUserRepository();
        //		protected int lid = Language.ENGLISH;
        protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

        protected void Page_Load(object sender, EventArgs e)
        {
            int sponsorID = Convert.ToInt32(Session["SponsorID"]);
            // sponsor = sponsorRepository.ReadSponsor2(sponsorID);

            sponsorName = Session["Name"] != null ? Session["Name"].ToString() : "";
            sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;

            super = Request.Url.AbsolutePath.Contains("super");

            //			lid = ConvertHelper.ToInt32(Session["lid"], 2);
            //var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
            //if (userSession != null)
            //{
            //    lid = userSession.Lang;
            //}
            // functions = managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID, lid);

            functions = new List<ManagerFunctionLang>();
            {

                var f = new ManagerFunctionLang
                {
                    Function = "Organization",
                    URL = "org.aspx",
                    Expl = "administer units and users",
                };

                functions.Add(f);


                var f1 = new ManagerFunctionLang
                {
                    Function = "Statistics",
                    URL = "stats.aspx",
                    Expl = "view results and compare groups",
                };
                functions.Add(f1);

                var f2 = new ManagerFunctionLang
                {
                    Function = "Messages",
                    URL = "messages.aspx",
                    Expl = "administer messages, invitations and reminders",
                };
                functions.Add(f2);

                var f3 = new ManagerFunctionLang
                {
                    Function = "Managers",
                    URL = "managers.aspx",
                    Expl = "administer unit managers",
                };
                functions.Add(f3);


                var f4 = new ManagerFunctionLang
                {
                    Function = "Exercise",
                    URL = "exercise.aspx",
                    Expl = "manager exercises",
                };
                functions.Add(f4);
            }
        }
    }
}