using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HWgrp
{
	public partial class Export : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			int GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			//bool stdev = (HttpContext.Current.Request.QueryString["STDEV"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 : false);
			int stdev = Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]);
			
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			
			int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
			int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
			
			int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
			int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			
			string url = string.Format(
				@"{11}reportImage.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&STDEV={5}&GB={6}&RPID={7}&PRUID={8}&GID={9}&GRPNG={10}",
				langID,
				fy,
				ty,
				SPONS,
				SID,
				stdev,
				GB,
				rpid,
				PRUID,
				GID,
				GRPNG,
				Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
			);
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			
			Response.ContentType = "application/pdf";
//			Response.AddHeader("Content-Disposition", "attachment;filename=Export.pdf");
			Response.BinaryWrite(output.ToArray());
		}
	}
}