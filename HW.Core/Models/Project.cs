//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Project : BaseModel
	{
		public string Internal { get; set; }
		public string Name { get; set; }
		public string AppURL { get; set; }
		public IList<ProjectRound> Rounds { get; set; }
	}
	
	public class ProjectRound : BaseModel
	{
		public Project Project { get; set; }
		public string Internal { get; set; }
		public DateTime? Started { get; set; }
		public DateTime? Closed { get; set; }
		public Report Report { get; set; }
		public IList<ProjectRoundLanguage> Languages { get; set; }
	}
	
	public class ProjectRoundLanguage : BaseModel
	{
		public ProjectRound Round { get; set; }
		public Language Language { get; set; }
	}
	
	public interface IHasLanguage
	{
		Language Language { get; set; }
	}
	
	public class ProjectRoundUnit : BaseModel, IHasLanguage
	{
		public string Name { get; set; }
		public Report Report { get; set; }
		public string SortString { get; set; }
		public Language Language { get; set; }
		public IList<Answer> Answers { get; set; }
		public Survey Survey { get; set; }
		
		public string TreeString { get; set; } // TODO: This comes from cf_projectUnitTree function.
	}
	
	public class ProjectRoundUnitManager : BaseModel
	{
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class ProjectRoundUser : BaseModel
	{
		public ProjectRound ProjectRound { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public UserCategory UserCategory { get; set; }
		public string Email { get; set; }
		
		public string SomeText { get; set; }
	}
	
	public class ProjectUnitCategory : BaseModel
	{
		public Project Project { get; set; }
	}
	
	public class ProjectUserCategory : BaseModel
	{
	}
}
