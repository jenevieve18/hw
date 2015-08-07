using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using System.Linq;

namespace HW.Invoicing.Core.Repositories.Sql
{
    public class SqlCurrencyRepository : BaseSqlRepository<Currency>
    {
        public override IList<Currency> FindAll()
        {
            string query = @"
    select id, code, name from currency";
            var currencies = new List<Currency>();
            using (var rs = ExecuteReader(query, "invoicing"))
            {
                while (rs.Read())
                {
                    currencies.Add(
                        new Currency { Id = GetInt32(rs, 0), Code = GetString(rs, 1), Name = GetString(rs, 2) }
                        );
                }
            }
            return currencies;
        }
    }
}
