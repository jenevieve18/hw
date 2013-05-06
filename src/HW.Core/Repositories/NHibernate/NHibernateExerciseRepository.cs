//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateExerciseRepository : BaseNHibernateRepository<Exercise>, IExerciseRepository
	{
		public NHibernateExerciseRepository()
		{
		}
		
		public override IList<Exercise> FindAll()
		{
			return base.FindAll("healthWatch");
		}
		
		public IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort)
		{
			throw new NotImplementedException();
		}
		
		public IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID)
		{
			throw new NotImplementedException();
		}
	}
}
