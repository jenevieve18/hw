using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface ISponsorRepository : IBaseRepository<Sponsor>
	{
		//IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId, string sortFirstName, string sortLastName);
        IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId, string orderBy);
		
		void UpdateDeletedAdmin(int sponsorId, int sponsorAdminId);
		
		void SaveSponsorAdminSessionFunction(int sessionId, int functionId, DateTime date);
		
		Sponsor ReadSponsor3(int sponsorID);
		
		IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorId);
		
		IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorId, int langId);
		
		SponsorAdmin ReadSponsorAdmin(string skey, string sakey, string sa, string said, string anv, string los);
		
		void SaveSponsorAdminSession(int sponsorAdminId, DateTime date);
		
		int ReadLastSponsorAdminSession();
		
		void UpdateSponsorAdminSession(int sponsorAdminSessionId, DateTime date);
	}
	
	public interface ISponsorAdminRepository : IBaseRepository<SponsorAdmin>
	{
		bool SponsorAdminHasAccess(int sponsorAdminID, int function);
	}
	
	public class SponsorRepositoryStub : BaseRepositoryStub<Sponsor>, ISponsorRepository
	{
		//public IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId, string sortFirstName, string sortLastName)
        public IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId, string orderBy)
		{
			return new [] {
				new SponsorAdmin {},
				new SponsorAdmin {},
				new SponsorAdmin {},
			};
		}
		
		public void UpdateDeletedAdmin(int sponsorId, int sponsorAdminId)
		{
		}
		
		public void SaveSponsorAdminSessionFunction(int sessionId, int functionId, DateTime date)
		{
		}
		
		public Sponsor ReadSponsor3(int sponsorID)
		{
			return new Sponsor {
				SuperSponsor = new SuperSponsor {
					Languages = new [] {
						new SuperSponsorLanguage { Header = "Header1" }
					}
				}
			};
		}
		
		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorId)
		{
			return new[] {
				new SponsorBackgroundQuestion {},
				new SponsorBackgroundQuestion {},
				new SponsorBackgroundQuestion {}
			};
		}
		
		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorId, int langId)
		{
			return new[] {
				new SponsorProjectRoundUnit {},
				new SponsorProjectRoundUnit {},
				new SponsorProjectRoundUnit {}
			};
		}
		
		public SponsorAdmin ReadSponsorAdmin(string skey, string sakey, string sa, string said, string anv, string los)
		{
			throw new NotImplementedException();
		}
		
		public void SaveSponsorAdminSession(int sponsorAdminId, DateTime date)
		{
		}
		
		public int ReadLastSponsorAdminSession()
		{
			return 1;
		}
		
		public void UpdateSponsorAdminSession(int sponsorAdminSessionId, DateTime date)
		{
		}
	}
	
	public class SponsorAdminRepositoryStub : BaseRepositoryStub<SponsorAdmin>, ISponsorAdminRepository
	{
		public bool SponsorAdminHasAccess(int sponsorAdminID, int function)
		{
			return true;
		}
	}
}
