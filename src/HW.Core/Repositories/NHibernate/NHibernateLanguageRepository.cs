﻿using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateLanguageRepository : BaseNHibernateRepository<Language>, ILanguageRepository
	{
		public NHibernateLanguageRepository()
		{
		}
		
		public IList<SponsorProjectRoundUnitLanguage> FindBySponsor(int sponsorID)
		{
			throw new NotImplementedException();
		}
	}
}
