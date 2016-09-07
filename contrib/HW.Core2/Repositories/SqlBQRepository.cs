using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlBQRepository : BaseSqlRepository<BQ>
	{
		public SqlBQRepository()
		{
		}
		
		public override void Save(BQ bQ)
		{
			string query = @"
INSERT INTO BQ(
	BQID, 
	Internal, 
	Type, 
	ReqLength, 
	MaxLength, 
	DefaultVal, 
	Comparison, 
	MeasurementUnit, 
	Layout, 
	Variable, 
	InternalAggregate, 
	Restricted, 
	IncludeInDemographics
)
VALUES(
	@BQID, 
	@Internal, 
	@Type, 
	@ReqLength, 
	@MaxLength, 
	@DefaultVal, 
	@Comparison, 
	@MeasurementUnit, 
	@Layout, 
	@Variable, 
	@InternalAggregate, 
	@Restricted, 
	@IncludeInDemographics
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQID", bQ.BQID),
				new SqlParameter("@Internal", bQ.Internal),
				new SqlParameter("@Type", bQ.Type),
				new SqlParameter("@ReqLength", bQ.ReqLength),
				new SqlParameter("@MaxLength", bQ.MaxLength),
				new SqlParameter("@DefaultVal", bQ.DefaultVal),
				new SqlParameter("@Comparison", bQ.Comparison),
				new SqlParameter("@MeasurementUnit", bQ.MeasurementUnit),
				new SqlParameter("@Layout", bQ.Layout),
				new SqlParameter("@Variable", bQ.Variable),
				new SqlParameter("@InternalAggregate", bQ.InternalAggregate),
				new SqlParameter("@Restricted", bQ.Restricted),
				new SqlParameter("@IncludeInDemographics", bQ.IncludeInDemographics)
			);
		}
		
		public override void Update(BQ bQ, int id)
		{
			string query = @"
UPDATE BQ SET
	BQID = @BQID,
	Internal = @Internal,
	Type = @Type,
	ReqLength = @ReqLength,
	MaxLength = @MaxLength,
	DefaultVal = @DefaultVal,
	Comparison = @Comparison,
	MeasurementUnit = @MeasurementUnit,
	Layout = @Layout,
	Variable = @Variable,
	InternalAggregate = @InternalAggregate,
	Restricted = @Restricted,
	IncludeInDemographics = @IncludeInDemographics
WHERE BQID = @BQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQID", bQ.BQID),
				new SqlParameter("@Internal", bQ.Internal),
				new SqlParameter("@Type", bQ.Type),
				new SqlParameter("@ReqLength", bQ.ReqLength),
				new SqlParameter("@MaxLength", bQ.MaxLength),
				new SqlParameter("@DefaultVal", bQ.DefaultVal),
				new SqlParameter("@Comparison", bQ.Comparison),
				new SqlParameter("@MeasurementUnit", bQ.MeasurementUnit),
				new SqlParameter("@Layout", bQ.Layout),
				new SqlParameter("@Variable", bQ.Variable),
				new SqlParameter("@InternalAggregate", bQ.InternalAggregate),
				new SqlParameter("@Restricted", bQ.Restricted),
				new SqlParameter("@IncludeInDemographics", bQ.IncludeInDemographics)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM BQ
WHERE BQID = @BQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQID", id)
			);
		}
		
		public override BQ Read(int id)
		{
			string query = @"
SELECT 	BQID, 
	Internal, 
	Type, 
	ReqLength, 
	MaxLength, 
	DefaultVal, 
	Comparison, 
	MeasurementUnit, 
	Layout, 
	Variable, 
	InternalAggregate, 
	Restricted, 
	IncludeInDemographics
FROM BQ
WHERE BQID = @BQID";
			BQ bQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@BQID", id))) {
				if (rs.Read()) {
					bQ = new BQ {
						BQID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Type = GetInt32(rs, 2),
						ReqLength = GetInt32(rs, 3),
						MaxLength = GetInt32(rs, 4),
						DefaultVal = GetString(rs, 5),
						Comparison = GetInt32(rs, 6),
						MeasurementUnit = GetString(rs, 7),
						Layout = GetInt32(rs, 8),
						Variable = GetString(rs, 9),
						InternalAggregate = GetString(rs, 10),
						Restricted = GetInt32(rs, 11),
						IncludeInDemographics = GetInt32(rs, 12)
					};
				}
			}
			return bQ;
		}
		
		public override IList<BQ> FindAll()
		{
			string query = @"
SELECT 	BQID, 
	Internal, 
	Type, 
	ReqLength, 
	MaxLength, 
	DefaultVal, 
	Comparison, 
	MeasurementUnit, 
	Layout, 
	Variable, 
	InternalAggregate, 
	Restricted, 
	IncludeInDemographics
FROM BQ";
			var bQs = new List<BQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					bQs.Add(new BQ {
						BQID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Type = GetInt32(rs, 2),
						ReqLength = GetInt32(rs, 3),
						MaxLength = GetInt32(rs, 4),
						DefaultVal = GetString(rs, 5),
						Comparison = GetInt32(rs, 6),
						MeasurementUnit = GetString(rs, 7),
						Layout = GetInt32(rs, 8),
						Variable = GetString(rs, 9),
						InternalAggregate = GetString(rs, 10),
						Restricted = GetInt32(rs, 11),
						IncludeInDemographics = GetInt32(rs, 12)
					});
				}
			}
			return bQs;
		}
	}
}
