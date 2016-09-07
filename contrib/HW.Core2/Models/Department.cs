using System;
	
namespace HW.Core2.Models
{
	public class Department
	{
		public int DepartmentID { get; set; }
		public int SponsorID { get; set; }
		public string DepartmentName { get; set; }
		public int ParentDepartmentID { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public string DepartmentShort { get; set; }
		public string DepartmentAnonymized { get; set; }
		public int PreviewExtendedSurveys { get; set; }
		public int MinUserCountToDisclose { get; set; }
		public int LoginDays { get; set; }
		public int LoginWeekday { get; set; }
		public int SortStringLength { get; set; }

		public Department()
		{
		}
	}
}
