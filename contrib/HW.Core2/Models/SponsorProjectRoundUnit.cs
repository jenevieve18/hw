using System;
	
namespace HW.Core2.Models
{
	public class SponsorProjectRoundUnit
	{
		public int SponsorProjectRoundUnitID { get; set; }
		public int SponsorID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public string Nav { get; set; }
		public Guid SurveyKey { get; set; }
		public int SortOrder { get; set; }
		public string Feedback { get; set; }
		public int Ext { get; set; }
		public int SurveyID { get; set; }
		public int OnlyEveryDays { get; set; }
		public int GoToStatistics { get; set; }

		public SponsorProjectRoundUnit()
		{
		}
	}
}
