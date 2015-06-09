using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Default : System.Web.UI.Page
    {
        SqlNewsRepository r = new SqlNewsRepository();
        protected News latestNews;

        protected void Page_Load(object sender, EventArgs e)
        {
            latestNews = r.ReadLatest();
        }
    }
}