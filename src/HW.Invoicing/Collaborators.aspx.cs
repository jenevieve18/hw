﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class Collaborators : System.Web.UI.Page
    {
        SqlUserRepository r = new SqlUserRepository();
        protected IList<User> users;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            users = r.FindCollaborators(companyId);
        }
    }
}