using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using System.Linq;

namespace HW.Invoicing.Core.Repositories.Sql
{
    public class SqlLanguageRepository : BaseSqlRepository<Language>
    {
        public override IList<Language> FindAll()
        {
            string query = @"
    select id, name from lang";
            var l = new List<Language>();
            using (var rs = ExecuteReader(query, "invoicing")) 
            {
                while (rs.Read())
                {
                    l.Add(new Language { 
                        Id = GetInt32(rs, 0),
                        Name = GetString(rs, 1)
                    });
                }
            }
            return l;
        }
    }
}
