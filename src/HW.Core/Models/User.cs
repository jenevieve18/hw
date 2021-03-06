﻿using System;
using System.Collections.Generic;
using HW.Core.Helpers;
using HW.Core.Util;

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
		
		public IList<SponsorProject> SponsorProjects { get; set; }
		public int ReminderType { get; set; }
		public string ReminderSettings { get; set; }
		public DateTime DT { get; set; }
		public int LID { get; set; }
		public int UserSponsorExtendedSurveyID { get; set; }
		public int AnswerID { get; set; }
	}
	
	public class UserRegistrationID : BaseModel
	{
		public User User { get; set; }
		public string RegistrationID { get; set; }
	}
	
	public class UserSession : BaseModel
	{
		public string HostAddress { get; set; }
		public string Agent { get; set; }
		public int Lang { get; set; }
	}
	
	public class UserMeasure : BaseModel, IAnswer
	{
		public User User { get; set; }
		public DateTime? Date { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Deleted { get; set; }
		public UserProfile UserProfile { get; set; }
		public IList<IValue> Values { get; set; }
		
		public UserMeasure()
		{
			Values = new List<IValue>();
		}
		
		public int DT { get; set; }
		public int DummyValue1 { get; set; } // TODO: This is used by dbo.cf_yearWeek and related methods
		public int DummyValue2 { get; set; }
		public int DummyValue3 { get; set; }
		
//		public HWList GetIntValues()
//		{
//			List<double> n = new List<double>();
//			foreach (var v in Values) {
//				n.Add((double)v.ValueInt);
//			}
//			return new HWList(n);
//		}
		
		public HWList GetDoubleValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add(v.ValueDouble);
			}
			return new HWList(n);
		}
	}
	
	public class UserMeasureComponent : BaseModel, IValue
	{
		public UserMeasure UserMeasure { get; set; }
		public MeasureComponent MeasureComponent { get; set; }
//		public int ValueInt { get; set; }
		public decimal ValueDecimal { get; set; }
		public string ValueText { get; set; }
		
		public double ValueDouble { get; set; }
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
