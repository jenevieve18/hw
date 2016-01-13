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
using System.Web.Services;

namespace HW.Invoicing
{
	public partial class Invoices : System.Web.UI.Page
	{
		SqlInvoiceRepository r = new SqlInvoiceRepository();
		protected IList<Invoice> invoices;
		SqlCompanyRepository cr = new SqlCompanyRepository();
		protected Company company;
		int companyId;
		int latestInvoiceId;

		[WebMethod]
		public static string UpdateInternalComments(string comments, int id)
		{
			var d = new SqlInvoiceRepository();
			d.UpdateInternalComments(comments, id);
			return comments;
		}

		protected bool IsLatestInvoice(string invoiceNumber)
		{
			int number = 0;
			string[] numbers = invoiceNumber.Split('-');
			if (numbers.Length > 1) {
				number = ConvertHelper.ToInt32(numbers[1]);
			}
			return number == latestInvoiceId;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			if (Session["Message"] != null)
			{
				panelMessage.Visible = true;
				labelMessage.Text = Session["Message"].ToString();
				Session.Remove("Message");
			}

			companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
			company = cr.Read(companyId);

			latestInvoiceId = r.GetLatestInvoiceNumber(companyId) - 1;

			if (!IsPostBack)
			{
				dropDownListFinancialYear.Items.Clear();
				//int year = DateTime.Now.Year;
				//for (int i = year; i >= year - 5; i--) {
				//    dropDownListFinancialYear.Items.Add(new ListItem(string.Format("{0} - {1}", i, i + 1), i.ToString()));
				//}
//				int year = DateTime.Now.Year;
//				int i = 0;
				var years = new List<Year>();
				foreach (var y in r.FindDistinctYears())
				{
//					if (i++ <= 0)
//					{
//						year = y + 1;
//					}
//					dropDownListFinancialYear.Items.Add(new ListItem(string.Format("{0} - {1}", y - 1, y), (y + 1).ToString()));
					var dateFrom = new DateTime(y, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
					var dateTo = new DateTime(y + 1, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
					if (r.HasInvoicesByCompany(dateFrom, dateTo, companyId)) {
						years.Add(new Year { From = y, To = y + 1 });
					}
				}
//				var dateFrom = new DateTime(year - 1, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
//				var dateTo = new DateTime(year, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
//				invoices = r.FindByDateAndCompany(dateFrom, dateTo, companyId);
				
				Year year = null;
				int i = 0;
				foreach (var y in years) {
					if (i++ <= 0) {
						year = y;
					}
					dropDownListFinancialYear.Items.Add(new ListItem(string.Format("{0} - {1}", y.From, y.To), y.From.ToString()));
				}
				var d1 = new DateTime(year.From, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
				var d2 = new DateTime(year.To, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
				invoices = r.FindByDateAndCompany(d1, d2, companyId);
			}
		}
		
		class Year
		{
			public int From { get; set; }
			public int To { get; set; }
		}

		protected void dropDownListFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			int year = ConvertHelper.ToInt32(dropDownListFinancialYear.SelectedValue);
			
//			var dateFrom = new DateTime(year - 1, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
//			var dateTo = new DateTime(year, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
//			invoices = r.FindByDateAndCompany(dateFrom, dateTo, companyId);
			var dateFrom = new DateTime(year, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
			var dateTo = new DateTime(year + 1, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
			invoices = r.FindByDateAndCompany(dateFrom, dateTo, companyId);
		}
	}
}