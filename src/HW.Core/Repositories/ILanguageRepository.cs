using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface ILanguageRepository : IBaseRepository<Language>
	{
		IList<SponsorProjectRoundUnitLanguage> FindBySponsor(int sponsorID);
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
