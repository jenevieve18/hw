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
		
		public override Exercise Read(int id)
		{
			return base.Read(id, "healthWatch");
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
		
		public IList<ExerciseCategory> FindCategories()
		{
			return base.FindAll<ExerciseCategory>("healthWatch");
		}
		
		public IList<ExerciseCategoryLanguage> FindCategoryLanguages()
		{
			return base.FindAll<ExerciseCategoryLanguage>("healthWatch");
		}
		
		public ExerciseCategoryLanguage ReadCategoryLanguage(int id)
		{
			return base.Read<ExerciseCategoryLanguage>(id, "healthWatch");
		}
		
		public IList<ExerciseAreaLanguage> FindAreaLanguages()
		{
			return base.FindAll<ExerciseAreaLanguage>("healthWatch");
		}
		
		public IList<ExerciseVariantLanguage> FindVariantLanguages()
		{
			return base.FindAll<ExerciseVariantLanguage>("healthWatch");
		}
		
		public IList<ExerciseTypeLanguage> FindTypeLanguages()
		{
			return base.FindAll<ExerciseTypeLanguage>("healthWatch");
		}
	}
}
