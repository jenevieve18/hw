using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
	public partial class Users : System.Web.UI.Page
	{
		IUserRepository r;
		protected IList<User> users;
		
		public Users() : this(new SqlUserRepository())
		{
		}
		
		public Users(IUserRepository r)
		{
			this.r = r;
		}
		
		public void Index()
		{
			users = r.FindAll();
		}
		
		protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			Index();
		}
	}
}