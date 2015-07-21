using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class faqdelete : System.Web.UI.Page
    {
        SqlFAQRepository r = new SqlFAQRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = ConvertHelper.ToInt32(Request.QueryString["FAQID"]);
            r.Delete(id);
            Response.Redirect("faq.aspx");
        }
    }
}