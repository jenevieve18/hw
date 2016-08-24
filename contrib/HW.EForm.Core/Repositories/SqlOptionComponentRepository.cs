using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlOptionComponentRepository : BaseSqlRepository<OptionComponent>
	{
		public SqlOptionComponentRepository()
		{
		}
		
		public override void Save(OptionComponent optionComponent)
		{
			string query = @"
INSERT INTO OptionComponent(
	OptionComponentID, 
	ExportValue, 
	Internal, 
	OptionComponentContainerID
)
VALUES(
	@OptionComponentID, 
	@ExportValue, 
	@Internal, 
	@OptionComponentContainerID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentID", optionComponent.OptionComponentID),
				new SqlParameter("@ExportValue", optionComponent.ExportValue),
				new SqlParameter("@Internal", optionComponent.Internal),
				new SqlParameter("@OptionComponentContainerID", optionComponent.OptionComponentContainerID)
			);
		}
		
		public override void Update(OptionComponent optionComponent, int id)
		{
			string query = @"
UPDATE OptionComponent SET
	OptionComponentID = @OptionComponentID,
	ExportValue = @ExportValue,
	Internal = @Internal,
	OptionComponentContainerID = @OptionComponentContainerID
WHERE OptionComponentID = @OptionComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentID", optionComponent.OptionComponentID),
				new SqlParameter("@ExportValue", optionComponent.ExportValue),
				new SqlParameter("@Internal", optionComponent.Internal),
				new SqlParameter("@OptionComponentContainerID", optionComponent.OptionComponentContainerID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM OptionComponent
WHERE OptionComponentID = @OptionComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentID", id)
			);
		}
		
		public override OptionComponent Read(int id)
		{
			string query = @"
SELECT 	OptionComponentID, 
	ExportValue, 
	Internal, 
	OptionComponentContainerID
FROM OptionComponent
WHERE OptionComponentID = @OptionComponentID";
			OptionComponent optionComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionComponentID", id))) {
				if (rs.Read()) {
					optionComponent = new OptionComponent {
						OptionComponentID = GetInt32(rs, 0),
						ExportValue = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						OptionComponentContainerID = GetInt32(rs, 3)
					};
				}
			}
			return optionComponent;
		}
		
		public override IList<OptionComponent> FindAll()
		{
			string query = @"
SELECT 	OptionComponentID, 
	ExportValue, 
	Internal, 
	OptionComponentContainerID
FROM OptionComponent";
			var optionComponents = new List<OptionComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					optionComponents.Add(new OptionComponent {
						OptionComponentID = GetInt32(rs, 0),
						ExportValue = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						OptionComponentContainerID = GetInt32(rs, 3)
					});
				}
			}
			return optionComponents;
		}
	}
}
