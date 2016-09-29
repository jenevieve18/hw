// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

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
	}
}
