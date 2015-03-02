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
	public partial class Items : System.Web.UI.Page
	{
		SqlItemRepository r = new SqlItemRepository();
		protected IList<Item> items;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			items = r.FindAll();
		}
	}
}