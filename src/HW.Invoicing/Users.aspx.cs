﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
	public partial class Users : System.Web.UI.Page
	{
		SqlUserRepository r = new SqlUserRepository();
		protected IList<User> users;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			users = r.FindAll();
		}
	}
}