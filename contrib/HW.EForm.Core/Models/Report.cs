using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Report
	{
		public int ReportID { get; set; }
		public string Internal { get; set; }
		public Guid ReportKey { get; set; }

		public Report()
		{
		}
		
		public IList<ReportPart> Parts { get; set; }
	}
}
