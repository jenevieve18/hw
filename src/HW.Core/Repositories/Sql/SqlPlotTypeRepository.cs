using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlPlotTypeRepository : BaseSqlRepository<PlotType>, IPlotTypeRepository
    {
        public int Save(PlotType p)
        {
            string query = string.Format(
                @"
INSERT INTO PlotType(Name, Description)
VALUES(@Name, @Description)"
                );
            ExecuteNonQuery(query, "eFormSqlConnection",
                new SqlParameter("@Name", p.Name),
                new SqlParameter("@Description", p.Description)
            );
            query = string.Format(
                @"
SELECT TOP 1 Id FROM PlotType ORDER BY Id DESC"
            );
            return (int)ExecuteScalar(query, "eFormSqlConnection");
        }

        public override void Update(PlotType p, int plotTypeId)
        {
            string query = string.Format(
                @"
UPDATE PlotType SET Name = @Name, Description = @Description
WHERE Id = @Id"
                );
            ExecuteNonQuery(query, "eFormSqlConnection",
                new SqlParameter("@Name", p.Name),
                new SqlParameter("@Description", p.Description),
                new SqlParameter("@Id", plotTypeId)
            );
        }

        public void SaveLanguage(PlotTypeLanguage p)
        {
            string query = string.Format(
                @"
INSERT INTO PlotTypeLang(Name, Description, PlotTypeID, LangID)
VALUES(@Name, @Description, @PlotTypeID, @LangID)"
            );
            ExecuteNonQuery(query, "eFormSqlConnection",
                new SqlParameter("@Name", p.Name),
                new SqlParameter("@Description", p.Description),
                new SqlParameter("@PlotTypeID", p.PlotType.Id),
                new SqlParameter("@LangID", p.Language.Id)
            );
        }

        public void UpdateLanguage(PlotTypeLanguage p, int plotTypeLangId)
        {
            string query = string.Format(
                @"
UPDATE PlotTypeLang SET Name = @Name,
Description = @Description
WHERE PlotTypeLangId = @PlotTypeLangId"
            );
            ExecuteNonQuery(query, "eFormSqlConnection",
                new SqlParameter("@Name", p.Name),
                new SqlParameter("@Description", p.Description),
                new SqlParameter("@PlotTypeLangId", plotTypeLangId)
            );
        }

        public List<PlotTypeLanguage> GetLanguagesWithPlotType(int plotTypeID)
        {
            string query = string.Format(
                @"
SELECT
  l.LangID,
  ptl.PlotTypeLangID
FROM healthWatch..Lang l
LEFT OUTER JOIN PlotTypeLang ptl
  ON l.LangID = (ptl.LangID - 1)
  AND ptl.PlotTypeID = @PlotTypeID"
            );
            var p = new List<PlotTypeLanguage>();
            using (SqlDataReader rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@PlotTypeID", plotTypeID)))
            {
                while (rs.Read())
                {
                    var t = new PlotTypeLanguage()
                    {
                        Language = new Language { Id = GetInt32(rs, 0) },
                        Id = GetInt32(rs, 1)
                    };
                    p.Add(t);
                }
            }
            return p;
        }

		public override IList<PlotType> FindAll()
		{
			string query = string.Format(
				@"
SELECT *
FROM PlotType"
			);
			var types = new List<PlotType>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var t = new PlotType() {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2)
					};
					types.Add(t);
				}
			}
			return types;
		}

        public List<PlotTypeLanguage> FindAllLanguages(int plotTypeId)
        {
            string query = string.Format(
                @"
SELECT PlotTypeLangID,
    PlotTypeID,
    LangID,
    Name,
    Description,
    ShortName,
    SupportsMultipleSeries
FROM PlotTypeLang
WHERE PlotTypeId = @PlotTypeId"
            );
            var l = new List<PlotTypeLanguage>();
            using (SqlDataReader rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@PlotTypeId", plotTypeId)))
            {
                while (rs.Read())
                {
                    var p = new PlotTypeLanguage()
                    {
                        Id = GetInt32(rs, 0),
                        PlotType = new PlotType { Id = GetInt32(rs, 1) }, 
                        Language = new Language { Id = GetInt32(rs, 2) },
                        Name = GetString(rs, 3),
                        Description = GetString(rs, 4),
                        ShortName = GetString(rs, 5),
                        SupportsMultipleSeries = GetInt32(rs, 6) == 1
                    };
                    l.Add(p);
                }
            }
            return l;
        }
		
		public override PlotType Read(int id)
		{
			string query = string.Format(
				@"
SELECT *
FROM PlotType
WHERE Id = @Id"
			);
			PlotType p = null;
			using (SqlDataReader rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					p = new PlotType() {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2)
					};
				}
			}
			return p;
		}
		
		public IList<PlotTypeLanguage> FindByLanguage(int langID)
		{
			string query = string.Format(
				@"
SELECT PlotTypeID, 
	Name, 
	Description, 
	ShortName,
	SupportsMultipleSeries
FROM PlotTypeLang
WHERE LangID = {0}",
				langID
			);
			var types = new List<PlotTypeLanguage>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var t = new PlotTypeLanguage() {
						PlotType = new PlotType { Id = GetInt32(rs, 0) },
						Name = GetString(rs, 1),
						Description = GetString(rs, 2),
						ShortName = GetString(rs, 3),
						SupportsMultipleSeries = GetInt32(rs, 4) == 1
					};
					types.Add(t);
				}
			}
			return types;
		}
	}
}
