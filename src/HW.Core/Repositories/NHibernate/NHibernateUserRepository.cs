using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateUserRepository : BaseNHibernateRepository<User>, IUserRepository
	{
		public NHibernateUserRepository()
		{
		}
		
		public void SaveUserProfileBackgroundQuestion(UserProfileBackgroundQuestion s)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateUserProfile(int userID, int sponsorID, int departmentID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateUser(int userID, int sponsorID, int departmentID)
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
		
		public void UpdateUserProjectRoundUser(int projectRoundUnitID, int userProjectRoundUserID)
		{
			throw new NotImplementedException();
		}
		
		public int a(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public User ReadByIdAndSponsorExtendedSurvey(int userID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public User ReadById(int userID)
		{
			throw new NotImplementedException();
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public IList<User> Find2(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int loginDays)
		{
			throw new NotImplementedException();
		}
		
		public IList<UserProjectRoundUser> FindUserProjectRoundUser(int sponsorID, int surveyID, int userID)
		{
			throw new NotImplementedException();
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
	}
}
