using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class InvoiceRevert : System.Web.UI.Page
    {
        SqlInvoiceRepository invoiceRepository = new SqlInvoiceRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int invoiceId = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            
            var invoice = invoiceRepository.Read(invoiceId);
            invoiceRepository.Revert(invoice);
            
            Response.Redirect("invoices.aspx");
        }
    }
}