using System;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using System.Collections.Generic;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlCompanyRepository : BaseSqlRepository<Company>
	{
        public void UnselectByUser(int userId)
        {
            string query = @"
UPDATE Company SET Selected = NULL
WHERE UserId = @UserId";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@UserId", userId));
        }

        public void SelectCompany(int companyId)
        {
            string query = @"
UPDATE Company SET Selected = 1
WHERE Id = @Id";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", companyId));
        }

        public override IList<Company> FindAll()
        {
            string query = @"
SELECT Id,
Name
FROM Company";
            var companies = new List<Company>();
            using (var rs = ExecuteReader(query, "invoicing"))
            {
                while (rs.Read())
                {
                    companies.Add(
                        new Company {
                            Id = GetInt32(rs, 0),
                            Name = GetString(rs, 1)
                        }
                    );
                }
            }
            return companies;
        }
		
		public override void Save(Company t)
		{
            string query = @"
INSERT INTO Company(Name, Address, Phone, BankAccountNumber, TIN, FinancialMonthStart, FinancialMonthEnd, HasSubscriber)
VALUES(@Name, @Address, @Phone, @BankAccountNumber, @TIN, @FinancialMonthStart, @FinancialMonthEnd, @HasSubscriber)";
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@Name", t.Name),
                new SqlParameter("@Address", t.Address),
                new SqlParameter("@Phone", t.Phone),
                new SqlParameter("@BankAccountNumber", t.BankAccountNumber),
                new SqlParameter("@TIN", t.TIN),
                new SqlParameter("@FinancialMonthStart", t.FinancialMonthStart),
                new SqlParameter("@FinancialMonthEnd", t.FinancialMonthEnd),
                new SqlParameter("@HasSubscriber", t.HasSubscriber)
            );
		}
		
		public override Company Read(int id)
		{
			string query = @"
SELECT Id,
    Name,
    Address,
    Phone,
    BankAccountNumber,
    TIN,
    FinancialMonthStart,
    FinancialMonthEnd,
    InvoicePrefix,
    HasSubscriber
FROM Company
WHERE Id = @Id";
			Company c = null;
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new Company {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Address = GetString(rs, 2),
						Phone = GetString(rs, 3),
						BankAccountNumber = GetString(rs, 4),
						TIN = GetString(rs, 5),
                        FinancialMonthStart = GetDateTime(rs, 6),
                        FinancialMonthEnd = GetDateTime(rs, 7),
                        InvoicePrefix = GetString(rs, 8),
                        HasSubscriber = GetInt32(rs, 9) == 1
					};
				}
			}
			return c;
		}

        public Company ReadSelectedCompanyByUser(int userId)
        {
            string query = @"
SELECT TOP 1 Id,
    Name,
    Address,
    Phone,
    BankAccountNumber,
    TIN,
    FinancialMonthStart,
    FinancialMonthEnd,
    ISNULL(Selected, 0) IsSelected
FROM Company
WHERE UserId = @UserId
ORDER BY IsSelected DESC";
            Company c = null;
            using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@UserId", userId)))
            {
                if (rs.Read())
                {
                    c = new Company
                    {
                        Id = GetInt32(rs, 0),
                        Name = GetString(rs, 1),
                        Address = GetString(rs, 2),
                        Phone = GetString(rs, 3),
                        BankAccountNumber = GetString(rs, 4),
                        TIN = GetString(rs, 5),
                        FinancialMonthStart = GetDateTime(rs, 6),
                        FinancialMonthEnd = GetDateTime(rs, 7)
                    };
                }
            }
            return c;
        }
		
		public override void Update(Company t, int id)
		{
			string query = @"
UPDATE Company set Name = @Name,
    Address = @Address,
    Phone = @Phone,
    BankAccountNumber = @BankAccountNumber,
    TIN = @TIN,
    FinancialMonthStart = @FinancialMonthStart,
    FinancialMonthEnd = @FinancialMonthEnd,
    InvoicePrefix = @InvoicePrefix,
    HasSubscriber = @HasSubscriber
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name),
				new SqlParameter("@Address", t.Address),
				new SqlParameter("@Phone", t.Phone),
				new SqlParameter("@BankAccountNumber", t.BankAccountNumber),
                new SqlParameter("@TIN", t.TIN),
                new SqlParameter("@FinancialMonthStart", t.FinancialMonthStart),
                new SqlParameter("@FinancialMonthEnd", t.FinancialMonthEnd),
                new SqlParameter("@InvoicePrefix", t.InvoicePrefix),
				new SqlParameter("@Id", id),
                new SqlParameter("@HasSubscriber", t.HasSubscriber)
			);
		}
	}
}
