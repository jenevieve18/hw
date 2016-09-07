using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlDiaryRepository : BaseSqlRepository<Diary>
	{
		public SqlDiaryRepository()
		{
		}
		
		public override void Save(Diary diary)
		{
			string query = @"
INSERT INTO Diary(
	DiaryID, 
	DiaryNote, 
	DiaryDate, 
	UserID, 
	CreatedDT, 
	DeletedDT, 
	Mood
)
VALUES(
	@DiaryID, 
	@DiaryNote, 
	@DiaryDate, 
	@UserID, 
	@CreatedDT, 
	@DeletedDT, 
	@Mood
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DiaryID", diary.DiaryID),
				new SqlParameter("@DiaryNote", diary.DiaryNote),
				new SqlParameter("@DiaryDate", diary.DiaryDate),
				new SqlParameter("@UserID", diary.UserID),
				new SqlParameter("@CreatedDT", diary.CreatedDT),
				new SqlParameter("@DeletedDT", diary.DeletedDT),
				new SqlParameter("@Mood", diary.Mood)
			);
		}
		
		public override void Update(Diary diary, int id)
		{
			string query = @"
UPDATE Diary SET
	DiaryID = @DiaryID,
	DiaryNote = @DiaryNote,
	DiaryDate = @DiaryDate,
	UserID = @UserID,
	CreatedDT = @CreatedDT,
	DeletedDT = @DeletedDT,
	Mood = @Mood
WHERE DiaryID = @DiaryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DiaryID", diary.DiaryID),
				new SqlParameter("@DiaryNote", diary.DiaryNote),
				new SqlParameter("@DiaryDate", diary.DiaryDate),
				new SqlParameter("@UserID", diary.UserID),
				new SqlParameter("@CreatedDT", diary.CreatedDT),
				new SqlParameter("@DeletedDT", diary.DeletedDT),
				new SqlParameter("@Mood", diary.Mood)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Diary
WHERE DiaryID = @DiaryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DiaryID", id)
			);
		}
		
		public override Diary Read(int id)
		{
			string query = @"
SELECT 	DiaryID, 
	DiaryNote, 
	DiaryDate, 
	UserID, 
	CreatedDT, 
	DeletedDT, 
	Mood
FROM Diary
WHERE DiaryID = @DiaryID";
			Diary diary = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@DiaryID", id))) {
				if (rs.Read()) {
					diary = new Diary {
						DiaryID = GetInt32(rs, 0),
						DiaryNote = GetString(rs, 1),
						DiaryDate = GetDateTime(rs, 2),
						UserID = GetInt32(rs, 3),
						CreatedDT = GetDateTime(rs, 4),
						DeletedDT = GetDateTime(rs, 5),
						Mood = GetInt32(rs, 6)
					};
				}
			}
			return diary;
		}
		
		public override IList<Diary> FindAll()
		{
			string query = @"
SELECT 	DiaryID, 
	DiaryNote, 
	DiaryDate, 
	UserID, 
	CreatedDT, 
	DeletedDT, 
	Mood
FROM Diary";
			var diarys = new List<Diary>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					diarys.Add(new Diary {
						DiaryID = GetInt32(rs, 0),
						DiaryNote = GetString(rs, 1),
						DiaryDate = GetDateTime(rs, 2),
						UserID = GetInt32(rs, 3),
						CreatedDT = GetDateTime(rs, 4),
						DeletedDT = GetDateTime(rs, 5),
						Mood = GetInt32(rs, 6)
					});
				}
			}
			return diarys;
		}
	}
}
