﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class InvoiceEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");
        }
    }
}