//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core;

namespace HW.Core
{
	public interface IRepositoryFactory
	{
		IExerciseRepository CreateExerciseRepository();
		
		ILanguageRepository CreateLanguageRepository();
		
		IDepartmentRepository CreateDepartmentRepository();
		
		IProjectRepository CreateProjectRepository();
		
		ISponsorRepository CreateSponsorRepository();
		
		IReportRepository CreateReportRepository();
		
		IAnswerRepository CreateAnswerRepository();
		
		IOptionRepository CreateOptionRepository();
		
		IIndexRepository CreateIndexRepository();
		
		IQuestionRepository CreateQuestionRepository();
		
		IManagerFunctionRepository CreateManagerFunctionRepository();
		
		IUserRepository CreateUserRepository();
	}
	
	public interface IBaseRepository<T>
	{
		void SaveOrUpdate(T t);
		
		void Delete(T t);
		
		T Read(int id);
		
		IList<T> FindAll();
	}
	
	public interface IIndexRepository : IBaseRepository<Index>
	{
		IList<Index> FindByLanguage(int id, int langID, int yearFrom, int yearTo, string sortString);
		
		Index ReadByIdAndLanguage(int idxID, int langID);
	}
	
	public interface IOptionRepository : IBaseRepository<Option>
	{
		int CountByOption(int optionID);
		
		IList<OptionComponentLanguage> FindComponentsByLanguage(int optionID, int langID);
	}
	
	public interface IReportRepository : IBaseRepository<Report>
	{
		ReportPart ReadReportPart(int reportPartID);
		
		ReportPartComponent ReadComponentByPartAndLanguage(int reportPartID, int langID);
		
		IList<ReportPartComponent> FindComponentsByPartAndLanguage(int reportPartID, int langID);
		
		IList<ReportPartComponent> FindComponentsByPartAndLanguage2(int reportPartID, int langID);
		
		IList<ReportPartComponent> FindComponentsByPart(int reportPartID);
		
		IList<ReportPartComponent> FindComponents(int reportPartID);
		
		IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID);
		
		IList<ReportPartLanguage> FindPartLanguagesByReport(int reportID);
	}
	
	public interface IExerciseRepository : IBaseRepository<Exercise>
	{
		IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort);
		
		IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID);
		
		IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID);
	}
	
	public interface IQuestionRepository : IBaseRepository<Question>
	{
		IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID);
		
		IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID);
	}
	
	public interface IAnswerRepository : IBaseRepository<Answer>
	{
		IList<BackgroundAnswer> FindBackgroundAnswers(int bqID);
		
		void UpdateAnswer(int projectRoundUnitID, int projectRoundUserID);
		
		IList<Answer> FindByProjectRound(int projectRoundID);
		
		IList<Answer> FindByQuestionAndOptionGrouped(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString);
		
		IList<Answer> FindByQuestionAndOptionJoinedAndGrouped(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo);
		
		IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo);
		
		Answer ReadByKey(string key);
		
		int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString);
		
		int CountByProject(int projectRoundUserID, int yearFrom, int yearTo);
		
		IList<Answer> FindByQuestionAndOptionWithYearSpan(int questionID, int optionID, int yearFrom, int yearTo);
		
		Answer ReadByQuestionAndOption(int answerID, int questionID, int optionID);
		
		Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString);
		
		Answer ReadMinMax(string groupBy, int questionID, int optionID, int yearFrom, int yearTO, string sortString);
		
		int CountByDate(int yearFrom, int yearTo, string sortString);
	}
	
	public interface ILanguageRepository : IBaseRepository<Language>
	{
		IList<SponsorProjectRoundUnitLanguage> FindBySponsor(int sponsorID);
	}
	
	public interface IProjectRepository : IBaseRepository<Project>
	{
		void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID);
		
		ProjectRound ReadRound(int projectRoundID);
		
		ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID);
		
		IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString);
		
		int CountForSortString(string sortString);
	}
	
	public interface IDepartmentRepository : IBaseRepository<Department>
	{
		void SaveSponsorAdminDepartment(SponsorAdminDepartment d);
		
		void UpdateDepartmentBySponsor(int sponsorID);
		
		void UpdateDepartment(Department d);
		
		void UpdateDepartment2(Department d);
		
		void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID);
		
		Department ReadBySponsor(int sponsorId);
		
		Department ReadByIdAndSponsor(int departmentID, int sponsorID);
		
		IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID);
		
		IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindIn(string rndsd2);
		
		IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID);
		
		IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorOrderedBySortString(int sponsorID);
		
		IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string GID);
		
		IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string GID);
		
		IList<Department> FindBySponsor(int sponsorID);
		
		IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID);
		
		IList<Department> FindBySponsorInDepth(int sponsorID);
	}
	
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
		
//		SponsorAdmin ReadSponsorAdmin2(int sponsorAdminID, string usr);
		
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
	
	public interface IManagerFunctionRepository : IBaseRepository<ManagerFunction>
	{
		ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID);
		
		IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID);
	}
	
	public class RepositoryFactoryStub : IRepositoryFactory
	{
		public IExerciseRepository CreateExerciseRepository()
		{
			return new ExerciseRepositoryStub();
		}
		
		public ILanguageRepository CreateLanguageRepository()
		{
			return new LanguageRepositoryStub();
		}
		
		public IDepartmentRepository CreateDepartmentRepository()
		{
			return new DepartmentRepositoryStub();
		}
		
		public IProjectRepository CreateProjectRepository()
		{
			return new ProjectRepositoryStub();
		}
		
		public ISponsorRepository CreateSponsorRepository()
		{
			return new SponsorRepositoryStub();
		}
		
		public IReportRepository CreateReportRepository()
		{
			return new ReportRepositoryStub();
		}
		
		public IAnswerRepository CreateAnswerRepository()
		{
			return new AnswerRepositoryStub();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			return new OptionRepositoryStub();
		}
		
		public IIndexRepository CreateIndexRepository()
		{
			return new IndexRepositoryStub();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new QuestionRepositoryStub();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new ManagerFunctionRepositoryStub();
		}
		
		public IUserRepository CreateUserRepository()
		{
			return new UserRepositoryStub();
		}
	}
	
	public class BaseRepositoryStub<T> : IBaseRepository<T>
	{
		IList<T> data;
		
		public IList<T> Data {
			get { return data; }
		}
		
		public BaseRepositoryStub()
		{
			data = new List<T>();
		}
		
		public void SaveOrUpdate(T t)
		{
			throw new NotImplementedException();
		}
		
		public void Delete(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual IList<T> FindAll()
		{
			throw new NotImplementedException();
		}
	}
	
	public class ExerciseRepositoryStub : BaseRepositoryStub<Exercise>, IExerciseRepository
	{
		public IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort)
		{
			var exercises = new List<Exercise>();
			for (int i = 0; i < 7; i++) {
				var e = new Exercise();
				e.Id = i;
				e.Image = "";
				e.CurrentLanguage = new ExerciseLanguage {
					IsNew = true,
					ExerciseName = "Exercise " + i,
					Time = "5-10 min",
					Teaser = "Teaser " + i
				};
				e.CurrentArea = new ExerciseAreaLanguage {
					Id = 1,
					AreaName = "Area " + i
				};
				e.CurrentVariant = new ExerciseVariantLanguage {
					Id = i,
					File = "File " + i,
					Size = 100,
					Content = "Content " + i,
					ExerciseWindowX = 10,
					ExerciseWindowY = 10
				};
				e.CurrentType = new ExerciseTypeLanguage {
					TypeName = "Type " + i,
					SubTypeName = "Sub Type " + i
				};
				e.CurrentCategory = new ExerciseCategoryLanguage {
					CategoryName = "Category " + i
				};
				exercises.Add(e);
			}
			return exercises;
		}
		
		public IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID)
		{
			var areas = new List<ExerciseAreaLanguage>();
			for (int i = 0; i < 10; i++) {
				var a = new ExerciseAreaLanguage {
					Area = new ExerciseArea { Id = i },
					AreaName = "Area Name " + i,
				};
				areas.Add(a);
			}
			return areas;
		}
		
		public IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID)
		{
			var categories = new List<ExerciseCategoryLanguage>();
			for (int i = 0; i < 10; i++) {
				var c = new ExerciseCategoryLanguage {
					Category = new ExerciseCategory { Id = i },
					CategoryName = "Category " + i
				};
				categories.Add(c);
			}
			return categories;
		}
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
	
	public class QuestionRepositoryStub : BaseRepositoryStub<Question>, IQuestionRepository
	{
		public IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			var questions = new List<BackgroundQuestion>();
			for (int i = 0; i < 10; i++) {
				var q = new BackgroundQuestion {
					Internal = "Internal " + i,
					Id = i,
					Type = 1
				};
				questions.Add(q);
			}
			return questions;
		}
		
		public IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID)
		{
			var questions = new List<BackgroundQuestion>();
			for (int i = 0; i < 10; i++) {
				var q = new BackgroundQuestion {
					Id = i,
					Internal = "Internal " + i
				};
				questions.Add(q);
			}
			return questions;
		}
	}
	
	public class IndexRepositoryStub : BaseRepositoryStub<Index>, IIndexRepository
	{
		public IList<Index> FindByLanguage(int id, int langID, int yearFrom, int yearTo, string sortString)
		{
			var indexes = new List<Index>();
			for (int j = 0; j < 10; j++) {
				var i = new Index();
				i.AverageAX = 50;
				i.Languages = new List<IndexLanguage>(
					new IndexLanguage[] {
						new IndexLanguage { IndexName = "Index " + j }
					}
				);
				i.Id = j;
				i.CountDX = 10;
				indexes.Add(i);
			}
			return indexes;
		}
		
		public Index ReadByIdAndLanguage(int idxID, int langID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class OptionRepositoryStub : BaseRepositoryStub<Option>, IOptionRepository
	{
		public int CountByOption(int optionID)
		{
			return 10;
		}
		
		public IList<OptionComponentLanguage> FindComponentsByLanguage(int optionID, int langID)
		{
			var components = new List<OptionComponentLanguage>();
			for (int i = 0; i < 10; i++) {
				var c = new OptionComponentLanguage {
					Text = "Text " + i,
					Component = new OptionComponent { Id = 1 }
				};
				components.Add(c);
			}
			return components;
		}
	}
	
	public class ReportRepositoryStub : BaseRepositoryStub<Report>, IReportRepository
	{
		public ReportPartComponent ReadComponentByPartAndLanguage(int reportPartID, int langID)
		{
			var c = new ReportPartComponent();
			c.QuestionOption = new WeightedQuestionOption {
				Id = 1,
				YellowLow = 40,
				GreenLow = 60,
				GreenHigh = 101,
				YellowHigh = 101,
				Question = new Question { Id = 1 },
				Option = new Option { Id = 1 }
			};
			return c;
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage(int reportPartID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage2(int reportPartID, int langID)
		{
			var components = new List<ReportPartComponent>();
			for (int i = 0; i < 10; i++) {
				var c = new ReportPartComponent();
				var q = new WeightedQuestionOption {
					Id = i,
					TargetValue = 10,
					YellowLow = 40,
					GreenLow = 60,
					GreenHigh = 101,
					YellowHigh = 101,
					Question = new Question { Id = 1 },
					Option = new Option { Id = 1 }
				};
				q.Languages = new List<WeightedQuestionOptionLanguage>(
					new WeightedQuestionOptionLanguage[] {
						new WeightedQuestionOptionLanguage { Question = "Question " + i }
					}
				);
				c.QuestionOption = q;
				components.Add(c);
			}
			return components;
		}
		
		public IList<ReportPartComponent> FindComponentsByPart(int reportPartID)
		{
			var components = new List<ReportPartComponent>();
			for (int i = 0; i < 10; i++) {
				var c = new ReportPartComponent();
				c.QuestionOption = new WeightedQuestionOption {
					Id = i,
					YellowLow = 40,
					GreenLow = 60,
					GreenHigh = 101,
					YellowHigh = 101,
					Question = new Question { Id = 1 },
					Option = new Option { Id = 1 }
				};
				components.Add(c);
			}
			return components;
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			var languages = new List<ReportPartLanguage>();
			var r = new Random();
			int[] types = new int[] { 2, 8, 9};
			for (int i = 0; i < 10; i++) {
				var l = new ReportPartLanguage {
					ReportPart = new ReportPart { Id = i, Type = types[r.Next(0, types.Length)] },
					Subject = "Subject " + i,
					Header = "Header " + i,
					Footer = "Footer " + i
				};
				languages.Add(l);
			}
			return languages;
		}
		
		public ReportPart ReadReportPart(int reportPartID)
		{
			int[] types = new int[] { 2, 8, 9 };
			return new ReportPart {
//				Type = types[new Random().Next(0, types.Length)],
				Type = 8,
				Components = new List<ReportPartComponent>(10),
				Question = new Question { Id = 1 },
				Option = new Option { Id = 1 },
				RequiredAnswerCount = 1,
				PartLevel = 1
			};
		}
		
		public IList<ReportPartComponent> FindComponents(int reportID)
		{
			var components = new List<ReportPartComponent>();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var c = new ReportPartComponent();
				c.Id = i;
				c.Index = new Index {
					TargetValue = r.Next(0, 10),
					YellowLow = 40,
					GreenLow = 60,
					GreenHigh = 101,
					YellowHigh = 101
				};
				c.Index.Parts = new List<IndexPart>(10);
				components.Add(c);
			}
			return components;
		}
		
		public IList<ReportPartLanguage> FindPartLanguagesByReport(int reportID)
		{
			var languages = new List<ReportPartLanguage>();
			for (int i = 0; i < 10; i++) {
				var p = new ReportPartLanguage {
					ReportPart = new ReportPart { Id = i, Type = 3 },
					Subject = "Subject " + i,
					Header = "Header " + i,
					Footer = "Footer " + i,
				};
				languages.Add(p);
			}
			return languages;
		}
	}
	
	public class ManagerFunctionRepositoryStub : BaseRepositoryStub<ManagerFunction>, IManagerFunctionRepository
	{
		public ManagerFunctionRepositoryStub()
		{
//			Data.Add(new ManagerFunction { URL = "org.aspx", Function = "Organization", Expl = "Organization" });
			Data.Add(new ManagerFunction { URL = "stats.aspx", Function = "Statistics", Expl = "Statistics" });
			Data.Add(new ManagerFunction { URL = "messages.aspx", Function = "Messages", Expl = "Messages" });
			Data.Add(new ManagerFunction { URL = "managers.aspx", Function = "Managers", Expl = "Managers" });
			Data.Add(new ManagerFunction { URL = "debug.aspx", Function = "TEST", Expl = "TEST" });
			Data.Add(new ManagerFunction { URL = "exercise.aspx", Function = "Exercises", Expl = "Exercises" });
		}
		
		public override IList<ManagerFunction> FindAll()
		{
			return Data;
		}
		
		public ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID)
		{
			return Data[0];
		}
		
		public IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID)
		{
			return Data;
		}
	}
	
	public class SponsorRepositoryStub : BaseRepositoryStub<Sponsor>, ISponsorRepository
	{
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
			return new SponsorAdmin {
				Name = ANV,
				SuperAdmin = false,
				Sponsor = new Sponsor { Id = 1, Name = ANV },
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
					new SponsorBackgroundQuestion { Id = 1, Question = new BackgroundQuestion { Internal = "Test" }}
				}
			);
		}
		
		public IList<SponsorBackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			return new List<SponsorBackgroundQuestion>(
				new SponsorBackgroundQuestion[] {
					new SponsorBackgroundQuestion { Id = 1, Question = new BackgroundQuestion { Internal = "Test" }}
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
				var v = new Survey { Id = 1, Name = "Survey " + i };
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
	}
	
	public class ProjectRepositoryStub : BaseRepositoryStub<Project>, IProjectRepository
	{
		public void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID)
		{
		}
		
		public ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID)
		{
			return new ProjectRoundUnit {
				SortString = "SortString",
				Language = new Language { Id = 1 }
			};
		}
		
		public IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString)
		{
			var units = new List<ProjectRoundUnit>();
			for (int i = 0; i < 10; i++) {
				var u = new ProjectRoundUnit() {
					TreeString = ">> Tree " + i,
					SortString = "Sort String " + i
				};
				units.Add(u);
			}
			return units;
		}
		
		public int CountForSortString(string sortString)
		{
			return 10;
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			var p = new ProjectRound {
				Started = DateTime.Now,
				Closed = DateTime.Now
			};
			return p;
		}
	}
	
	public class AnswerRepositoryStub : BaseRepositoryStub<Answer>, IAnswerRepository
	{
		public IList<BackgroundAnswer> FindBackgroundAnswers(int bqID)
		{
			var answers = new List<BackgroundAnswer>();
			for (int i = 0; i < 10; i++) {
				var a = new BackgroundAnswer {
					Id = i,
					Internal = "Internal " + i
				};
				answers.Add(a);
			}
			return answers;
		}
		
		public void UpdateAnswer(int projectRoundUnitID, int projectRoundUserID)
		{
		}
		
		public IList<Answer> FindByProjectRound(int projectRoundID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Answer> FindByQuestionAndOptionGrouped(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 2; i++) {
				var a = new Answer {
					SomeInteger = 1,
					AverageV = r.Next(20, 100),
					CountV = 10,
					StandardDeviation = 11.3f
				};
				answers.Add(a);
			}
			return answers;
		}
		
		static int x = 1;
		Random r = new Random();
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped(string @join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 10; i++) {
				var a = new Answer {
					SomeInteger = 1,
					AverageV = r.Next(0, 100),
					CountV = 10,
					StandardDeviation = 11.3f
				};
				answers.Add(a);
			}
			x += 2;
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string @join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 1; i++) {
				var a = new Answer {
					SomeInteger = 1,
					AverageV = r.Next(0, 100),
					CountV = 10,
					StandardDeviation = 11.3f
				};
				a.Values = new List<AnswerValue>();
				for (int j = 0; j < 10; j++) {
					a.Values.Add(new AnswerValue { ValueDecimal = r.Next(0, 100), ValueInt = r.Next(0, 100) });
				}
				answers.Add(a);
			}
			x += 2;
			return answers;
		}
		
		public Answer ReadByKey(string key)
		{
			return new Answer() {
				Id = 1,
				ProjectRoundUser = new ProjectRoundUser { Id = 1 }
			};
		}
		
		public int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString)
		{
			return 10;
		}
		
		public int CountByProject(int projectRoundUserID, int yearFrom, int yearTo)
		{
			return 10;
		}
		
		public IList<Answer> FindByQuestionAndOptionWithYearSpan(int questionID, int optionID, int yearFrom, int yearTo)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 10; i++) {
				var a = new Answer() {
					SomeString = "Some String " + i,
					Average = 44f
				};
				answers.Add(a);
			}
			return answers;
		}
		
		public Answer ReadByQuestionAndOption(int answerID, int questionID, int optionID)
		{
			var a = new Answer();
			a.Values = new List<AnswerValue>(
				new AnswerValue[] {
					new AnswerValue { Answer = a, ValueInt = 10 }
				}
			);
			return a;
		}
		
		public Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString)
		{
			return new Answer() {
				DummyValue1 = 10,
				DummyValue2 = 10,
				DummyValue3 = 100
			};
		}
		
		public Answer ReadMinMax(string groupBy, int questionID, int optionID, int yearFrom, int yearTO, string sortString)
		{
			return new Answer() {
				Max = 12,
				Min = 100
			};
		}
		
		public int CountByDate(int yearFrom, int yearTo, string sortString)
		{
			return 10;
		}
	}
	
	public class DepartmentRepositoryStub : BaseRepositoryStub<Department>, IDepartmentRepository
	{
		public DepartmentRepositoryStub()
		{
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Name = "Department " + i,
					Id = i,
					ShortName = "Short Name " + i,
					Depth = r.Next(0, 8),
					Siblings = r.Next(0, 8),
					SortString = "SortString " + i
				};
				Data.Add(d);
			}
		}
		
		public void SaveSponsorAdminDepartment(SponsorAdminDepartment d)
		{
		}
		
		public void UpdateDepartment(Department d)
		{
		}
		
		public void UpdateDepartment2(Department d)
		{
		}
		
		public void UpdateDepartmentBySponsor(int sponsorID)
		{
		}
		
		public void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID)
		{
		}
		
		public Department ReadBySponsor(int sponsorID)
		{
			return new Department {
				SortString = "SortString",
				Parent = new Department { Id = 1 },
				Name = "Department",
				ShortName = "ShortName"
			};
		}
		
		public Department ReadByIdAndSponsor(int departmentID, int sponsorID)
		{
			return new Department {
				SortString = "SortString",
				Parent = new Department { Id = 1 },
				Name = "Department",
				ShortName = "ShortName"
			};
		}
		
		public IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<SponsorAdminDepartment>();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var d = new SponsorAdminDepartment {
					Admin = new SponsorAdmin { SuperUser = true },
					Department = new Department {
						Name = "Department " + i,
						Depth = r.Next(0, 8),
						Id = i,
						Siblings = r.Next(0, 8),
						ShortName = "Short Name " + i
					}
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<SponsorAdminDepartment>();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var d = new SponsorAdminDepartment {
					Admin = new SponsorAdmin { SuperUser = false },
					Department = new Department {
						Name = "Department " + i,
						Depth = r.Next(0, 8),
						Id = i,
						Siblings = r.Next(0, 8),
						ShortName = "Short Name " + i
					}
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindIn(string rndsd2)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Name = "Department " + i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Name = "Department " + i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Id = i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
		{
			var departments = new List<Department>();
			for (int i = 0; i < 10; i++) {
				var d = new Department {
					Id = i,
					TreeName = "TreeName " + i
				};
				departments.Add(d);
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string GID)
		{
			return Data;
		}
		
		public IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string GID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			return Data;
		}
		
		public IList<Department> FindBySponsorOrderedBySortString(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			return Data;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			return Data;
		}
		
		public IList<Department> FindBySponsorInDepth(int sponsorID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class LanguageRepositoryStub : BaseRepositoryStub<Language>, ILanguageRepository
	{
		public IList<SponsorProjectRoundUnitLanguage> FindBySponsor(int sponsorID)
		{
			return new List<SponsorProjectRoundUnitLanguage>(
				new SponsorProjectRoundUnitLanguage [] {
					new SponsorProjectRoundUnitLanguage {
						Language = new Language { Id = 1, Name = "Svenska" },
						SponsorProjectRoundUnit = new SponsorProjectRoundUnit { ProjectRoundUnit = new ProjectRoundUnit { Id = 1 } }
					},
					new SponsorProjectRoundUnitLanguage {
						Language = new Language { Id = 2, Name = "English" },
						SponsorProjectRoundUnit = new SponsorProjectRoundUnit { ProjectRoundUnit = new ProjectRoundUnit { Id = 1 } }
					}
				}
			);
		}
	}
}
