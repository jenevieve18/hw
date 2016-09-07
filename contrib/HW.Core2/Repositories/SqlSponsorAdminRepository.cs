using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminRepository : BaseSqlRepository<SponsorAdmin>
	{
		public SqlSponsorAdminRepository()
		{
		}
		
		public override void Save(SponsorAdmin sponsorAdmin)
		{
			string query = @"
INSERT INTO SponsorAdmin(
	SponsorAdminID, 
	Usr, 
	Pas, 
	SponsorID, 
	Name, 
	Email, 
	SuperUser, 
	SponsorAdminKey, 
	Anonymized, 
	SeeUsers, 
	ReadOnly, 
	LastName, 
	PermanentlyDeleteUsers, 
	InviteSubject, 
	InviteTxt, 
	InviteReminderSubject, 
	InviteReminderTxt, 
	AllMessageSubject, 
	AllMessageBody, 
	InviteLastSent, 
	InviteReminderLastSent, 
	AllMessageLastSent, 
	LoginLastSent, 
	UniqueKey, 
	UniqueKeyUsed
)
VALUES(
	@SponsorAdminID, 
	@Usr, 
	@Pas, 
	@SponsorID, 
	@Name, 
	@Email, 
	@SuperUser, 
	@SponsorAdminKey, 
	@Anonymized, 
	@SeeUsers, 
	@ReadOnly, 
	@LastName, 
	@PermanentlyDeleteUsers, 
	@InviteSubject, 
	@InviteTxt, 
	@InviteReminderSubject, 
	@InviteReminderTxt, 
	@AllMessageSubject, 
	@AllMessageBody, 
	@InviteLastSent, 
	@InviteReminderLastSent, 
	@AllMessageLastSent, 
	@LoginLastSent, 
	@UniqueKey, 
	@UniqueKeyUsed
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", sponsorAdmin.SponsorAdminID),
				new SqlParameter("@Usr", sponsorAdmin.Usr),
				new SqlParameter("@Pas", sponsorAdmin.Pas),
				new SqlParameter("@SponsorID", sponsorAdmin.SponsorID),
				new SqlParameter("@Name", sponsorAdmin.Name),
				new SqlParameter("@Email", sponsorAdmin.Email),
				new SqlParameter("@SuperUser", sponsorAdmin.SuperUser),
				new SqlParameter("@SponsorAdminKey", sponsorAdmin.SponsorAdminKey),
				new SqlParameter("@Anonymized", sponsorAdmin.Anonymized),
				new SqlParameter("@SeeUsers", sponsorAdmin.SeeUsers),
				new SqlParameter("@ReadOnly", sponsorAdmin.ReadOnly),
				new SqlParameter("@LastName", sponsorAdmin.LastName),
				new SqlParameter("@PermanentlyDeleteUsers", sponsorAdmin.PermanentlyDeleteUsers),
				new SqlParameter("@InviteSubject", sponsorAdmin.InviteSubject),
				new SqlParameter("@InviteTxt", sponsorAdmin.InviteTxt),
				new SqlParameter("@InviteReminderSubject", sponsorAdmin.InviteReminderSubject),
				new SqlParameter("@InviteReminderTxt", sponsorAdmin.InviteReminderTxt),
				new SqlParameter("@AllMessageSubject", sponsorAdmin.AllMessageSubject),
				new SqlParameter("@AllMessageBody", sponsorAdmin.AllMessageBody),
				new SqlParameter("@InviteLastSent", sponsorAdmin.InviteLastSent),
				new SqlParameter("@InviteReminderLastSent", sponsorAdmin.InviteReminderLastSent),
				new SqlParameter("@AllMessageLastSent", sponsorAdmin.AllMessageLastSent),
				new SqlParameter("@LoginLastSent", sponsorAdmin.LoginLastSent),
				new SqlParameter("@UniqueKey", sponsorAdmin.UniqueKey),
				new SqlParameter("@UniqueKeyUsed", sponsorAdmin.UniqueKeyUsed)
			);
		}
		
		public override void Update(SponsorAdmin sponsorAdmin, int id)
		{
			string query = @"
UPDATE SponsorAdmin SET
	SponsorAdminID = @SponsorAdminID,
	Usr = @Usr,
	Pas = @Pas,
	SponsorID = @SponsorID,
	Name = @Name,
	Email = @Email,
	SuperUser = @SuperUser,
	SponsorAdminKey = @SponsorAdminKey,
	Anonymized = @Anonymized,
	SeeUsers = @SeeUsers,
	ReadOnly = @ReadOnly,
	LastName = @LastName,
	PermanentlyDeleteUsers = @PermanentlyDeleteUsers,
	InviteSubject = @InviteSubject,
	InviteTxt = @InviteTxt,
	InviteReminderSubject = @InviteReminderSubject,
	InviteReminderTxt = @InviteReminderTxt,
	AllMessageSubject = @AllMessageSubject,
	AllMessageBody = @AllMessageBody,
	InviteLastSent = @InviteLastSent,
	InviteReminderLastSent = @InviteReminderLastSent,
	AllMessageLastSent = @AllMessageLastSent,
	LoginLastSent = @LoginLastSent,
	UniqueKey = @UniqueKey,
	UniqueKeyUsed = @UniqueKeyUsed
WHERE SponsorAdminID = @SponsorAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", sponsorAdmin.SponsorAdminID),
				new SqlParameter("@Usr", sponsorAdmin.Usr),
				new SqlParameter("@Pas", sponsorAdmin.Pas),
				new SqlParameter("@SponsorID", sponsorAdmin.SponsorID),
				new SqlParameter("@Name", sponsorAdmin.Name),
				new SqlParameter("@Email", sponsorAdmin.Email),
				new SqlParameter("@SuperUser", sponsorAdmin.SuperUser),
				new SqlParameter("@SponsorAdminKey", sponsorAdmin.SponsorAdminKey),
				new SqlParameter("@Anonymized", sponsorAdmin.Anonymized),
				new SqlParameter("@SeeUsers", sponsorAdmin.SeeUsers),
				new SqlParameter("@ReadOnly", sponsorAdmin.ReadOnly),
				new SqlParameter("@LastName", sponsorAdmin.LastName),
				new SqlParameter("@PermanentlyDeleteUsers", sponsorAdmin.PermanentlyDeleteUsers),
				new SqlParameter("@InviteSubject", sponsorAdmin.InviteSubject),
				new SqlParameter("@InviteTxt", sponsorAdmin.InviteTxt),
				new SqlParameter("@InviteReminderSubject", sponsorAdmin.InviteReminderSubject),
				new SqlParameter("@InviteReminderTxt", sponsorAdmin.InviteReminderTxt),
				new SqlParameter("@AllMessageSubject", sponsorAdmin.AllMessageSubject),
				new SqlParameter("@AllMessageBody", sponsorAdmin.AllMessageBody),
				new SqlParameter("@InviteLastSent", sponsorAdmin.InviteLastSent),
				new SqlParameter("@InviteReminderLastSent", sponsorAdmin.InviteReminderLastSent),
				new SqlParameter("@AllMessageLastSent", sponsorAdmin.AllMessageLastSent),
				new SqlParameter("@LoginLastSent", sponsorAdmin.LoginLastSent),
				new SqlParameter("@UniqueKey", sponsorAdmin.UniqueKey),
				new SqlParameter("@UniqueKeyUsed", sponsorAdmin.UniqueKeyUsed)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdmin
WHERE SponsorAdminID = @SponsorAdminID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", id)
			);
		}
		
		public override SponsorAdmin Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminID, 
	Usr, 
	Pas, 
	SponsorID, 
	Name, 
	Email, 
	SuperUser, 
	SponsorAdminKey, 
	Anonymized, 
	SeeUsers, 
	ReadOnly, 
	LastName, 
	PermanentlyDeleteUsers, 
	InviteSubject, 
	InviteTxt, 
	InviteReminderSubject, 
	InviteReminderTxt, 
	AllMessageSubject, 
	AllMessageBody, 
	InviteLastSent, 
	InviteReminderLastSent, 
	AllMessageLastSent, 
	LoginLastSent, 
	UniqueKey, 
	UniqueKeyUsed
FROM SponsorAdmin
WHERE SponsorAdminID = @SponsorAdminID";
			SponsorAdmin sponsorAdmin = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminID", id))) {
				if (rs.Read()) {
					sponsorAdmin = new SponsorAdmin {
						SponsorAdminID = GetInt32(rs, 0),
						Usr = GetString(rs, 1),
						Pas = GetString(rs, 2),
						SponsorID = GetInt32(rs, 3),
						Name = GetString(rs, 4),
						Email = GetString(rs, 5),
						SuperUser = GetInt32(rs, 6),
						SponsorAdminKey = GetGuid(rs, 7),
						Anonymized = GetInt32(rs, 8),
						SeeUsers = GetInt32(rs, 9),
						ReadOnly = GetInt32(rs, 10),
						LastName = GetString(rs, 11),
						PermanentlyDeleteUsers = GetInt32(rs, 12),
						InviteSubject = GetString(rs, 13),
						InviteTxt = GetString(rs, 14),
						InviteReminderSubject = GetString(rs, 15),
						InviteReminderTxt = GetString(rs, 16),
						AllMessageSubject = GetString(rs, 17),
						AllMessageBody = GetString(rs, 18),
						InviteLastSent = GetDateTime(rs, 19),
						InviteReminderLastSent = GetDateTime(rs, 20),
						AllMessageLastSent = GetDateTime(rs, 21),
						LoginLastSent = GetDateTime(rs, 22),
						UniqueKey = GetString(rs, 23),
						UniqueKeyUsed = GetInt32(rs, 24)
					};
				}
			}
			return sponsorAdmin;
		}
		
		public override IList<SponsorAdmin> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminID, 
	Usr, 
	Pas, 
	SponsorID, 
	Name, 
	Email, 
	SuperUser, 
	SponsorAdminKey, 
	Anonymized, 
	SeeUsers, 
	ReadOnly, 
	LastName, 
	PermanentlyDeleteUsers, 
	InviteSubject, 
	InviteTxt, 
	InviteReminderSubject, 
	InviteReminderTxt, 
	AllMessageSubject, 
	AllMessageBody, 
	InviteLastSent, 
	InviteReminderLastSent, 
	AllMessageLastSent, 
	LoginLastSent, 
	UniqueKey, 
	UniqueKeyUsed
FROM SponsorAdmin";
			var sponsorAdmins = new List<SponsorAdmin>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdmins.Add(new SponsorAdmin {
						SponsorAdminID = GetInt32(rs, 0),
						Usr = GetString(rs, 1),
						Pas = GetString(rs, 2),
						SponsorID = GetInt32(rs, 3),
						Name = GetString(rs, 4),
						Email = GetString(rs, 5),
						SuperUser = GetInt32(rs, 6),
						SponsorAdminKey = GetGuid(rs, 7),
						Anonymized = GetInt32(rs, 8),
						SeeUsers = GetInt32(rs, 9),
						ReadOnly = GetInt32(rs, 10),
						LastName = GetString(rs, 11),
						PermanentlyDeleteUsers = GetInt32(rs, 12),
						InviteSubject = GetString(rs, 13),
						InviteTxt = GetString(rs, 14),
						InviteReminderSubject = GetString(rs, 15),
						InviteReminderTxt = GetString(rs, 16),
						AllMessageSubject = GetString(rs, 17),
						AllMessageBody = GetString(rs, 18),
						InviteLastSent = GetDateTime(rs, 19),
						InviteReminderLastSent = GetDateTime(rs, 20),
						AllMessageLastSent = GetDateTime(rs, 21),
						LoginLastSent = GetDateTime(rs, 22),
						UniqueKey = GetString(rs, 23),
						UniqueKeyUsed = GetInt32(rs, 24)
					});
				}
			}
			return sponsorAdmins;
		}
	}
}
