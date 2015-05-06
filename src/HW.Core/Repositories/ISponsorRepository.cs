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
	}
	
	public class SponsorAdminRepositoryStub : BaseRepositoryStub<SponsorAdmin>, ISponsorAdminRepository
	{
		public bool SponsorAdminHasAccess(int sponsorAdminID, int function)
		{
			return true;
		}
	}
}
