﻿using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IExerciseRepository : IBaseRepository<Exercise>
	{
		void SaveExercise(Exercise e);
		
		List<ExerciseVariantLanguage> FindExerciseVariants(int langID);
		
		ExerciseVariantLanguage ReadExerciseVariant(int exerciseVariantLangID);
		
		ExerciseLanguage Read(int id, int langID);
		
		IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort);
		
		IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID);
		
		IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID);
	}
	
	public class ExerciseRepositoryStub : BaseRepositoryStub<Exercise>, IExerciseRepository
	{
		public void SaveExercise(Exercise e)
		{
			throw new NotImplementedException();
		}
		
		public List<ExerciseVariantLanguage> FindExerciseVariants(int langID)
		{
			throw new NotImplementedException();
		}
		
		public ExerciseVariantLanguage ReadExerciseVariant(int exerciseVariantLangID)
		{
			throw new NotImplementedException();
		}
		
		public ExerciseLanguage Read(int id, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort)
		{
			throw new NotImplementedException();
		}
		
		public IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID)
		{
			return new List<ExerciseCategoryLanguage>(
				new [] {
					new ExerciseCategoryLanguage {},
					new ExerciseCategoryLanguage {},
					new ExerciseCategoryLanguage {},
				}
			);
		}
		
		public IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID)
		{
			return new List<ExerciseAreaLanguage>(
				new [] {
					new ExerciseAreaLanguage { Area = new ExerciseArea { Id = 1 }, AreaName = "Name1" },
					new ExerciseAreaLanguage { Area = new ExerciseArea { Id = 2 }, AreaName = "Name2" },
					new ExerciseAreaLanguage { Area = new ExerciseArea { Id = 3 }, AreaName = "Name3" },
				}
			);
		}
	}
}
