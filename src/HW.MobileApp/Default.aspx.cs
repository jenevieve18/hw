﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Default : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}