using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public interface IHasLanguage
	{
		Language Language { get; set; }
	}
	
	public class Project : BaseModel
	{
		public virtual string Internal { get; set; }
		public virtual string Name { get; set; }
		public virtual string AppURL { get; set; }
		public virtual IList<ProjectRound> Rounds { get; set; }
	}
	
	public class ProjectRound : BaseModel
	{
		public virtual Project Project { get; set; }
		public virtual string Internal { get; set; }
		public virtual DateTime? Started { get; set; }
		public virtual DateTime? Closed { get; set; }
		public virtual Report Report { get; set; }
		public virtual IList<ProjectRoundLanguage> Languages { get; set; }
		public virtual IList<ProjectRoundUnit> Units { get; set; }
		public virtual Survey Survey { get; set; }
		public virtual List<Answer> Answers { get; set; }
		
		public bool IsOpen {
			get { return Closed == null || Closed >= DateTime.Now; }
		}
		
		public string ToPeriodString()
		{
			return (Started ==  null ? "?" : Started.Value.ToString("yyyy-MM-dd")) + "--" + (Closed == null ? "?" : Closed.Value.ToString("yyyy-MM-dd"));
		}
	}
	
	public class ProjectRoundLanguage : BaseModel
	{
		public virtual ProjectRound Round { get; set; }
		public virtual Language Language { get; set; }
		public virtual string InvitationSubject { get; set; }
	}
	
	public class ProjectRoundUnit : BaseModel, IHasLanguage
	{
		public virtual string Name { get; set; }
		public virtual Report Report { get; set; }
		public virtual string SortString { get; set; }
		public virtual Language Language { get; set; }
		public virtual List<Answer> Answers { get; set; }
		public virtual Survey Survey { get; set; }
		public virtual ProjectRound ProjectRound { get; set; }
		
		public virtual string TreeString { get; set; } // TODO: This comes from cf_projectUnitTree function.
	}
	
	public class ProjectRoundUnitManager : BaseModel
	{
		public virtual ProjectRoundUnit ProjectRoundUnit { get; set; }
		public virtual ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class ProjectRoundUser : BaseModel
	{
		public virtual ProjectRound ProjectRound { get; set; }
		public virtual ProjectRoundUnit ProjectRoundUnit { get; set; }
		public virtual UserCategory UserCategory { get; set; }
		public virtual string Email { get; set; }
		
		public virtual string SomeText { get; set; }
	}
	
	public class ProjectUnitCategory : BaseModel
	{
		public virtual Project Project { get; set; }
	}
	
	public class ProjectUserCategory : BaseModel
	{
	}
}
