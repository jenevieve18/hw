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
    public partial class ItemAdd : System.Web.UI.Page
    {
    	SqlItemRepository r = new SqlItemRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var i = new Item {
        		Name = textBoxName.Text,
        		Description = textBoxDescription.Text
        	};
        	r.Save(i);
        	Response.Redirect("items.aspx");
        }
    }
}