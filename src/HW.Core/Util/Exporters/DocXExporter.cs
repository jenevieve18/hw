﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using HW.Core.Models;
using HW.Core.Services;
using Novacode;

namespace HW.Core.Util.Exporters
{
	public class DocXStatsExporter : AbstractExporter
	{
		ReportPart r;
		IList<IReportPart> parts;
		ReportService service;
		string template;
		
		public DocXStatsExporter(ReportPart r, string template)
		{
			this.r = r;
			this.template = template;
		}
		
		public DocXStatsExporter(ReportService service, IList<IReportPart> parts, string template)
		{
			this.service = service;
			this.parts = parts;
			this.template = template;
		}
		
		public override string Type {
			get { return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=\"HealthWatch {0} {1}.docx\";", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=\"HealthWatch Survey {0}.docx\";", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
//		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			MemoryStream output = new MemoryStream();
//			using (DocX d = DocX.Load(template)) {
//				Paragraph header = d.Paragraphs[0];
//				header.Append(r.CurrentLanguage.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
//
//				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, fm, tm);
//
//				Paragraph image = d.InsertParagraph();
//				image.AppendPicture(CreatePicture(d, url));
//
//				d.SaveAs(output);
//			}
//			return output;
//		}
//
//		public override object ExportAll(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			MemoryStream output = new MemoryStream();
//			using (DocX d = DocX.Load(template)) {
//				int i = 0;
//				foreach (var p in parts) {
//					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
//					if (i == 0) {
//						Paragraph header = d.Paragraphs[0];
//						header.Append(r.CurrentLanguage.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
//					} else {
//						Paragraph header = d.InsertParagraph();
//						header.Append(r.CurrentLanguage.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
//					}
//
//					string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, fm, tm);
//					Paragraph image = d.InsertParagraph();
//					image.AppendPicture(CreatePicture(d, url));
//					image.InsertPageBreakAfterSelf();
//
//					i++;
//				}
//				d.SaveAs(output);
//			}
//
//			return output;
//		}
		
		public override object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			MemoryStream output = new MemoryStream();
			using (DocX d = DocX.Load(template)) {
				Paragraph header = d.Paragraphs[0];
				header.Append(r.SelectedReportPartLang.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
				
				Paragraph image = d.InsertParagraph();
				image.AppendPicture(CreatePicture(d, url));
				
				d.SaveAs(output);
			}
			return output;
		}
		
		public override object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			MemoryStream output = new MemoryStream();
			using (DocX d = DocX.Load(template)) {
				int i = 0;
				foreach (var p in parts) {
					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
					if (i == 0) {
						Paragraph header = d.Paragraphs[0];
						header.Append(p.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
					} else {
						Paragraph header = d.InsertParagraph();
						header.Append(p.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
					}
					
					if (r == null) {
						r = new ReportPart();
					}
					
					var e = new ReportPartEventArgs(r);
					OnUrlSet(e);
					string url = e.Url;
					
					Paragraph image = d.InsertParagraph();
					image.AppendPicture(CreatePicture(d, url));
//					image.InsertPageBreakAfterSelf();
//
//					i++;
					if (i++ < parts.Count - 1) {
						image.InsertPageBreakAfterSelf();
					}
				}
				d.SaveAs(output);
			}

			return output;
		}
		
		public override object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			MemoryStream output = new MemoryStream();
			using (DocX d = DocX.Load(template)) {
				Paragraph header = d.Paragraphs[0];
				header.Append(r.SelectedReportPartLang.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
				
				Paragraph image = d.InsertParagraph();
				image.AppendPicture(CreatePicture(d, url));
				
				d.SaveAs(output);
			}
			return output;
		}
		
		public override object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			MemoryStream output = new MemoryStream();
			using (DocX d = DocX.Load(template)) {
				int i = 0;
				foreach (var p in parts) {
					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
					if (i == 0) {
						Paragraph header = d.Paragraphs[0];
						header.Append(r.SelectedReportPartLang.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
					} else {
						Paragraph header = d.InsertParagraph();
						header.Append(r.SelectedReportPartLang.Subject).Font(new FontFamily("Calibri")).FontSize(14).Bold().Color(Color.SteelBlue);
					}
					
					var e = new ReportPartEventArgs(r);
					OnUrlSet(e);
					string url = e.Url;
					
					Paragraph image = d.InsertParagraph();
					image.AppendPicture(CreatePicture(d, url));
					image.InsertPageBreakAfterSelf();
					
					i++;
				}
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
	}
}
