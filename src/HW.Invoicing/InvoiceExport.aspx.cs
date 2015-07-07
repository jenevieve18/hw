using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

            r.Exported(id);

            var invoice = r.Read(id);
            var company = cr.Read(1);

            var exporter = new InvoiceExporter();
            
            Response.ClearHeaders();
            Response.ClearContent();
			Response.ContentType = exporter.Type;

            string file = string.Format("{0} {1} {2} {3}", invoice.Number, company.Name, invoice.Customer.YourReferencePerson, DateTime.Now.ToString("MMM yyyy"));
			AddHeaderIf(exporter.HasContentDisposition(file), "content-disposition", exporter.GetContentDisposition(file));
			Write(exporter.Export(invoice, Server.MapPath(@"IHG faktura MALL Ian without comments.pdf")));
		}
		
		void Write(object obj)
		{
			if (obj is MemoryStream) {
				Response.BinaryWrite(((MemoryStream)obj).ToArray());
				Response.End();
			} else if (obj is string) {
				Response.Write((string)obj);
			}
		}

        void AddHeaderIf(bool condition, string name, string value)
        {
            if (condition)
            {
                Response.AddHeader(name, value);
            }
        }
    }
}