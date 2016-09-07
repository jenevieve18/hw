using System;
	
namespace HW.Core2.Models
{
	public class SponsorExtendedSurveyDepartment
	{
		public int SponsorExtendedSurveyDepartmentID { get; set; }
		public int SponsorExtendedSurveyID { get; set; }
		public int DepartmentID { get; set; }
		public int RequiredUserCount { get; set; }
		public int Hide { get; set; }
		public int Ext { get; set; }
		public int Answers { get; set; }
		public int Total { get; set; }

		public SponsorExtendedSurveyDepartment()
		{
		}
	}
}
