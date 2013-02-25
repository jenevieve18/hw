//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	// FIXME: This has conflict with eForm's User class. Verify!
	public class User : BaseModel
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public Department Department { get; set; }
		public UserProfile Profile { get; set; }
		public Sponsor Sponsor { get; set; }
		public string AltEmail { get; set; }
		public int ReminderLink { get; set; }
		public string UserKey { get; set; }
	}
	
	public class UserMeasure : BaseModel
	{
		public User User { get; set; }
		public DateTime Created { get; set; }
		public DateTime Deleted { get; set; }
		public UserProfile UserProfile { get; set; }
	}
	
	public class UserMeasureComponent : BaseModel
	{
		public UserMeasure Measure { get; set; }
		public MeasureComponent Component { get; set; }
		public int IntegerValue { get; set; }
		public decimal DecimalValue { get; set; }
		public string StringValue { get; set; }
	}
	
	public class UserProfile : BaseModel
	{
		public User User { get; set; }
		public Sponsor Sponsor { get; set; }
		public Department Department { get; set; }
		public ProfileComparison Comparison { get; set; }
		public DateTime Created { get; set; }
	}
	
	public class UserProfileBackgroundQuestion : BaseModel
	{
		public UserProfile Profile { get; set; }
		public BackgroundQuestion Question { get; set; }
		public int ValueInt { get; set; }
		public string ValueText { get; set; }
		public DateTime? ValueDate { get; set; }
	}
	
	public class UserProjectRoundUser : BaseModel
	{
		public User User { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserProjectRoundUserAnswer : BaseModel
	{
		public DateTime Date { get; set; }
		public UserProfile Profile { get; set; }
		public Answer Answer { get; set; }
//		public ProjectRoundUser ProjectRoundUser { get; set; }
		public UserProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserToken : BaseModel
	{
		public string Token { get; set; }
		public User Owner { get; set; }
		public DateTime Expiry { get; set; }
	}
	
	public class UserCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class UserNote : BaseModel
	{
		public User User { get; set; }
		public string Note { get; set; }
		public DateTime Date { get; set; }
		public SponsorAdmin SponsorAdmin { get; set; }
	}
}
