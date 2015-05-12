using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface ISponsorRepository : IBaseRepository<Sponsor>
	{
		IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId, string sort);
		
		void UpdateDeletedAdmin(int sponsorId, int sponsorAdminId);
		
		void SaveSponsorAdminSessionFunction(int sessionId, int functionId, DateTime date);
		
		Sponsor ReadSponsor3(int sponsorID);
		
		IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorId);
		
		IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorId, int langId);
	}
	
	public interface ISponsorAdminRepository : IBaseRepository<SponsorAdmin>
	{
		bool SponsorAdminHasAccess(int sponsorAdminID, int function);
	}
	
	public class SponsorRepositoryStub : BaseRepositoryStub<Sponsor>, ISponsorRepository
	{
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorId, int sponsorAdminId, string sort)
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
	}
	
	public class SponsorAdminRepositoryStub : BaseRepositoryStub<SponsorAdmin>, ISponsorAdminRepository
	{
		public bool SponsorAdminHasAccess(int sponsorAdminID, int function)
		{
			return true;
		}
	}
}
