using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
	public partial class Items : System.Web.UI.Page
	{
		IItemRepository r;
		protected IList<Item> items;
		
		public Items() : this(new SqlItemRepository())
		{
		}
		
		public Items(IItemRepository r)
		{
			this.r = r;
		}
		
		public void Index()
		{
			items = r.FindAll();
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			Index();
		}
	}
}