using System;

namespace HW.Core.Models
{
	// FIXME: This has conflict with eForm's User class. Verify!
	public class User : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual string Password { get; set; }
		public virtual string Email { get; set; }
		public virtual Department Department { get; set; }
		public virtual UserProfile Profile { get; set; }
		public virtual Sponsor Sponsor { get; set; }
		public virtual string AltEmail { get; set; }
		public virtual int ReminderLink { get; set; }
		public virtual string UserKey { get; set; }
	}
	
	public class UserMeasure : BaseModel
	{
		public virtual User User { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime Deleted { get; set; }
		public virtual UserProfile UserProfile { get; set; }
	}
	
	public class UserMeasureComponent : BaseModel
	{
		public virtual UserMeasure Measure { get; set; }
		public virtual MeasureComponent Component { get; set; }
		public virtual int IntegerValue { get; set; }
		public virtual decimal DecimalValue { get; set; }
		public virtual string StringValue { get; set; }
	}
	
	public class UserProfile : BaseModel
	{
		public virtual User User { get; set; }
		public virtual Sponsor Sponsor { get; set; }
		public virtual Department Department { get; set; }
		public virtual ProfileComparison Comparison { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual ProfileComparison ProfileComparison { get; set; }
	}
	
	public class UserProfileBackgroundQuestion : BaseModel
	{
		public virtual UserProfile Profile { get; set; }
		public virtual BackgroundQuestion BackgroundQuestion { get; set; }
		public virtual int ValueInt { get; set; }
		public virtual string ValueText { get; set; }
		public virtual DateTime? ValueDate { get; set; }
		
		public virtual double Average { get; set; }
		public virtual int Count { get; set; }
	}
	
	public class UserProjectRoundUser : BaseModel
	{
		public virtual User User { get; set; }
		public virtual ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserProjectRoundUserAnswer : BaseModel
	{
		public virtual DateTime Date { get; set; }
		public virtual UserProfile Profile { get; set; }
		public virtual Answer Answer { get; set; }
		public virtual UserProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserToken : BaseModel
	{
		public virtual string Token { get; set; }
		public virtual User Owner { get; set; }
		public virtual DateTime Expiry { get; set; }
	}
	
	public class UserCategory : BaseModel
	{
		public virtual string Internal { get; set; }
	}
	
	public class UserNote : BaseModel
	{
		public virtual User User { get; set; }
		public virtual string Note { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual SponsorAdmin SponsorAdmin { get; set; }
	}
	
	public class UserSponsorExtendedSurvey : BaseModel
	{
		public virtual User User { get; set; }
		public virtual Answer Answer { get; set; }
	}
}
