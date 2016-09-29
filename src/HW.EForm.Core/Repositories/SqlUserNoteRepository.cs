using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUserNoteRepository : BaseSqlRepository<UserNote>
	{
		public SqlUserNoteRepository()
		{
		}
		
		public override void Save(UserNote userNote)
		{
			string query = @"
INSERT INTO UserNote(
	UserNoteID, 
	UserID, 
	DT, 
	SponsorAdminID, 
	Note, 
	EditSponsorAdminID, 
	EditDT, 
	NoteJapaneseUnicode
)
VALUES(
	@UserNoteID, 
	@UserID, 
	@DT, 
	@SponsorAdminID, 
	@Note, 
	@EditSponsorAdminID, 
	@EditDT, 
	@NoteJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserNoteID", userNote.UserNoteID),
				new SqlParameter("@UserID", userNote.UserID),
				new SqlParameter("@DT", userNote.DT),
				new SqlParameter("@SponsorAdminID", userNote.SponsorAdminID),
				new SqlParameter("@Note", userNote.Note),
				new SqlParameter("@EditSponsorAdminID", userNote.EditSponsorAdminID),
				new SqlParameter("@EditDT", userNote.EditDT),
				new SqlParameter("@NoteJapaneseUnicode", userNote.NoteJapaneseUnicode)
			);
		}
		
		public override void Update(UserNote userNote, int id)
		{
			string query = @"
UPDATE UserNote SET
	UserNoteID = @UserNoteID,
	UserID = @UserID,
	DT = @DT,
	SponsorAdminID = @SponsorAdminID,
	Note = @Note,
	EditSponsorAdminID = @EditSponsorAdminID,
	EditDT = @EditDT,
	NoteJapaneseUnicode = @NoteJapaneseUnicode
WHERE UserNoteID = @UserNoteID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserNoteID", userNote.UserNoteID),
				new SqlParameter("@UserID", userNote.UserID),
				new SqlParameter("@DT", userNote.DT),
				new SqlParameter("@SponsorAdminID", userNote.SponsorAdminID),
				new SqlParameter("@Note", userNote.Note),
				new SqlParameter("@EditSponsorAdminID", userNote.EditSponsorAdminID),
				new SqlParameter("@EditDT", userNote.EditDT),
				new SqlParameter("@NoteJapaneseUnicode", userNote.NoteJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserNote
WHERE UserNoteID = @UserNoteID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserNoteID", id)
			);
		}
		
		public override UserNote Read(int id)
		{
			string query = @"
SELECT 	UserNoteID, 
	UserID, 
	DT, 
	SponsorAdminID, 
	Note, 
	EditSponsorAdminID, 
	EditDT, 
	NoteJapaneseUnicode
FROM UserNote
WHERE UserNoteID = @UserNoteID";
			UserNote userNote = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserNoteID", id))) {
				if (rs.Read()) {
					userNote = new UserNote {
						UserNoteID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetDateTime(rs, 2),
						SponsorAdminID = GetInt32(rs, 3),
						Note = GetString(rs, 4),
						EditSponsorAdminID = GetInt32(rs, 5),
						EditDT = GetDateTime(rs, 6),
						NoteJapaneseUnicode = GetString(rs, 7)
					};
				}
			}
			return userNote;
		}
		
		public override IList<UserNote> FindAll()
		{
			string query = @"
SELECT 	UserNoteID, 
	UserID, 
	DT, 
	SponsorAdminID, 
	Note, 
	EditSponsorAdminID, 
	EditDT, 
	NoteJapaneseUnicode
FROM UserNote";
			var userNotes = new List<UserNote>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userNotes.Add(new UserNote {
						UserNoteID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetDateTime(rs, 2),
						SponsorAdminID = GetInt32(rs, 3),
						Note = GetString(rs, 4),
						EditSponsorAdminID = GetInt32(rs, 5),
						EditDT = GetDateTime(rs, 6),
						NoteJapaneseUnicode = GetString(rs, 7)
					});
				}
			}
			return userNotes;
		}
	}
}
