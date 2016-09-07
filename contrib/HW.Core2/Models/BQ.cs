using System;
	
namespace HW.Core2.Models
{
	public class BQ
	{
		public int BQID { get; set; }
		public string Internal { get; set; }
		public int Type { get; set; }
		public int ReqLength { get; set; }
		public int MaxLength { get; set; }
		public string DefaultVal { get; set; }
		public int Comparison { get; set; }
		public string MeasurementUnit { get; set; }
		public int Layout { get; set; }
		public string Variable { get; set; }
		public string InternalAggregate { get; set; }
		public int Restricted { get; set; }
		public int IncludeInDemographics { get; set; }

		public BQ()
		{
		}
	}
}
