using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlOptionComponentContainerRepository : BaseSqlRepository<OptionComponentContainer>
	{
		public SqlOptionComponentContainerRepository()
		{
		}
		
		public override void Save(OptionComponentContainer optionComponentContainer)
		{
			string query = @"
INSERT INTO OptionComponentContainer(
	OptionComponentContainerID, 
	OptionComponentContainer
)
VALUES(
	@OptionComponentContainerID, 
	@OptionComponentContainer
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentContainerID", optionComponentContainer.OptionComponentContainerID),
				new SqlParameter("@OptionComponentContainer", optionComponentContainer.OptionComponentContainerText)
			);
		}
		
		public override void Update(OptionComponentContainer optionComponentContainer, int id)
		{
			string query = @"
UPDATE OptionComponentContainer SET
	OptionComponentContainerID = @OptionComponentContainerID,
	OptionComponentContainer = @OptionComponentContainer
WHERE OptionComponentContainerID = @OptionComponentContainerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentContainerID", optionComponentContainer.OptionComponentContainerID),
				new SqlParameter("@OptionComponentContainer", optionComponentContainer.OptionComponentContainerText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM OptionComponentContainer
WHERE OptionComponentContainerID = @OptionComponentContainerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentContainerID", id)
			);
		}
		
		public override OptionComponentContainer Read(int id)
		{
			string query = @"
SELECT 	OptionComponentContainerID, 
	OptionComponentContainer
FROM OptionComponentContainer
WHERE OptionComponentContainerID = @OptionComponentContainerID";
			OptionComponentContainer optionComponentContainer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionComponentContainerID", id))) {
				if (rs.Read()) {
					optionComponentContainer = new OptionComponentContainer {
						OptionComponentContainerID = GetInt32(rs, 0),
						OptionComponentContainerText = GetString(rs, 1)
					};
				}
			}
			return optionComponentContainer;
		}
		
		public override IList<OptionComponentContainer> FindAll()
		{
			string query = @"
SELECT 	OptionComponentContainerID, 
	OptionComponentContainer
FROM OptionComponentContainer";
			var optionComponentContainers = new List<OptionComponentContainer>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					optionComponentContainers.Add(new OptionComponentContainer {
						OptionComponentContainerID = GetInt32(rs, 0),
						OptionComponentContainerText = GetString(rs, 1)
					});
				}
			}
			return optionComponentContainers;
		}
	}
}
