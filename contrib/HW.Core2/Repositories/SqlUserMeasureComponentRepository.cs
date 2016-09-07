using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserMeasureComponentRepository : BaseSqlRepository<UserMeasureComponent>
	{
		public SqlUserMeasureComponentRepository()
		{
		}
		
		public override void Save(UserMeasureComponent userMeasureComponent)
		{
			string query = @"
INSERT INTO UserMeasureComponent(
	UserMeasureComponentID, 
	UserMeasureID, 
	MeasureComponentID, 
	ValInt, 
	ValDec, 
	ValTxt
)
VALUES(
	@UserMeasureComponentID, 
	@UserMeasureID, 
	@MeasureComponentID, 
	@ValInt, 
	@ValDec, 
	@ValTxt
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserMeasureComponentID", userMeasureComponent.UserMeasureComponentID),
				new SqlParameter("@UserMeasureID", userMeasureComponent.UserMeasureID),
				new SqlParameter("@MeasureComponentID", userMeasureComponent.MeasureComponentID),
				new SqlParameter("@ValInt", userMeasureComponent.ValInt),
				new SqlParameter("@ValDec", userMeasureComponent.ValDec),
				new SqlParameter("@ValTxt", userMeasureComponent.ValTxt)
			);
		}
		
		public override void Update(UserMeasureComponent userMeasureComponent, int id)
		{
			string query = @"
UPDATE UserMeasureComponent SET
	UserMeasureComponentID = @UserMeasureComponentID,
	UserMeasureID = @UserMeasureID,
	MeasureComponentID = @MeasureComponentID,
	ValInt = @ValInt,
	ValDec = @ValDec,
	ValTxt = @ValTxt
WHERE UserMeasureComponentID = @UserMeasureComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserMeasureComponentID", userMeasureComponent.UserMeasureComponentID),
				new SqlParameter("@UserMeasureID", userMeasureComponent.UserMeasureID),
				new SqlParameter("@MeasureComponentID", userMeasureComponent.MeasureComponentID),
				new SqlParameter("@ValInt", userMeasureComponent.ValInt),
				new SqlParameter("@ValDec", userMeasureComponent.ValDec),
				new SqlParameter("@ValTxt", userMeasureComponent.ValTxt)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserMeasureComponent
WHERE UserMeasureComponentID = @UserMeasureComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserMeasureComponentID", id)
			);
		}
		
		public override UserMeasureComponent Read(int id)
		{
			string query = @"
SELECT 	UserMeasureComponentID, 
	UserMeasureID, 
	MeasureComponentID, 
	ValInt, 
	ValDec, 
	ValTxt
FROM UserMeasureComponent
WHERE UserMeasureComponentID = @UserMeasureComponentID";
			UserMeasureComponent userMeasureComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserMeasureComponentID", id))) {
				if (rs.Read()) {
					userMeasureComponent = new UserMeasureComponent {
						UserMeasureComponentID = GetInt32(rs, 0),
						UserMeasureID = GetInt32(rs, 1),
						MeasureComponentID = GetInt32(rs, 2),
						ValInt = GetInt32(rs, 3),
						ValDec = GetDecimal(rs, 4),
						ValTxt = GetString(rs, 5)
					};
				}
			}
			return userMeasureComponent;
		}
		
		public override IList<UserMeasureComponent> FindAll()
		{
			string query = @"
SELECT 	UserMeasureComponentID, 
	UserMeasureID, 
	MeasureComponentID, 
	ValInt, 
	ValDec, 
	ValTxt
FROM UserMeasureComponent";
			var userMeasureComponents = new List<UserMeasureComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userMeasureComponents.Add(new UserMeasureComponent {
						UserMeasureComponentID = GetInt32(rs, 0),
						UserMeasureID = GetInt32(rs, 1),
						MeasureComponentID = GetInt32(rs, 2),
						ValInt = GetInt32(rs, 3),
						ValDec = GetDecimal(rs, 4),
						ValTxt = GetString(rs, 5)
					});
				}
			}
			return userMeasureComponents;
		}
	}
}
