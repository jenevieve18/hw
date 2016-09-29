using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlMailQueueRepository : BaseSqlRepository<MailQueue>
	{
		public SqlMailQueueRepository()
		{
		}
		
		public override void Save(MailQueue mailQueue)
		{
			string query = @"
INSERT INTO MailQueue(
	MailQueueID, 
	ProjectRoundUserID, 
	AdrTo, 
	AdrFrom, 
	Subject, 
	Body, 
	Sent, 
	SendType, 
	ErrorDescription, 
	BodyJapaneseUnicode, 
	SubjectJapaneseUnicode, 
	LangID
)
VALUES(
	@MailQueueID, 
	@ProjectRoundUserID, 
	@AdrTo, 
	@AdrFrom, 
	@Subject, 
	@Body, 
	@Sent, 
	@SendType, 
	@ErrorDescription, 
	@BodyJapaneseUnicode, 
	@SubjectJapaneseUnicode, 
	@LangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MailQueueID", mailQueue.MailQueueID),
				new SqlParameter("@ProjectRoundUserID", mailQueue.ProjectRoundUserID),
				new SqlParameter("@AdrTo", mailQueue.AdrTo),
				new SqlParameter("@AdrFrom", mailQueue.AdrFrom),
				new SqlParameter("@Subject", mailQueue.Subject),
				new SqlParameter("@Body", mailQueue.Body),
				new SqlParameter("@Sent", mailQueue.Sent),
				new SqlParameter("@SendType", mailQueue.SendType),
				new SqlParameter("@ErrorDescription", mailQueue.ErrorDescription),
				new SqlParameter("@BodyJapaneseUnicode", mailQueue.BodyJapaneseUnicode),
				new SqlParameter("@SubjectJapaneseUnicode", mailQueue.SubjectJapaneseUnicode),
				new SqlParameter("@LangID", mailQueue.LangID)
			);
		}
		
		public override void Update(MailQueue mailQueue, int id)
		{
			string query = @"
UPDATE MailQueue SET
	MailQueueID = @MailQueueID,
	ProjectRoundUserID = @ProjectRoundUserID,
	AdrTo = @AdrTo,
	AdrFrom = @AdrFrom,
	Subject = @Subject,
	Body = @Body,
	Sent = @Sent,
	SendType = @SendType,
	ErrorDescription = @ErrorDescription,
	BodyJapaneseUnicode = @BodyJapaneseUnicode,
	SubjectJapaneseUnicode = @SubjectJapaneseUnicode,
	LangID = @LangID
WHERE MailQueueID = @MailQueueID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MailQueueID", mailQueue.MailQueueID),
				new SqlParameter("@ProjectRoundUserID", mailQueue.ProjectRoundUserID),
				new SqlParameter("@AdrTo", mailQueue.AdrTo),
				new SqlParameter("@AdrFrom", mailQueue.AdrFrom),
				new SqlParameter("@Subject", mailQueue.Subject),
				new SqlParameter("@Body", mailQueue.Body),
				new SqlParameter("@Sent", mailQueue.Sent),
				new SqlParameter("@SendType", mailQueue.SendType),
				new SqlParameter("@ErrorDescription", mailQueue.ErrorDescription),
				new SqlParameter("@BodyJapaneseUnicode", mailQueue.BodyJapaneseUnicode),
				new SqlParameter("@SubjectJapaneseUnicode", mailQueue.SubjectJapaneseUnicode),
				new SqlParameter("@LangID", mailQueue.LangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MailQueue
WHERE MailQueueID = @MailQueueID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MailQueueID", id)
			);
		}
		
		public override MailQueue Read(int id)
		{
			string query = @"
SELECT 	MailQueueID, 
	ProjectRoundUserID, 
	AdrTo, 
	AdrFrom, 
	Subject, 
	Body, 
	Sent, 
	SendType, 
	ErrorDescription, 
	BodyJapaneseUnicode, 
	SubjectJapaneseUnicode, 
	LangID
FROM MailQueue
WHERE MailQueueID = @MailQueueID";
			MailQueue mailQueue = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MailQueueID", id))) {
				if (rs.Read()) {
					mailQueue = new MailQueue {
						MailQueueID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						AdrTo = GetString(rs, 2),
						AdrFrom = GetString(rs, 3),
						Subject = GetString(rs, 4),
						Body = GetString(rs, 5),
						Sent = GetDateTime(rs, 6),
						SendType = GetInt32(rs, 7),
						ErrorDescription = GetString(rs, 8),
						BodyJapaneseUnicode = GetString(rs, 9),
						SubjectJapaneseUnicode = GetString(rs, 10),
						LangID = GetInt32(rs, 11)
					};
				}
			}
			return mailQueue;
		}
		
		public override IList<MailQueue> FindAll()
		{
			string query = @"
SELECT 	MailQueueID, 
	ProjectRoundUserID, 
	AdrTo, 
	AdrFrom, 
	Subject, 
	Body, 
	Sent, 
	SendType, 
	ErrorDescription, 
	BodyJapaneseUnicode, 
	SubjectJapaneseUnicode, 
	LangID
FROM MailQueue";
			var mailQueues = new List<MailQueue>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					mailQueues.Add(new MailQueue {
						MailQueueID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						AdrTo = GetString(rs, 2),
						AdrFrom = GetString(rs, 3),
						Subject = GetString(rs, 4),
						Body = GetString(rs, 5),
						Sent = GetDateTime(rs, 6),
						SendType = GetInt32(rs, 7),
						ErrorDescription = GetString(rs, 8),
						BodyJapaneseUnicode = GetString(rs, 9),
						SubjectJapaneseUnicode = GetString(rs, 10),
						LangID = GetInt32(rs, 11)
					});
				}
			}
			return mailQueues;
		}
	}
}
