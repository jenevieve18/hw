using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ProjectRoundUnit
	{
		public ProjectRoundUnit()
		{
			Users = new List<ProjectRoundUser>();
		}
		
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundID { get; set; }
		public ProjectRound ProjectRound { get; set; }
		public string Unit { get; set; }
		public string ID { get; set; }
		public int ParentProjectRoundUnitID { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public int SurveyID { get; set; }
		public Survey Survey { get; set; }
		public int LangID { get; set; }
		public Guid UnitKey { get; set; }
		public int UserCount { get; set; }
		public int UnitCategoryID { get; set; }
		public bool CanHaveUsers { get; set; }
		public int ReportID { get; set; }
		public int Timeframe { get; set; }
		public int Yellow { get; set; }
		public int Green { get; set; }
		public string SurveyIntro { get; set; }
		public bool Terminated { get; set; }
		public int IndividualReportID { get; set; }
		public string UniqueID { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int SortStringLength { get; set; }
		public IList<ProjectRoundUser> Users { get; set; }
		
		public void AddUser(string email)
		{
			AddUser(new ProjectRoundUser { Email = email });
		}
		
		public void AddUser(ProjectRoundUser user)
		{
			user.ProjectRoundUnit = this;
			Users.Add(user);
		}
	}
}
