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

        [WebMethod]
        public static string UpdateInternalComments(string comments, int id)
        {
            var d = new SqlInvoiceRepository();
            d.UpdateInternalComments(comments, id);
            return comments;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
			HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx?r=" + HttpUtility.UrlEncode("invoices.aspx"));

            company = cr.Read(1);
            if (!IsPostBack)
            {
                dropDownListFinancialYear.Items.Clear();
                int year = DateTime.Now.Year;
                for (int i = year; i >= year - 5; i--) {
                    dropDownListFinancialYear.Items.Add(new ListItem(string.Format("{0} - {1}", i, i + 1), i.ToString()));
                }
                var dateFrom = new DateTime(year, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
                var dateTo = new DateTime(year + 1, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
                invoices = r.FindByDate(dateFrom, dateTo);
            }
        }

        protected void dropDownListFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = ConvertHelper.ToInt32(dropDownListFinancialYear.SelectedValue);
            var dateFrom = new DateTime(year, company.FinancialMonthStart.Value.Month, company.FinancialMonthStart.Value.Day, 0, 0, 0);
            var dateTo = new DateTime(year + 1, company.FinancialMonthEnd.Value.Month, company.FinancialMonthEnd.Value.Day, 23, 59, 59);
            invoices = r.FindByDate(dateFrom, dateTo);
        }
    }
}