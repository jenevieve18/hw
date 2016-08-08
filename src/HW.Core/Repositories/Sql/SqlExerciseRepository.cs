using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlExerciseRepository : BaseSqlRepository<Exercise>, IExerciseRepository
	{
		public void UpdateVariant(int id, string content)
		{
			string query = @"
UPDATE ExerciseVariantLang SET ExerciseContent = @ExerciseContent
WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@ExerciseContent", content),
				new SqlParameter("@ExerciseVariantLangID", id)
			);
		}
		
		public void UpdateExerciseScript(string script, int exerciseID)
		{
			string query = @"
UPDATE dbo.Exercise
   SET Script = @Script
 WHERE ExerciseID = @ExerciseID";
			ExecuteNonQuery(query, "healthWatchSqlConnection",
			                new SqlParameter("@Script", script),
			                new SqlParameter("@ExerciseID", exerciseID));
		}
		
		public void UpdateExerciseVariantLanguageContent(string content, int exerciseVariantLangID)
		{
			string query = @"
UPDATE dbo.ExerciseVariantLang
   SET ExerciseContent = @ExerciseContent
 WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
			ExecuteNonQuery(query, "healthWatchSqlConnection",
			                new SqlParameter("@ExerciseContent", content),
			                new SqlParameter("@ExerciseVariantLangID", exerciseVariantLangID));
		}
		
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
		
		public void SaveExerciseVariantLanguage(string content, string script, int exerciseVariantLangID)
		{
			string query = @"
UPDATE dbo.ExerciseVariantLang SET ExerciseContent = @ExerciseContent
WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@ExerciseContent", content),
				new SqlParameter("@ExerciseVariantLangID", exerciseVariantLangID)
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
		
		public SponsorAdminExercise ReadSponsorAdminExercise(int sponsorAdminExerciseID)
		{
			string query = string.Format(
				@"
SELECT evl.ExerciseContent,
evl.ExerciseVariantLangID
FROM dbo.SponsorAdminExercise sae
INNER JOIN dbo.ExerciseVariantLang evl ON evl.ExerciseVariantLangID = sae.ExerciseVariantLangID
INNER JOIN dbo.ExerciseVariant ev ON ev.ExerciseVariantID = evl.ExerciseVariantID
WHERE sae.SponsorAdminExerciseID = {0}",
			sponsorAdminExerciseID);
			
			SponsorAdminExercise s = null;
			using (var rs = ExecuteReader(query)) {
				if (rs.Read()) {
					s = new SponsorAdminExercise {
						ExerciseVariantLanguage = new ExerciseVariantLanguage {
							Content = GetString(rs, 0),
							Id = GetInt32(rs, 1)
						}
					};
				}
			}
			return s;
		}
		
		public ExerciseLanguage ReadExerciseLanguage(int exerciseID, int langID)
		{
			string query = @"
SELECT [ExerciseLangID]
      ,[ExerciseID]
      ,[Exercise]
      ,[ExerciseTime]
      ,[ExerciseTeaser]
      ,[Lang]
      ,[New]
      ,[ExerciseContent]
  FROM [healthWatch].[dbo].[ExerciseLang]
  WHERE [ExerciseID] = @ExerciseID AND [Lang] = @Lang
";
			ExerciseLanguage el = null;
			using (SqlDataReader rs = ExecuteReader(
				query, 
				"healthWatchSqlConnection", 
				new SqlParameter("@ExerciseID", exerciseID),
				new SqlParameter("@Lang", langID))) {
				if (rs.Read()) {
					el = new ExerciseLanguage {
						Id = GetInt32(rs, 0),
						Exercise = new Exercise { Id = GetInt32(rs, 1) },
						ExerciseName = GetString(rs, 2),
						Time = GetString(rs, 3),
						Teaser = GetString(rs, 4),
						Language = new Language { Id = GetInt32(rs, 5) },
						IsNew = GetBoolean(rs, 6),
						Content = GetString(rs, 7)
					};
				}
			}
			return el;
		}
		
		public Exercise ReadExercise(int exerciseID)
		{
			string query = @"
SELECT [ExerciseID]
      ,[ExerciseAreaID]
      ,[ExerciseSortOrder]
      ,[ExerciseImg]
      ,[RequiredUserLevel]
      ,[Minutes]
      ,[ExerciseCategoryID]
      ,[PrintOnBottom]
      ,[ReplacementHead]
      ,[Status]
      ,[Script]
  FROM [healthWatch].[dbo].[Exercise]
  WHERE [ExerciseID] = @ExerciseID";
			Exercise e = null;
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@ExerciseID", exerciseID))) {
				if (rs.Read()) {
					e = new Exercise {
						Id = GetInt32(rs, 0),
						Area = new ExerciseArea { Id = GetInt32(rs, 1) },
						SortOrder = GetInt32(rs, 2),
						Image = GetString(rs, 3),
						RequiredUserLevel = GetInt32(rs, 4),
						Minutes = GetInt32(rs, 5),
						Category = new ExerciseCategory { Id = GetInt32(rs, 6) },
						PrintOnBottom = GetInt32(rs, 7) == 1,
						ReplacementHead = GetString(rs, 8),
						Status = GetInt32(rs, 9),
						Script = GetString(rs, 10)
					};
				}
			}
			return e;
		}
		
		public ExerciseVariant ReadExerciseVariant2(int exerciseVariantID)
		{
			string query = @"
SELECT [ExerciseVariantID]
      ,[ExerciseID]
      ,[ExerciseTypeID]
  FROM [healthWatch].[dbo].[ExerciseVariant]
  WHERE [ExerciseVariantID] = @ExerciseVariantID";
			ExerciseVariant ev = null;
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@ExerciseVariantID", exerciseVariantID))) {
				if (rs.Read()) {
					ev = new ExerciseVariant {
						Id = GetInt32(rs, 0),
						Exercise = new Exercise { Id = GetInt32(rs, 1) },
						Type = new ExerciseType { Id = GetInt32(rs, 2) }
					};
				}
			}
			return ev;
		}
		
		public ExerciseVariantLanguage ReadExerciseVariantLanguage(int exerciseVariantLangID)
		{
			string query = @"
SELECT [ExerciseVariantLangID]
      ,[ExerciseVariantID]
      ,[ExerciseFile]
      ,[ExerciseFileSize]
      ,[ExerciseContent]
      ,[ExerciseWindowX]
      ,[ExerciseWindowY]
      ,[Lang]
  FROM [healthWatch].[dbo].[ExerciseVariantLang]
  WHERE [ExerciseVariantLangID] = @ExerciseVariantLangID";
			ExerciseVariantLanguage evl = null;
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@ExerciseVariantLangID", exerciseVariantLangID))) {
				if (rs.Read()) {
					evl = new ExerciseVariantLanguage {
						Id = GetInt32(rs, 0),
						Variant = new ExerciseVariant { Id = GetInt32(rs, 1) },
						File = GetString(rs, 2),
						Size = GetInt32(rs, 3),
						Content = GetString(rs, 4),
						ExerciseWindowX = GetInt32(rs, 5),
						ExerciseWindowY = GetInt32(rs, 6),
						Language = new Language { Id = GetInt32(rs, 7) }
					};
				}
			}
			return evl;
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
	evl.Lang,
    e.Script
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
								ReplacementHead = GetString(rs, 5, ""),
								Script = GetString(rs, 7)
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
//										ExerciseWindowX = GetInt32(rs, 14, 650),
//										ExerciseWindowY = GetInt32(rs, 15, 580)
										ExerciseWindowX = GetInt32(rs, 14, 960),
										ExerciseWindowY = GetInt32(rs, 15, 760)
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
		
		public IList<SponsorAdminExercise> FindBySponsorAdminExerciseHistory(int langID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT sae.Date,
	e.ExerciseImg,
	el.Exercise,
	el.ExerciseTeaser,
	el.ExerciseTime,
	evl.ExerciseFile,
	eal.ExerciseArea,
	ecl.ExerciseCategory,
	etl.ExerciseType,
	evl.ExerciseWindowX,
	evl.ExerciseWindowY,
	evl.ExerciseVariantLangID,
	sae.SponsorAdminExerciseID
FROM SponsorAdminExercise sae
INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = sae.ExerciseVariantLangID
INNER JOIN ExerciseVariant ev ON ev.ExerciseVariantID = evl.ExerciseVariantID
INNER JOIN ExerciseType et ON et.ExerciseTypeID = ev.ExerciseTypeID
INNER JOIN ExerciseTypeLang etl ON etl.ExerciseTypeID = et.ExerciseTypeID AND etl.Lang = @Lang
INNER JOIN Exercise e ON e.ExerciseID = ev.ExerciseID
INNER JOIN ExerciseLang el ON el.ExerciseID = e.ExerciseID AND el.Lang = @Lang
INNER JOIN ExerciseCategory ec ON ec.ExerciseCategoryID = e.ExerciseCategoryID
INNER JOIN ExerciseCategoryLang ecl ON ecl.ExerciseCategoryID = ec.ExerciseCategoryID AND ecl.Lang = @Lang
INNER JOIN ExerciseArea ea ON ea.ExerciseAreaID = e.ExerciseAreaID
INNER JOIN ExerciseAreaLang eal ON eal.ExerciseAreaID = ea.ExerciseAreaID AND eal.Lang = @Lang
WHERE sae.SponsorAdminID = @SponsorAdminID
ORDER BY sae.Date DESC"
			);
			var managerExercises = new List<SponsorAdminExercise>();
			using (SqlDataReader rs = ExecuteReader(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@Lang", langID),
				new SqlParameter("@SponsorAdminID", sponsorAdminID))) {
				while (rs.Read()) {
					var e = new Exercise {
						Image = GetString(rs, 1),
						Languages = new List<ExerciseLanguage>(
							new ExerciseLanguage[] {
								new ExerciseLanguage {
									ExerciseName = GetString(rs, 2),
									Teaser = GetString(rs, 3),
									Time = GetString(rs, 4),
								}
							}
						),
						Area = new ExerciseArea {
							Languages = new List<ExerciseAreaLanguage>(
								new ExerciseAreaLanguage[] {
									new ExerciseAreaLanguage { AreaName = GetString(rs, 6) }
								}
							)
						},
						Category = new ExerciseCategory {
							Languages = new List<ExerciseCategoryLanguage>(
								new ExerciseCategoryLanguage[] {
									new ExerciseCategoryLanguage { CategoryName = GetString(rs, 7) }
								}
							)
						}
					};
					var ev = new ExerciseVariant {
						Exercise = e,
						Type =  new ExerciseType {
							Languages = new List<ExerciseTypeLanguage>(
								new ExerciseTypeLanguage[] {
									new ExerciseTypeLanguage { TypeName = GetString(rs, 8) }
								}
							)
						}
					};
					var evl = new ExerciseVariantLanguage {
						Variant = ev,
						File = GetString(rs, 5),
//						ExerciseWindowX = GetInt32(rs, 9, 650),
//						ExerciseWindowY = GetInt32(rs, 10, 580),
						ExerciseWindowX = GetInt32(rs, 9, 980),
						ExerciseWindowY = GetInt32(rs, 10, 760),
						Id = GetInt32(rs, 11)
							
					};
					var sae = new SponsorAdminExercise {
						Date = GetDateTime(rs, 0),
						ExerciseVariantLanguage = evl,
						Id = GetInt32(rs, 12)
					};
					managerExercises.Add(sae);
				}
			}
			return managerExercises;
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
					e.Area = new ExerciseArea(GetInt32(rs, 4), new ExerciseAreaLanguage(GetString(rs, 3, "")));
					e.CurrentVariant = new ExerciseVariantLanguage {
						Id = GetInt32(rs, 2),
						File = GetString(rs, 11),
						Size = GetInt32(rs, 12),
						Content = GetString(rs, 13),
//						ExerciseWindowX = GetInt32(rs, 14, 650),
//						ExerciseWindowY = GetInt32(rs, 15, 580)
						ExerciseWindowX = GetInt32(rs, 14, 960),
						ExerciseWindowY = GetInt32(rs, 15, 760)
					};
					e.CurrentType = new ExerciseTypeLanguage {
						TypeName = GetString(rs, 17),
						SubTypeName = GetString(rs, 18)
					};
					e.Category = new ExerciseCategory(new ExerciseCategoryLanguage(GetString(rs, 19, "")));
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
