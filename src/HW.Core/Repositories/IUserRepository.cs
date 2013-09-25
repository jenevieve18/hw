using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
		void SaveUserProfileBackgroundQuestion(UserProfileBackgroundQuestion s);
		
		void UpdateUserProfile(int userID, int sponsorID, int departmentID);
		
		void UpdateUser(int userID, int sponsorID, int departmentID);
		
		void UpdateLastReminderSent(int userID);
		
		void UpdateEmailFailure(int userID);
		
		void UpdateUserProjectRoundUser(int projectRoundUnitID, int userProjectRoundUserID);
		
		int a(int sponsorID, int sponsorAdminID);
		
		int CountBySponsorWithAdminAndExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID);
		
		int CountBySponsorWithAdminAndExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID);
		
		User ReadByIdAndSponsorExtendedSurvey(int userID, int sponsorExtendedSurveyID);
		
		User ReadById(int userID);
		
		IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID);
		
		IList<User> Find2(int sponsorID, int sponsorAdminID);
		
		IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int loginDays);
		
		IList<UserProjectRoundUser> FindUserProjectRoundUser(int sponsorID, int surveyID, int userID);
		
		IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID);
	}
	
	public class UserRepositoryStub : BaseRepositoryStub<User>, IUserRepository
	{
		public void SaveUserProfileBackgroundQuestion(UserProfileBackgroundQuestion s)
		{
		}
		
		public User ReadByIdAndSponsorExtendedSurvey(int userID, int sponsorExtendedSurveyID)
		{
			var s = new Sponsor();
			s.ExtendedSurveys = new List<SponsorExtendedSurvey>(
				new SponsorExtendedSurvey[] {
					new SponsorExtendedSurvey { ExtraEmailBody = "Email Body", ExtraEmailSubject = "Email Subject" }
				}
			);
			var u = new User {
				Sponsor = s,
				Email = "some@domain.com",
				Id = 1,
				ReminderLink = 1,
				UserKey = "UserKey"
			};
			return u;
		}
		
		public void UpdateUserProfile(int userID, int sponsorID, int departmentID)
		{
		}
		
		public void UpdateUser(int userID, int sponsorID, int departmentID)
		{
		}
		
		public User ReadById(int userID)
		{
			return new User {
				Id = 1,
				Sponsor = new Sponsor { Id = 1 }
			};
		}
		
		public int a(int sponsorID, int sponsorAdminID)
		{
			return 10;
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return 10;
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			return 10;
		}
		
		public IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int loginDays)
		{
			throw new NotImplementedException();
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateLastReminderSent(int userID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateEmailFailure(int userID)
		{
			throw new NotImplementedException();
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public IList<UserProjectRoundUser> FindUserProjectRoundUser(int sponsorID, int surveyID, int userID)
		{
			var users = new List<UserProjectRoundUser>();
			for (int i = 0; i < 10; i++) {
				var u = new UserProjectRoundUser {
					Id = 1,
					ProjectRoundUser = new ProjectRoundUser { Id = 1 }
				};
				users.Add(u);
			}
			return users;
		}
		
		public IList<User> Find2(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateUserProjectRoundUser(int projectRoundUnitID, int userProjectRoundUserID)
		{
		}
	}
}
