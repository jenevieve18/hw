using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureComponentRepository : BaseSqlRepository<MeasureComponent>
	{
		public SqlMeasureComponentRepository()
		{
		}
		
		public override void Save(MeasureComponent measureComponent)
		{
			string query = @"
INSERT INTO MeasureComponent(
	MeasureComponentID, 
	MeasureID, 
	MeasureComponent, 
	Type, 
	Required, 
	SortOrder, 
	Unit, 
	Decimals, 
	ShowInList, 
	ShowUnitInList, 
	ShowInGraph, 
	Inherit, 
	AutoScript
)
VALUES(
	@MeasureComponentID, 
	@MeasureID, 
	@MeasureComponent, 
	@Type, 
	@Required, 
	@SortOrder, 
	@Unit, 
	@Decimals, 
	@ShowInList, 
	@ShowUnitInList, 
	@ShowInGraph, 
	@Inherit, 
	@AutoScript
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentID", measureComponent.MeasureComponentID),
				new SqlParameter("@MeasureID", measureComponent.MeasureID),
				new SqlParameter("@MeasureComponent", measureComponent.Component),
				new SqlParameter("@Type", measureComponent.Type),
				new SqlParameter("@Required", measureComponent.Required),
				new SqlParameter("@SortOrder", measureComponent.SortOrder),
				new SqlParameter("@Unit", measureComponent.Unit),
				new SqlParameter("@Decimals", measureComponent.Decimals),
				new SqlParameter("@ShowInList", measureComponent.ShowInList),
				new SqlParameter("@ShowUnitInList", measureComponent.ShowUnitInList),
				new SqlParameter("@ShowInGraph", measureComponent.ShowInGraph),
				new SqlParameter("@Inherit", measureComponent.Inherit),
				new SqlParameter("@AutoScript", measureComponent.AutoScript)
			);
		}
		
		public override void Update(MeasureComponent measureComponent, int id)
		{
			string query = @"
UPDATE MeasureComponent SET
	MeasureComponentID = @MeasureComponentID,
	MeasureID = @MeasureID,
	MeasureComponent = @MeasureComponent,
	Type = @Type,
	Required = @Required,
	SortOrder = @SortOrder,
	Unit = @Unit,
	Decimals = @Decimals,
	ShowInList = @ShowInList,
	ShowUnitInList = @ShowUnitInList,
	ShowInGraph = @ShowInGraph,
	Inherit = @Inherit,
	AutoScript = @AutoScript
WHERE MeasureComponentID = @MeasureComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentID", measureComponent.MeasureComponentID),
				new SqlParameter("@MeasureID", measureComponent.MeasureID),
				new SqlParameter("@MeasureComponent", measureComponent.Component),
				new SqlParameter("@Type", measureComponent.Type),
				new SqlParameter("@Required", measureComponent.Required),
				new SqlParameter("@SortOrder", measureComponent.SortOrder),
				new SqlParameter("@Unit", measureComponent.Unit),
				new SqlParameter("@Decimals", measureComponent.Decimals),
				new SqlParameter("@ShowInList", measureComponent.ShowInList),
				new SqlParameter("@ShowUnitInList", measureComponent.ShowUnitInList),
				new SqlParameter("@ShowInGraph", measureComponent.ShowInGraph),
				new SqlParameter("@Inherit", measureComponent.Inherit),
				new SqlParameter("@AutoScript", measureComponent.AutoScript)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureComponent
WHERE MeasureComponentID = @MeasureComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentID", id)
			);
		}
		
		public override MeasureComponent Read(int id)
		{
			string query = @"
SELECT 	MeasureComponentID, 
	MeasureID, 
	MeasureComponent, 
	Type, 
	Required, 
	SortOrder, 
	Unit, 
	Decimals, 
	ShowInList, 
	ShowUnitInList, 
	ShowInGraph, 
	Inherit, 
	AutoScript
FROM MeasureComponent
WHERE MeasureComponentID = @MeasureComponentID";
			MeasureComponent measureComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureComponentID", id))) {
				if (rs.Read()) {
					measureComponent = new MeasureComponent {
						MeasureComponentID = GetInt32(rs, 0),
						MeasureID = GetInt32(rs, 1),
						Component = GetString(rs, 2),
						Type = GetInt32(rs, 3),
						Required = GetInt32(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Unit = GetString(rs, 6),
						Decimals = GetInt32(rs, 7),
						ShowInList = GetInt32(rs, 8),
						ShowUnitInList = GetInt32(rs, 9),
						ShowInGraph = GetInt32(rs, 10),
						Inherit = GetInt32(rs, 11),
						AutoScript = GetString(rs, 12)
					};
				}
			}
			return measureComponent;
		}
		
		public override IList<MeasureComponent> FindAll()
		{
			string query = @"
SELECT 	MeasureComponentID, 
	MeasureID, 
	MeasureComponent, 
	Type, 
	Required, 
	SortOrder, 
	Unit, 
	Decimals, 
	ShowInList, 
	ShowUnitInList, 
	ShowInGraph, 
	Inherit, 
	AutoScript
FROM MeasureComponent";
			var measureComponents = new List<MeasureComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureComponents.Add(new MeasureComponent {
						MeasureComponentID = GetInt32(rs, 0),
						MeasureID = GetInt32(rs, 1),
						Component = GetString(rs, 2),
						Type = GetInt32(rs, 3),
						Required = GetInt32(rs, 4),
						SortOrder = GetInt32(rs, 5),
						Unit = GetString(rs, 6),
						Decimals = GetInt32(rs, 7),
						ShowInList = GetInt32(rs, 8),
						ShowUnitInList = GetInt32(rs, 9),
						ShowInGraph = GetInt32(rs, 10),
						Inherit = GetInt32(rs, 11),
						AutoScript = GetString(rs, 12)
					});
				}
			}
			return measureComponents;
		}
	}
}
