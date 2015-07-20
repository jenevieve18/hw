using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

namespace HW.Adm
{
    public partial class faq : System.Web.UI.Page
    {
        SqlFAQRepository r = new SqlFAQRepository();
        protected IList<FAQ> faqs;

        protected void Page_Load(object sender, EventArgs e)
        {
            faqs = r.FindAll();
        }
    }
}