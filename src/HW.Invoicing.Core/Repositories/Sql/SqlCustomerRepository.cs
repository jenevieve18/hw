using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlCustomerRepository : BaseSqlRepository<Customer>
	{
		public SqlCustomerRepository()
		{
		}
		
		public void Save(Customer c)
		{
			string query = string.Format(
				@"
INSERT INTO Customer(Name)
VALUES(@Name)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", c.Name)
			);
		}
		
		public override IList<Customer> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id, Name
FROM Customer"
			);
			var customers = new List<Customer>();
			using (SqlDataReader rs = Db.rs(query, "invoicing")) {
				while (rs.Read()) {
					customers.Add(
						new Customer { Id = GetInt32(rs, 0), Name = GetString(rs, 1) }
					);
				}
			}
			return customers;
		}
	}
}
