using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlOptionContainerRepository : BaseSqlRepository<OptionContainer>
	{
		public SqlOptionContainerRepository()
		{
		}
		
		public override void Save(OptionContainer optionContainer)
		{
			string query = @"
INSERT INTO OptionContainer(
	OptionContainerID, 
	OptionContainer
)
VALUES(
	@OptionContainerID, 
	@OptionContainer
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionContainerID", optionContainer.OptionContainerID),
				new SqlParameter("@OptionContainer", optionContainer.OptionContainerText)
			);
		}
		
		public override void Update(OptionContainer optionContainer, int id)
		{
			string query = @"
UPDATE OptionContainer SET
	OptionContainerID = @OptionContainerID,
	OptionContainer = @OptionContainer
WHERE OptionContainerID = @OptionContainerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionContainerID", optionContainer.OptionContainerID),
				new SqlParameter("@OptionContainer", optionContainer.OptionContainerText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM OptionContainer
WHERE OptionContainerID = @OptionContainerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionContainerID", id)
			);
		}
		
		public override OptionContainer Read(int id)
		{
			string query = @"
SELECT 	OptionContainerID, 
	OptionContainer
FROM OptionContainer
WHERE OptionContainerID = @OptionContainerID";
			OptionContainer optionContainer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionContainerID", id))) {
				if (rs.Read()) {
					optionContainer = new OptionContainer {
						OptionContainerID = GetInt32(rs, 0),
						OptionContainerText = GetString(rs, 1)
					};
				}
			}
			return optionContainer;
		}
		
		public override IList<OptionContainer> FindAll()
		{
			string query = @"
SELECT 	OptionContainerID, 
	OptionContainer
FROM OptionContainer";
			var optionContainers = new List<OptionContainer>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					optionContainers.Add(new OptionContainer {
						OptionContainerID = GetInt32(rs, 0),
						OptionContainerText = GetString(rs, 1)
					});
				}
			}
			return optionContainers;
		}
	}
}
