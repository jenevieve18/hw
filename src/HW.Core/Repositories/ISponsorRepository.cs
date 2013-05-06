//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
//	public interface ISponsorRepository : IBaseRepository<SponsorAdmin>
	public interface ISponsorRepository : IBaseRepository<Sponsor>
	{
		void UpdateSponsorInviteSent(int userID, int sponsorInviteID);
		
		void UpdateNullUserForUserInvite(int userID);
		
		void Z(int sponsorInviteID, string previewExtendedSurveys);
		
		void SaveSponsorAdminFunction(SponsorAdminFunction f);
		
		void DeleteSponsorAdmin(int sponsorAdminID);
		
		void UpdateSponsor(Sponsor s);
		
		void UpdateSponsorExtendedSurvey(SponsorExtendedSurvey s);
		
		void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyID);
		
		void UpdateLastAllMessageSent(int sponsorID);
		
		void UpdateSponsorLastLoginSent(int sponsorID);
		
		void UpdateSponsorLastInviteReminderSent(int sponsorID);
		
		void UpdateExtendedSurveyLastEmailSent(int sponsorExtendedSurveyID);
		
		void UpdateSponsorLastInviteSent(int sponsorID);
		
		void UpdateSponsorAdmin(SponsorAdmin a);
		
		void SaveSponsorAdmin(SponsorAdmin a);
		
		void UpdateDeletedAdmin(int sponsorID, int sponsorAdminID);
		
		bool SponsorAdminExists(int sponsorAdminID, string usr);
		
		int CountSentInvitesBySponsor(int sponsorID, DateTime dateSent);
		
		int CountCreatedInvitesBySponsor(int sponsorID, DateTime dateCreated);
		
		Sponsor X(int sponsorID);
		
		Sponsor ReadSponsor(int sponsorID);
		
		SponsorInvite ReadSponsorInvite(int sponsorInviteID);
		
		SponsorInvite ReadSponsorInviteByUser(int userID);
		
		SponsorInvite ReadSponsorInviteBySponsor(int inviteID, int sponsorID);
		
		SponsorInvite ReadSponsorInvite(string email, int sponsorID);
		
		SponsorInviteBackgroundQuestion ReadSponsorInviteBackgroundQuestion(int sponsorID, int userID, int bqID);
		
		SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password);
		
		SponsorAdmin ReadSponsorAdmin(int sponsorAdminID);
		
		SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, int SAID);
		
		SponsorAdmin ReadSponsorAdmin(string SKEY, string SAKEY, string SA, string SAID, string ANV, string LOS);
		
		SponsorAdmin ReadSponsorAdmin(int sponsorAdminID, string usr);
		
		SponsorProjectRoundUnit ReadSponsorProjectRoundUnit(int sponsorID);
		
		SponsorBackgroundQuestion ReadSponsorBackgroundQuestion(int sponsorBQID);
		
//		SponsorAdmin ReadSponsorAdmin2(int sponsorAdminID, string usr);
		
//		IList<Sponsor> FindAllSponsors();
		
		IList<Sponsor> FindAndCountDetailsBySuperAdmin(int superAdminID);
		
		IList<SponsorInviteBackgroundQuestion> FindInviteBackgroundQuestionsByUser(int userID);
		
		IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID);
		
		IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID);
		
		IList<SponsorAdminDepartment> FindAdminDepartmentBySponsorAdmin(int sponsorAdminID);
		
		IList<SponsorAdminFunction> FindAdminFunctionBySponsorAdmin(int sponsorAdminID);
		
		IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID);
		
		IList<SponsorBackgroundQuestion> FindBackgroundQuestions(int sponsorID);
		
		IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorID, int langID);
		
		IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin(int superAdminID);
		
		IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsor(int sponsorID);
		
		IList<SponsorProjectRoundUnit> FindDistinctRoundUnitsWithReportBySuperAdmin(int superAdminID);
		
		IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID);
		
		IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID);
	}
	
//	public class SponsorRepositoryStub : BaseRepositoryStub<SponsorAdmin>, ISponsorRepository
	public class SponsorRepositoryStub : BaseRepositoryStub<Sponsor>, ISponsorRepository
	{
		public SponsorRepositoryStub()
		{
//			data.Add(new SponsorAdmin {
//	         	Name = "test",
//	         	Password = "",
//	         	SuperAdmin = false,
//	         	Sponsor = new Sponsor { Id = 1, Name = "test" },
//	         	Anonymized = true,
//	         	SeeUsers = true,
//	         	ReadOnly = false
//	         });
		}
		
		public void SaveSponsorAdminFunction(SponsorAdminFunction f)
		{
		}
		
		public void SaveSponsorAdmin(SponsorAdmin a)
		{
		}
		
		public void UpdateSponsorAdmin(SponsorAdmin a)
		{
		}
		
		public void DeleteSponsorAdmin(int sponsorAdminID)
		{
		}
		
		public void UpdateSponsorInviteSent(int userID, int sponsorInviteID)
		{
		}
		
		public void UpdateNullUserForUserInvite(int userID)
		{
		}
		
		public void UpdateDeletedAdmin(int sponsorID, int sponsorAdminID)
		{
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorID)
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
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateLastAllMessageSent(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public void UpdateSponsor(Sponsor s)
		{
		}
		
		public void UpdateSponsorExtendedSurvey(SponsorExtendedSurvey s)
		{
		}
		
		public void Z(int sponsorInviteID, string previewExtendedSurveys)
		{
		}
		
		public int CountSentInvitesBySponsor(int sponsorID, DateTime dateSent)
		{
			return 10;
		}
		
		public int CountCreatedInvitesBySponsor(int sponsorID, DateTime dateCreated)
		{
			return 10;
		}
		
		public bool SponsorAdminExists(int sponsorAdminID, string usr)
		{
			return usr == "ian";
		}
		
		public SponsorInvite ReadSponsorInviteByUser(int userID)
		{
			return new SponsorInvite {
				Email = "some@domain.com",
				Department = new Department { Id = 1 },
				StoppedReason = 1,
				Stopped = DateTime.Now
			};
		}
		
		public SponsorInviteBackgroundQuestion ReadSponsorInviteBackgroundQuestion(int sponsorID, int userID, int bqID)
		{
			return new SponsorInviteBackgroundQuestion {
			};
		}
		
		public SponsorInvite ReadSponsorInviteBySponsor(int inviteID, int sponsorID)
		{
			return new SponsorInvite {
				Email = "some@domain.com",
				InvitationKey = "InvitationKey",
				User = new User {
					Id = 1,
					ReminderLink = 1,
					UserKey = "UserKey"
				},
				Sponsor = new Sponsor {
					InviteText = "InviteText",
					InviteSubject = "InviteSubject",
					LoginText = "LoginText",
					LoginSubject = "LoginSubject"
				}
			};
		}
		
		public SponsorInvite ReadSponsorInvite(int sponsorInviteID)
		{
			return new SponsorInvite {
				Email = "some@domain.com"
			};
		}
		
		public SponsorInvite ReadSponsorInvite(string email, int sponsorID)
		{
			return new SponsorInvite {
				Id = 1,
				Email = "some@domain.com"
			};
		}
		
		public SponsorAdmin ReadSponsorAdmin(string SKEY, string SAKEY, string SA, string SAID, string ANV, string LOS)
		{
//			return data.Find(x => x.Name == ANV && x.Password == LOS);;
			return new SponsorAdmin {
				Name = "test",
				Password = "",
				SuperAdmin = false,
				Sponsor = new Sponsor { Id = 1, Name = "test" },
				Anonymized = true,
				SeeUsers = true,
				ReadOnly = false
			};
		}
		
		public Sponsor X(int sponsorID)
		{
			var s = new Sponsor();
			s.Name = "test";
			var u =  new SuperSponsor { Id = 1 };
			s.SuperSponsor = u;
			u.Languages = new List<SuperSponsorLanguage>(
				new SuperSponsorLanguage[] {
					new SuperSponsorLanguage { Header = "Header 1" }
				}
			);
			return s;
		}
		
		public Sponsor ReadSponsor(int sponsorID)
		{
			return new Sponsor {
				InviteText = "Invite Text",
				InviteReminderText = "Invite Reminder Text",
				LoginText = "Login Text",
				InviteSubject = "Invite Subject",
				InviteReminderSubject = "Invite Reminder Subject",
				LoginSubject = "Login Subject",
				InviteLastSent = DateTime.Now,
				InviteReminderLastSent = DateTime.Now,
				LoginLastSent = DateTime.Now,
				LoginDays = 1,
				LoginWeekday = 1,
				AllMessageSubject = "All Message Subject",
				AllMessageBody = "All Message Body",
				AllMessageLastSent = DateTime.Now,
			};
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID)
		{
			return new SponsorAdmin {
				SuperUser = true
			};
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, int SAID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorAdmin ReadSponsorAdmin2(int sponsorAdminID, string usr)
		{
			return new SponsorAdmin {
				Id = 1
			};
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID, string usr)
		{
			return new SponsorAdmin {
				Id = 1
			};
		}
		
		public SponsorProjectRoundUnit ReadSponsorProjectRoundUnit(int sponsorID)
		{
			return new SponsorProjectRoundUnit {
				Id = 1,
				Survey = new Survey { Id = 1 }
			};
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password)
		{
			return new SponsorAdmin {
				Id = 1
			};
		}
		
		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorInviteBackgroundQuestion> FindInviteBackgroundQuestionsByUser(int userID)
		{
			var invites = new List<SponsorInviteBackgroundQuestion>();
			for (int i = 0; i < 10; i++) {
				var q = new SponsorInviteBackgroundQuestion {
					Question = new BackgroundQuestion { Id = 1, Type = 1, Restricted = 1 },
					Answer = new BackgroundAnswer { Id = 1 },
					ValueInt = i,
					ValueDate = DateTime.Now,
					ValueText = "Text " + i
				};
				invites.Add(q);
			}
			return invites;
		}
		
		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID)
		{
			return new List<SponsorBackgroundQuestion>(
				new SponsorBackgroundQuestion[] {
					new SponsorBackgroundQuestion { Id = 1, BackgroundQuestion = new BackgroundQuestion { Internal = "Test" }}
				}
			);
		}
		
		public IList<SponsorBackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			return new List<SponsorBackgroundQuestion>(
				new SponsorBackgroundQuestion[] {
					new SponsorBackgroundQuestion { Id = 1, BackgroundQuestion = new BackgroundQuestion { Internal = "Test" }}
				}
			);
		}
		
		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorID, int langID)
		{
			return new List<SponsorProjectRoundUnit>(
				new SponsorProjectRoundUnit[] {
					new SponsorProjectRoundUnit {
						Id = 1,
						Navigation = "Health & Stress",
						ProjectRoundUnit = new ProjectRoundUnit { Id = 1 }
					}
				}
			);
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin(int superAdminID)
		{
			var surveys = new List<SponsorExtendedSurvey>();
			for (int i = 0; i < 10; i++) {
				var v = new Survey { Id = 1, Internal = "Survey " + i };
				var u = new ProjectRoundUnit { Id = 1, Survey = v };
				u.Answers = new List<Answer>(10);
				var s = new SponsorExtendedSurvey {
					Sponsor = new Sponsor { Name = "Sponsor " + i },
					ProjectRoundUnit = u,
					Internal = "Internal " + i,
					RoundText = "RoundText " + i
				};
				surveys.Add(s);
			}
			return surveys;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsor(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorProjectRoundUnit> FindDistinctRoundUnitsWithReportBySuperAdmin(int superAdminID)
		{
			var units = new List<SponsorProjectRoundUnit>();
			for (int i = 0; i < 10; i++) {
				var p = new ProjectRoundUnit {
					Id = i,
					Report = new Report { Id = 1, Internal = "Internal " + i },
					Answers = new List<Answer>(10)
				};
				var u = new SponsorProjectRoundUnit {
					Sponsor = new Sponsor { Name = "Sponsor " + i },
					ProjectRoundUnit = p,
					Navigation = "Navigation " + i
				};
				units.Add(u);
			}
			return units;
		}
		
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID)
		{
			var admins = new List<SponsorAdmin>();
			for (int i = 0; i < 10; i++) {
				var a = new SponsorAdmin {
					Id = i,
					Usr = "Usr " + i,
					Name = "Name " + i,
					ReadOnly = true
				};
			}
			return admins;
		}
		
		public IList<Sponsor> FindAndCountDetailsBySuperAdmin(int superAdminID)
		{
			var sponsors = new List<Sponsor>();
			for (int i = 0; i < 10; i++) {
				var s = new Sponsor {
					Id = i,
					Name = "Sponsor " + i,
					SponsorKey = "Sponsor Key " + i,
					ClosedAt = DateTime.Now,
					MinimumInviteDate = DateTime.Now
				};
				s.ExtendedSurveys = new List<SponsorExtendedSurvey>(10);
				s.SentInvites = new List<SponsorInvite>(10);
				s.ActiveInvites = new List<SponsorInvite>(10);
				s.SuperAdminSponsors = new List<SuperAdminSponsor>(
					new SuperAdminSponsor[] {
						new SuperAdminSponsor { SeeUsers = true }
					}
				);
				s.Invites = new List<SponsorInvite>(10);
				sponsors.Add(s);
			}
			return sponsors;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			var surveys = new List<SponsorExtendedSurvey>();
			for (int i = 0; i < 10; i++) {
				var s = new SponsorExtendedSurvey {
					ProjectRoundUnit = new ProjectRoundUnit { Id = 1 },
					EmailSubject = "Email Subject " + i,
					EmailBody = "Email Body " + i,
					EmailLastSent = DateTime.Now,
					Internal = "Internal " + i,
					Id = i,
					FinishedEmailSubject = "Finished Email Subject " + i,
					FinishedEmailBody = "Finished Email Body " + i,
					RoundText = "Round Text " + i
				};
				surveys.Add(s);
			}
			return surveys;
		}
		
		public IList<SponsorAdminDepartment> FindAdminDepartmentBySponsorAdmin(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public IList<SponsorAdminFunction> FindAdminFunctionBySponsorAdmin(int sponsorAdminID)
		{
			throw new NotImplementedException();
		}
		
		public SponsorBackgroundQuestion ReadSponsorBackgroundQuestion(int sponsorBQID)
		{
			throw new NotImplementedException();
		}
	}
}
