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
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
	public partial class Customers : System.Web.UI.Page
	{
		protected IList<Customer> subscribers;
		protected IList<Customer> nonSubscribers;
		protected IList<Customer> deletedCustomers;
		CustomerService s = new CustomerService(new SqlCustomerRepository(), new SqlItemRepository());
		protected Company company;
		SqlCompanyRepository cr = new SqlCompanyRepository();
        protected string selectedTab;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
			company = cr.Read(companyId);
            selectedTab = Request.QueryString["SelectedTab"] == null ? "subscribed" : Request.QueryString["SelectedTab"];

			subscribers = s.FindSubscribersByCompany(companyId);
			nonSubscribers = s.FindNonSubscribersByCompany(companyId);
			deletedCustomers = s.FindDeletedCustomersByCompany(companyId);
		}

		public bool HasSubscribers
		{
			get { return subscribers.Count() > 0; }
		}
	}
}