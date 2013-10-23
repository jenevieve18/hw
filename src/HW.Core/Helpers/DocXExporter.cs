using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using HW.Core.Models;
using HW.Core.Services;
using Novacode;

namespace HW.Core.Helpers
{
	public class DocXExporter : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
        string template;
		
		public DocXExporter(ReportPart r, string template)
		{
			this.r = r;
            this.template = template;
		}
		
		public DocXExporter(ReportService service, IList<ReportPartLanguage> parts, string template)
		{
			this.service = service;
			this.parts = parts;
            this.template = template;
		}
		
		public override string Type {
			get { return "application/octet-stream"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=HealthWatch {0} {1}.docx;", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=HealthWatch Survey {0}.docx;", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot,	string path)
		{
			MemoryStream output = new MemoryStream();
			using (DocX d = DocX.Load(template)) {
				Paragraph header = d.Paragraphs[0];
				header.Append(r.CurrentLanguage.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
				
				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
				
				Paragraph image = d.InsertParagraph();
				image.AppendPicture(CreatePicture(d, url));
				
				d.SaveAs(output);
			}
			return output;
		}
		
		Novacode.Picture CreatePicture(DocX d, string url)
		{
			WebRequest req = WebRequest.Create(url);
			WebResponse response = req.GetResponse();
			Stream stream = response.GetResponseStream();
			System.Drawing.Image i = new Bitmap(stream);
			MemoryStream s = new MemoryStream();
			i.Save(s, ImageFormat.Jpeg);
			Novacode.Image img = d.AddImage(s);
			Novacode.Picture p = img.CreatePicture();
			p.Height = 288;
			p.Width = 576;
			return p;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path)
		{
			MemoryStream output = new MemoryStream();
			using (DocX d = DocX.Load(template)) {
				int i = 0;
				foreach (var p in parts) {
					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
					if (i == 0) {
						Paragraph header = d.Paragraphs[0];
						header.Append(r.CurrentLanguage.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
					} else {
						Paragraph header = d.InsertParagraph();
						header.Append(r.CurrentLanguage.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
					}
					
					string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
					Paragraph image = d.InsertParagraph();
					image.AppendPicture(CreatePicture(d, url));
					image.InsertPageBreakAfterSelf();
					
					i++;
				}
				d.SaveAs(output);
			}

			return output;
		}
	}
}
