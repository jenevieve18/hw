﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlExerciseRepository : BaseSqlRepository<Exercise>, IExerciseRepository
	{
		public void SaveStats(int ExerciseVariantLangID, int UID, int UPID)
		{
			string query = string.Format(
				@"
INSERT INTO [ExerciseStats] (ExerciseVariantLangID, UserID, UserProfileID)
VALUES ({0}, {1}, {2})",
				ExerciseVariantLangID,
				UID,
				UPID
			);
			Db.exec(query);
		}
		
		public void SaveExercise(Exercise e)
		{
			string query = string.Format(
				@"
INSERT INTO Exercise(ExerciseAreaID, ExerciseSortOrder, ExerciseImg, RequiredUserLevel, Minutes, ExerciseCategoryID, PrintOnBottom, ReplacementHead)
VALUES(@ExerciseAreaID, @ExerciseSortOrder, @ExerciseImg, @RequiredUserLevel, @Minutes, @ExerciseCategoryID, @PrintOnBottom, @ReplacementHead)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@ExerciseAreaID", e.Area.Id),
				new SqlParameter("@ExerciseSortOrder", e.SortOrder),
				new SqlParameter("@ExerciseImg", e.Image),
				new SqlParameter("@RequiredUserLevel", e.RequiredUserLevel),
				new SqlParameter("@Minutes", e.Minutes),
				new SqlParameter("@ExerciseCategoryID", e.Category.Id),
				new SqlParameter("@PrintOnBottom", e.PrintOnBottom),
				new SqlParameter("@ReplacementHead", e.ReplacementHead)
			);
		}
		
		public List<ExerciseVariantLanguage> FindExerciseVariants(int langID)
		{
			string query = string.Format(
				@"
SELECT el.Exercise,
	evl.ExerciseFile,
	et.ExerciseTypeID,
	evl.ExerciseContent,
	e.PrintOnBottom,
	e.ReplacementHead,
	evl.Lang
FROM [ExerciseVariantLang] evl
INNER JOIN [ExerciseVariant] ev ON evl.ExerciseVariantID = ev.ExerciseVariantID
INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
INNER JOIN [ExerciseLang] el ON ev.ExerciseID = el.ExerciseID AND el.Lang = evl.Lang
INNER JOIN [Exercise] e ON el.ExerciseID = e.ExerciseID
WHERE el.Lang = {0}",
				langID
			);
			List<ExerciseVariantLanguage> exercises = new List<ExerciseVariantLanguage>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var evl = new ExerciseVariantLanguage {
						Variant = new ExerciseVariant {
							Exercise = new Exercise {
								Languages = new [] { new ExerciseLanguage { ExerciseName = GetString(rs, 0) }},
								PrintOnBottom = GetInt32(rs, 4) == 1,
								ReplacementHead = GetString(rs, 5),
							},
							Type = GetObject<ExerciseType>(rs, 2)
						},
						File = GetString(rs, 1),
						Content = GetString(rs, 3),
						Language = GetObject<Language>(rs, 6)
					};
					exercises.Add(evl);
				}
			}
			return exercises;
		}
		
		public ExerciseVariantLanguage ReadExerciseVariant(int exerciseVariantLangID)
		{
			string query = string.Format(
				@"
SELECT el.Exercise,
	evl.ExerciseFile,
	et.ExerciseTypeID,
	evl.ExerciseContent,
	e.PrintOnBottom,
	e.ReplacementHead,
	evl.Lang
FROM [ExerciseVariantLang] evl
INNER JOIN [ExerciseVariant] ev ON evl.ExerciseVariantID = ev.ExerciseVariantID
INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
INNER JOIN [ExerciseLang] el ON ev.ExerciseID = el.ExerciseID AND el.Lang = evl.Lang
INNER JOIN [Exercise] e ON el.ExerciseID = e.ExerciseID
WHERE evl.ExerciseVariantLangID = {0}",
				exerciseVariantLangID
			);
			ExerciseVariantLanguage evl = null;
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					evl = new ExerciseVariantLanguage {
						Variant = new ExerciseVariant {
							Exercise = new Exercise {
								Languages = new [] { new ExerciseLanguage { ExerciseName = GetString(rs, 0) }},
								PrintOnBottom = GetInt32(rs, 4) == 1,
								ReplacementHead = GetString(rs, 5),
							},
							Type = GetObject<ExerciseType>(rs, 2)
						},
						File = GetString(rs, 1),
						Content = GetString(rs, 3),
						Language = GetObject<Language>(rs, 6)
					};
				}
			}
			return evl;
		}
		
		public ExerciseLanguage Read(int id, int langID)
		{
			string query = string.Format(
				@"
SELECT e.ExerciseImg,
	e.Minutes,
	e.ReplacementHead,
	el.Exercise,
	el.ExerciseTeaser
FROM Exercise e
INNER JOIN ExerciseLang el ON el.ExerciseID = e.ExerciseID
WHERE e.ExerciseID = {0}
AND el.Lang = {1}",
				id,
				langID
			);
			ExerciseLanguage e = null;
			using (SqlDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					e = new ExerciseLanguage {
						Exercise = new Exercise {
							Image = GetString(rs, 0),
							Minutes = GetInt32(rs, 1),
							ReplacementHead = GetString(rs, 2)
						},
						ExerciseName = GetString(rs, 3),
						Teaser = GetString(rs, 4)
					};
				}
			}
			return e;
		}
		
		public IList<Exercise> FindByAreaAndCategory2(int areaID, int categoryID, int langID, int sort)
		{
			string query = string.Format(
				@"
SELECT el.New,
	NULL,
	evl.ExerciseVariantLangID,
	eal.ExerciseArea,
	eal.ExerciseAreaID,
	e.ExerciseImg,
	e.ExerciseID,
	ea.ExerciseAreaImg,
	el.Exercise,
	el.ExerciseTime,
	el.ExerciseTeaser,
	evl.ExerciseFile,
	evl.ExerciseFileSize,
	evl.ExerciseContent,
	evl.ExerciseWindowX,
	evl.ExerciseWindowY,
	et.ExerciseTypeID,
	etl.ExerciseType,
	etl.ExerciseSubtype,
	ecl.ExerciseCategory
FROM [ExerciseArea] ea
INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID
INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID
INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
LEFT OUTER JOIN [ExerciseCategory] ec ON e.ExerciseCategoryID = ec.ExerciseCategoryID
LEFT OUTER JOIN [ExerciseCategoryLang] ecl ON ec.ExerciseCategoryID = ecl.ExerciseCategoryID AND ecl.Lang = eal.Lang
WHERE eal.Lang = el.Lang
AND e.RequiredUserLevel = 10
AND el.Lang = evl.Lang
AND evl.Lang = etl.Lang
AND etl.Lang = {0}
{1}
{2}
ORDER BY
{3}
HASHBYTES('MD2',CAST(RAND({4})*e.ExerciseID AS VARCHAR(16))) ASC,
et.ExerciseTypeSortOrder ASC",
				langID,
				(categoryID != 0 ? "AND e.ExerciseCategoryID = " + categoryID + " " : ""),
				(areaID != 0 ? "AND e.ExerciseAreaID = " + areaID + " " : ""),
				(sort == 1 ? "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) DESC, " : (sort == 2 ? "el.Exercise ASC, " : "")),
				DateTime.Now.Second * DateTime.Now.Minute
			);
			var exercises = new List<Exercise>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var e = new Exercise {
						Id = GetInt32(rs, 6),
						Image = GetString(rs, 5),
						Languages = new [] {
							new ExerciseLanguage {
								IsNew = GetBoolean(rs, 0),
								ExerciseName = GetString(rs, 8),
								Time = GetString(rs, 9),
								Teaser = GetString(rs, 10)
							}
						},
						Area = new ExerciseArea {
							Id = GetInt32(rs, 4),
							Languages = new [] { new ExerciseAreaLanguage { AreaName = GetString(rs, 3) }}
						},
						Variants = new [] {
							new ExerciseVariant {
								Id = GetInt32(rs, 2),
								Type = new ExerciseType {
									Languages = new [] {
										new ExerciseTypeLanguage {
											TypeName = GetString(rs, 17),
											SubTypeName = GetString(rs, 18)
										}
									}
								},
								Languages = new [] {
									new ExerciseVariantLanguage {
										File = GetString(rs, 11),
										Size = GetInt32(rs, 12),
										Content = GetString(rs, 13),
										ExerciseWindowX = GetInt32(rs, 14, 650),
										ExerciseWindowY = GetInt32(rs, 15, 580)
									}
								}
							}
						},
						Category = new ExerciseCategory {
							Languages = new [] {
								new ExerciseCategoryLanguage { CategoryName = GetString(rs, 19) }
							}
						}
					};
//					var e = new Exercise();
//					e.Id = rs.GetInt32(6);
//					e.Image = GetString(rs, 5);
//					e.CurrentLanguage = new ExerciseLanguage {
//						IsNew = rs.GetBoolean(0),
//						ExerciseName = rs.GetString(8),
//						Time = rs.GetString(9),
//						Teaser = rs.GetString(10)
//					};
//					e.CurrentArea = new ExerciseAreaLanguage {
//						Id = rs.GetInt32(4),
//						AreaName = rs.GetString(3)
//					};
//					e.CurrentVariant = new ExerciseVariantLanguage {
//						Id = GetInt32(rs, 2),
//						File = GetString(rs, 11),
//						Size = GetInt32(rs, 12),
//						Content = GetString(rs, 13),
//						ExerciseWindowX = GetInt32(rs, 14, 650),
//						ExerciseWindowY = GetInt32(rs, 15, 580)
//					};
//					e.CurrentType = new ExerciseTypeLanguage {
//						TypeName = GetString(rs, 17),
//						SubTypeName = GetString(rs, 18)
//					};
//					e.CurrentCategory = new ExerciseCategoryLanguage {
//						CategoryName = GetString(rs, 19)
//					};
					exercises.Add(e);
				}
			}
			return exercises;
		}
		
		public IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort)
		{
			string query = string.Format(
				@"
SELECT el.New,
	NULL,
	evl.ExerciseVariantLangID,
	eal.ExerciseArea,
	eal.ExerciseAreaID,
	e.ExerciseImg,
	e.ExerciseID,
	ea.ExerciseAreaImg,
	el.Exercise,
	el.ExerciseTime,
	el.ExerciseTeaser,
	evl.ExerciseFile,
	evl.ExerciseFileSize,
	evl.ExerciseContent,
	evl.ExerciseWindowX,
	evl.ExerciseWindowY,
	et.ExerciseTypeID,
	etl.ExerciseType,
	etl.ExerciseSubtype,
	ecl.ExerciseCategory
FROM [ExerciseArea] ea
INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID
INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID
INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
LEFT OUTER JOIN [ExerciseCategory] ec ON e.ExerciseCategoryID = ec.ExerciseCategoryID
LEFT OUTER JOIN [ExerciseCategoryLang] ecl ON ec.ExerciseCategoryID = ecl.ExerciseCategoryID AND ecl.Lang = eal.Lang
WHERE eal.Lang = el.Lang
AND e.RequiredUserLevel = 10
AND el.Lang = evl.Lang
AND evl.Lang = etl.Lang
AND etl.Lang = {0}
{1}
{2}
ORDER BY
{3}
HASHBYTES('MD2',CAST(RAND({4})*e.ExerciseID AS VARCHAR(16))) ASC,
et.ExerciseTypeSortOrder ASC",
				langID,
				(categoryID != 0 ? "AND e.ExerciseCategoryID = " + categoryID + " " : ""),
				(areaID != 0 ? "AND e.ExerciseAreaID = " + areaID + " " : ""),
				(sort == 1 ? "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) DESC, " : (sort == 2 ? "el.Exercise ASC, " : "")),
				DateTime.Now.Second * DateTime.Now.Minute
			);
			var exercises = new List<Exercise>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var e = new Exercise();
					e.Id = GetInt32(rs, 6);
					e.Image = GetString(rs, 5);
					e.CurrentLanguage = new ExerciseLanguage {
						IsNew = GetBoolean(rs, 0),
						ExerciseName = GetString(rs, 8),
						Time = GetString(rs, 9),
						Teaser = GetString(rs, 10)
					};
					e.Area = new ExerciseArea(GetInt32(rs, 4), new ExerciseAreaLanguage(GetString(rs, 3)));
//					e.CurrentArea = new ExerciseAreaLanguage {
//						Id = rs.GetInt32(4),
//						AreaName = rs.GetString(3)
//					};
					e.CurrentVariant = new ExerciseVariantLanguage {
						Id = GetInt32(rs, 2),
						File = GetString(rs, 11),
						Size = GetInt32(rs, 12),
						Content = GetString(rs, 13),
						ExerciseWindowX = GetInt32(rs, 14, 650),
						ExerciseWindowY = GetInt32(rs, 15, 580)
					};
					e.CurrentType = new ExerciseTypeLanguage {
						TypeName = GetString(rs, 17),
						SubTypeName = GetString(rs, 18)
					};
					e.Category = new ExerciseCategory(new ExerciseCategoryLanguage(GetString(rs, 19)));
//					e.CurrentCategory = new ExerciseCategoryLanguage {
//						CategoryName = GetString(rs, 19)
//					};
					exercises.Add(e);
				}
			}
			return exercises;
		}
		
		public IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID)
		{
			string query = string.Format(
				@"
SELECT eal.ExerciseCategory,
	eal.ExerciseCategoryID
FROM [ExerciseCategory] ea
INNER JOIN [ExerciseCategoryLang] eal ON ea.ExerciseCategoryID = eal.ExerciseCategoryID
WHERE eal.Lang = {1}
AND (
	SELECT COUNT(*)
	FROM Exercise e
	INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
	INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
	INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
	INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
	INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
	WHERE e.ExerciseCategoryID = ea.ExerciseCategoryID
	{2}
	AND eal.Lang = el.Lang
	AND e.RequiredUserLevel = 10
	AND el.Lang = evl.Lang
	AND evl.Lang = etl.Lang
) > 0
ORDER BY CASE eal.ExerciseCategoryID WHEN {0} THEN NULL ELSE ea.ExerciseCategorySortOrder END",
				categoryID,
				langID,
				areaID != 0 ? "AND e.ExerciseAreaID = " + areaID + " " : ""
			);
			var categories = new List<ExerciseCategoryLanguage>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var c = new ExerciseCategoryLanguage {
						Category = new ExerciseCategory { Id = rs.GetInt32(1) },
						CategoryName = rs.GetString(0)
					};
					categories.Add(c);
				}
			}
			return categories;
		}
		
		public IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID)
		{
			string query = string.Format(
				@"
SELECT eal.ExerciseArea,
	eal.ExerciseAreaID
FROM [ExerciseArea] ea
INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID
WHERE eal.Lang = {1} AND (
	SELECT COUNT(*)
	FROM Exercise e
	INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
	INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
	INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
	INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
	INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
	WHERE e.ExerciseAreaID = ea.ExerciseAreaID
	AND eal.Lang = el.Lang
	AND e.RequiredUserLevel = 10
	AND el.Lang = evl.Lang
	AND evl.Lang = etl.Lang
) > 0
ORDER BY CASE eal.ExerciseAreaID WHEN {0} THEN NULL ELSE ea.ExerciseAreaSortOrder END",
				areaID,
				langID
			);
			var areas = new List<ExerciseAreaLanguage>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a = new ExerciseAreaLanguage {
						Area = new ExerciseArea { Id = rs.GetInt32(1) },
						AreaName = rs.GetString(0)
					};
					areas.Add(a);
				}
			}
			return areas;
		}
	}
}
