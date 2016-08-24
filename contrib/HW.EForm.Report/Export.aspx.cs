using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Report
{
	public partial class Export : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var exporter = new PromasExporter();
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition("test"), "content-disposition", exporter.GetContentDisposition("test"), Response);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

			HtmlHelper.Write(exporter.Export(), Response);
		}
	}
}