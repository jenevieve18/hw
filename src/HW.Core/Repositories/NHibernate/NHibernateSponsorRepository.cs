using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateSponsorRepository : BaseNHibernateRepository<Sponsor>, ISponsorRepository
	{
		public NHibernateSponsorRepository()
		{
		}
		
		public override void SaveOrUpdate(Sponsor t)
		{
			base.SaveOrUpdate(t, "healthWatch");
		}
		
		public override IList<Sponsor> FindAll()
		{
			return NHibernateHelper.OpenSession("healthWatch").CreateCriteria(typeof(Sponsor)).List<Sponsor>();
		}
		
		public override Sponsor Read(int id)
		{
			return NHibernateHelper.OpenSession("healthWatch").Load<Sponsor>(id);
		}
		
		public SponsorBackgroundQuestion ReadSponsorBackgroundQuestion(int id)
		{
			return NHibernateHelper.OpenSession("healthWatch").Load<SponsorBackgroundQuestion>(id);
		}
		
		public void UpdateSponsorInviteSent(int userID, int sponsorInviteID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateNullUserForUserInvite(int userID)
		{
			throw new NotImplementedException();
		}
		
		public void Z(int sponsorInviteID, string previewExtendedSurveys)
		{
			throw new NotImplementedException();
		}
		
		public void SaveSponsorAdminFunction(SponsorAdminFunction f)
		{
			throw new NotImplementedException();
		}
		
		public void DeleteSponsorAdminFunction(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsor(Sponsor s)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsorExtendedSurvey(SponsorExtendedSurvey s)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateLastAllMessageSent(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsorLastLoginSent(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsorAdmin(SponsorAdmin a)
		{
			throw new NotImplementedException();
		}
		
		public void SaveSponsorAdmin(SponsorAdmin a)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateDeletedAdmin(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public bool SponsorAdminExists(int sponsorAdminID, string usr)
		{
			throw new NotImplementedException();
		}
		
		public int CountSentInvitesBySponsor(int sponsorID, DateTime dateSent)
		{
			throw new NotImplementedException();
		}
		
		public int CountCreatedInvitesBySponsor(int sponsorID, DateTime dateCreated)
		{
			throw new NotImplementedException();
		}
		
		public Sponsor X(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public Sponsor ReadSponsor(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorInvite ReadSponsorInvite(int sponsorInviteID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorInvite ReadSponsorInviteByUser(int userID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorInvite ReadSponsorInviteBySponsor(int inviteID, int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorInvite ReadSponsorInvite(string email, int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorInviteBackgroundQuestion ReadSponsorInviteBackgroundQuestion(int sponsorID, int userID, int bqID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password)
		{
			throw new NotImplementedException();
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID)
		{
			return Read<SponsorAdmin>(sponsorAdminID, "healthWatch");
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, int SAID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorAdmin ReadSponsorAdmin(string SKEY, string SAKEY, string SA, string SAID, string ANV, string LOS)
		{
			throw new NotImplementedException();
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID, string usr)
		{
			throw new NotImplementedException();
		}
		
		public SponsorProjectRoundUnit ReadSponsorProjectRoundUnit(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Sponsor> FindAndCountDetailsBySuperAdmin(int superAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorInviteBackgroundQuestion> FindInviteBackgroundQuestionsByUser(int userID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorAdminDepartment> FindAdminDepartmentBySponsorAdmin(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorAdminFunction> FindAdminFunctionBySponsorAdmin(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorBackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin(int superAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsor(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorProjectRoundUnit> FindDistinctRoundUnitsWithReportBySuperAdmin(int superAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public void SaveOrUpdate(SponsorAdmin t)
		{
			throw new NotImplementedException();
		}
		
		public void Delete(SponsorAdmin t)
		{
			throw new NotImplementedException();
		}
	}
}
