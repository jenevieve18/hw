using System;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlCompanyRepository : BaseSqlRepository<Company>
	{
		public SqlCompanyRepository()
		{
		}
		
		public override void Save(Company t)
		{
			base.Save(t);
		}
		
		public override Company Read(int id)
		{
			string query = @"
select Id, Name, Address, Phone, BankAccountNumber, TIN
from Company
where Id = @Id";
			Company c = null;
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new Company {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Address = GetString(rs, 2),
						Phone = GetString(rs, 3),
						BankAccountNumber = GetString(rs, 4),
						TIN = GetString(rs, 5)
					};
				}
			}
			return c;
		}
		
		public override void Update(Company t, int id)
		{
			string query = @"
update company set name = @Name,
Address = @Address,
Phone = @Phone,
BankAccountNumber = @BankAccountNumber,
TIN = @TIN
where id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name),
				new SqlParameter("@Address", t.Address),
				new SqlParameter("@Phone", t.Phone),
				new SqlParameter("@BankAccountNumber", t.BankAccountNumber),
				new SqlParameter("@TIN", t.Name),
				new SqlParameter("@Id", id)
			);
		}
	}
}
