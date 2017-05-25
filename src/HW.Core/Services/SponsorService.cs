using System;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class SponsorService
	{
		SqlSponsorRepository sponsorRepo = new SqlSponsorRepository();
		SqlSponsorAdminRepository sponsorAdminRepo = new SqlSponsorAdminRepository();
		
		public SponsorService()
		{
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID)
		{
			var sa = sponsorAdminRepo.Read(sponsorAdminID);
			if (sa != null) {
				sa.Sponsor = sponsorRepo.Read(sa.Sponsor.Id);
			}
			return sa;
		}
		
		public Sponsor ReadSponsor(int sponsorID)
		{
			var s = sponsorRepo.Read(sponsorID);
			return s;
		}
	}
}
