using System;
	
namespace HW.Core2.Models
{
	public class User
	{
		public int UserID { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime? Created { get; set; }
		public Guid UserKey { get; set; }
		public int SponsorID { get; set; }
		public int Reminder { get; set; }
		public DateTime? AttitudeSurvey { get; set; }
		public int UserProfileID { get; set; }
		public int DepartmentID { get; set; }
		public DateTime? ReminderLastSent { get; set; }
		public DateTime? EmailFailure { get; set; }
		public int ReminderType { get; set; }
		public int ReminderLink { get; set; }
		public string ReminderSettings { get; set; }
		public DateTime? ReminderNextSend { get; set; }
		public int LID { get; set; }
		public string AltEmail { get; set; }

		public User()
		{
		}
	}
}
