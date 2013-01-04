using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}