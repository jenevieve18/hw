using System;
using System.Collections.Generic;
using HW.Core.Helpers;

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
		public DateTime? Date { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Deleted { get; set; }
		public UserProfile UserProfile { get; set; }
		public IList<UserMeasureComponent> Values { get; set; }
		public HWList GetIntValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add((double)v.ValueInt);
			}
			return new HWList(n);
		}
		public HWList GetDecimalValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add((double)v.ValueDecimal);
			}
			return new HWList(n);
		}
		public UserMeasure()
		{
			Values = new List<UserMeasureComponent>();
		}
	}
	
	public class UserMeasureComponent : BaseModel, IValue
	{
		public UserMeasure UserMeasure { get; set; }
		public MeasureComponent MeasureComponent { get; set; }
		public int ValueInt { get; set; }
		public decimal ValueDecimal { get; set; }
		public string ValueText { get; set; }
	}
	
	public class UserSponsorProject : BaseModel
	{
		public User User { get; set; }
		public SponsorProject SponsorProject { get; set; }
		public DateTime ContentDate { get; set; }
	}
	
	public class UserProfile : BaseModel
	{
		public User User { get; set; }
		public Sponsor Sponsor { get; set; }
		public Department Department { get; set; }
		public ProfileComparison Comparison { get; set; }
		public DateTime Created { get; set; }
		public ProfileComparison ProfileComparison { get; set; }
	}
	
	public class UserProfileBackgroundQuestion : BaseModel
	{
		public UserProfile Profile { get; set; }
		public BackgroundQuestion BackgroundQuestion { get; set; }
		public int ValueInt { get; set; }
		public string ValueText { get; set; }
		public DateTime? ValueDate { get; set; }
		
		public double Average { get; set; }
		public int Count { get; set; }
	}
	
	public class UserProjectRoundUser : BaseModel
	{
		public User User { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
	}
	
	public class UserProjectRoundUserAnswer : BaseModel
	{
		public DateTime Date { get; set; }
		public UserProfile Profile { get; set; }
		public Answer Answer { get; set; }
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
	
	public class UserSponsorExtendedSurvey : BaseModel
	{
		public User User { get; set; }
		public Answer Answer { get; set; }
	}
}
