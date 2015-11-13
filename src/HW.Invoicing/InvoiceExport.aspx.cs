using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class InvoiceExport : System.Web.UI.Page
    {
        SqlInvoiceRepository r = new SqlInvoiceRepository();
        SqlCompanyRepository cr = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            bool flatten = ConvertHelper.ToInt32(Request.QueryString["Flatten"]) == 1;

            r.Exported(id);

            var invoice = r.Read(id);
            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            var company = cr.Read(companyId);
            
            invoice.Company = company;
            if (company.HasInvoiceLogo)
            {
                company.InvoiceLogo = string.Format(Server.MapPath("~/uploads/{0}"), company.InvoiceLogo);
            }
            else
            {
                company.InvoiceLogo = Server.MapPath("~/img/ihg.png");
            }

            Response.ClearHeaders();
            Response.ClearContent();
            Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;

            string file = string.Format("{0} {1} {2} {3}", invoice.Number, invoice.Customer.Name, invoice.Customer.YourReferencePerson, DateTime.Now.ToString("MMM yyyy"));
            Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.pdf\";", file));

            string templateFileName = company.HasInvoiceTemplate ? string.Format(Server.MapPath("~/uploads/{0}"), company.InvoiceTemplate) : Server.MapPath(@"IHG faktura MALL Ian without comments.pdf");
            
            //var exporter = InvoiceExporterFactory.GetExporter(company.HasInvoiceTemplate ? companyId : InvoiceExporterFactory.IHGF);
            var exporter = InvoiceExporterFactory.GetExporter2(company.InvoiceExporter);

            var exported = exporter.Export(invoice, templateFileName, Server.MapPath(@"arial.ttf"), flatten);
            exported.WriteTo(Response.OutputStream);

            Response.Flush();
            Response.Close();
            Response.End();
		}
    }
}