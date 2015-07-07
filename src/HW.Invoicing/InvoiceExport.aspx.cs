﻿using System;
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
            //Response.ContentType = "application/pdf";
            Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;

            string file = string.Format("{0} {1} {2} {3}", invoice.Number, company.Name, invoice.Customer.YourReferencePerson, DateTime.Now.ToString("MMM yyyy"));
            Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.pdf\";", file));

            /*using (FileStream f = new FileStream(Server.MapPath(@"test.pdf"), FileMode.Create, FileAccess.Write))
            {
                var s = exporter.Export(invoice, Server.MapPath(@"IHG faktura MALL Ian without comments.pdf"));
                s.WriteTo(f);
            }

            var output = new MemoryStream();
            using (FileStream f = new FileStream(Server.MapPath(@"test.pdf"), FileMode.Open))
            {
                f.CopyTo(output);
            }*/

            var exported = exporter.Export(invoice, Server.MapPath(@"IHG faktura MALL Ian without comments.pdf"));
            /*var output = new MemoryStream();
            output.Write(exported, 0, exported.Length);
            output.WriteTo(Response.OutputStream);*/
            exported.WriteTo(Response.OutputStream);

            Response.Flush();
            Response.Close();
            Response.End();
		}
    }
}