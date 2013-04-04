//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IExerciseRepository : IBaseRepository<Exercise>
	{
		IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort);
		
		IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID);
		
		IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID);
	}
	
	public class ExerciseRepositoryStub : BaseRepositoryStub<Exercise>, IExerciseRepository
	{
		public IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort)
		{
			var exercises = new List<Exercise>();
			for (int i = 0; i < 7; i++) {
				var e = new Exercise();
				e.Id = i;
				e.Image = "";
				e.CurrentLanguage = new ExerciseLanguage {
					IsNew = true,
					ExerciseName = "Exercise " + i,
					Time = "5-10 min",
					Teaser = "Teaser " + i
				};
				e.CurrentArea = new ExerciseAreaLanguage {
					Id = 1,
					AreaName = "Area " + i
				};
				e.CurrentVariant = new ExerciseVariantLanguage {
					Id = i,
					File = "File " + i,
					Size = 100,
					Content = "Content " + i,
					ExerciseWindowX = 10,
					ExerciseWindowY = 10
				};
				e.CurrentType = new ExerciseTypeLanguage {
					TypeName = "Type " + i,
					SubTypeName = "Sub Type " + i
				};
				e.CurrentCategory = new ExerciseCategoryLanguage {
					CategoryName = "Category " + i
				};
				exercises.Add(e);
			}
			return exercises;
		}
		
		public IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID)
		{
			var areas = new List<ExerciseAreaLanguage>();
			for (int i = 0; i < 10; i++) {
				var a = new ExerciseAreaLanguage {
					Area = new ExerciseArea { Id = i },
					AreaName = "Area Name " + i,
				};
				areas.Add(a);
			}
			return areas;
		}
		
		public IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID)
		{
			var categories = new List<ExerciseCategoryLanguage>();
			for (int i = 0; i < 10; i++) {
				var c = new ExerciseCategoryLanguage {
					Category = new ExerciseCategory { Id = i },
					CategoryName = "Category " + i
				};
				categories.Add(c);
			}
			return categories;
		}
	}
}
