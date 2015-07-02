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

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);

            r.Exported(id);

            var invoice = r.Read(id);
            
            var exporter = new InvoiceExporter();
//            exporter.Export(invoice);
			Response.ContentType = exporter.Type;
			
			AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2);
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