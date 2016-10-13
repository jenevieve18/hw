using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core.Util.Exporters
{
    public interface IExporter
    {
        string Type { get; }

        bool HasContentDisposition(string file);

        string GetContentDisposition(string file);

        string ContentDisposition2 { get; }

        bool HasContentDisposition2 { get; }

        object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);

        object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);

        object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);

        object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);

        event EventHandler<ReportPartEventArgs> UrlSet;

        event EventHandler<ExcelCellEventArgs> CellWrite;
    }
    
	public abstract class AbstractExporter : IExporter
	{
		public abstract string Type { get; }
		
		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
		public event EventHandler<ExcelCellEventArgs> CellWrite;
		
		protected virtual void OnCellWrite(ExcelCellEventArgs e)
		{
			if (CellWrite != null) {
				CellWrite(this, e);
			}
		}
		
		public event EventHandler<ReportPartEventArgs> UrlSet;
		
		protected virtual void OnUrlSet(ReportPartEventArgs e)
		{
			if (UrlSet != null) {
				UrlSet(this, e);
			}
		}
		
		public abstract string GetContentDisposition(string file);
		
		public bool HasContentDisposition2 {
			get { return ContentDisposition2.Length > 0; }
		}
		
		public abstract string ContentDisposition2 { get; }
		
		public abstract object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);
		
		public abstract object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);
		
		public abstract object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
		
		public abstract object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
	}

	public abstract class BinaryExporter : AbstractExporter
	{
		public override string Type {
			get { return "application/octet-stream"; }
		}
	}
	
	public class Distribution
	{
		public const int None = 0;
		public const int StandardDeviation = 1;
		public const int ConfidenceInterval = 2;
	}
}
