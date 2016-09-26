using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlOptionComponentsRepository : BaseSqlRepository<OptionComponents>, IOptionComponentsRepository
	{
		public SqlOptionComponentsRepository()
		{
		}
		
		public override void Save(OptionComponents optionComponent)
		{
			string query = @"
INSERT INTO OptionComponents(
	OptionComponentsID, 
	OptionComponentID, 
	OptionID, 
	ExportValue, 
	SortOrder
)
VALUES(
	@OptionComponentsID, 
	@OptionComponentID, 
	@OptionID, 
	@ExportValue, 
	@SortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentsID", optionComponent.OptionComponentsID),
				new SqlParameter("@OptionComponentID", optionComponent.OptionComponentID),
				new SqlParameter("@OptionID", optionComponent.OptionID),
				new SqlParameter("@ExportValue", optionComponent.ExportValue),
				new SqlParameter("@SortOrder", optionComponent.SortOrder)
			);
		}
		
		public override void Update(OptionComponents optionComponent, int id)
		{
			string query = @"
UPDATE OptionComponents SET
	OptionComponentsID = @OptionComponentsID,
	OptionComponentID = @OptionComponentID,
	OptionID = @OptionID,
	ExportValue = @ExportValue,
	SortOrder = @SortOrder
WHERE OptionComponentsID = @OptionComponentsID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentsID", optionComponent.OptionComponentsID),
				new SqlParameter("@OptionComponentID", optionComponent.OptionComponentID),
				new SqlParameter("@OptionID", optionComponent.OptionID),
				new SqlParameter("@ExportValue", optionComponent.ExportValue),
				new SqlParameter("@SortOrder", optionComponent.SortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM OptionComponents
WHERE OptionComponentsID = @OptionComponentsID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentsID", id)
			);
		}
		
		public override OptionComponents Read(int id)
		{
			string query = @"
SELECT 	OptionComponentsID, 
	OptionComponentID, 
	OptionID, 
	ExportValue, 
	SortOrder
FROM OptionComponents
WHERE OptionComponentsID = @OptionComponentsID";
			OptionComponents optionComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionComponentsID", id))) {
				if (rs.Read()) {
					optionComponent = new OptionComponents {
						OptionComponentsID = GetInt32(rs, 0),
						OptionComponentID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						ExportValue = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					};
				}
			}
			return optionComponent;
		}
		
		public override IList<OptionComponents> FindAll()
		{
			string query = @"
SELECT 	OptionComponentsID, 
	OptionComponentID, 
	OptionID, 
	ExportValue, 
	SortOrder
FROM OptionComponents";
			var optionComponents = new List<OptionComponents>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					optionComponents.Add(new OptionComponents {
						OptionComponentsID = GetInt32(rs, 0),
						OptionComponentID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						ExportValue = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					});
				}
			}
			return optionComponents;
		}
		
		public IList<OptionComponents> FindByOption(int optionID)
		{
			string query = @"
SELECT 	OptionComponentsID, 
	OptionComponentID, 
	OptionID, 
	ExportValue, 
	SortOrder
FROM OptionComponents
WHERE OptionID = @OptionID";
			var optionComponents = new List<OptionComponents>();
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionID", optionID))) {
				while (rs.Read()) {
					optionComponents.Add(new OptionComponents {
						OptionComponentsID = GetInt32(rs, 0),
						OptionComponentID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						ExportValue = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					});
				}
			}
			return optionComponents;
		}
	}
}
