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
    public partial class Companies : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();
        protected IList<Company> companies;

        protected void Page_Load(object sender, EventArgs e)
        {
            companies = r.FindAll();
        }
    }
}