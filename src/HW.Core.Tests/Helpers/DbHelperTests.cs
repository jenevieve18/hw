using System;
using HW.Core.Helpers;
using NUnit.Framework;
using System.Data.SqlClient;
using P = System.Data.SqlClient.SqlParameter;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class DbHelperTests
	{
		const string name = "Bruce Wayne";
		const string address = "1007 Mountain Drive, Gotham";
		const int age = 35;
		DateTime birthday = new DateTime(1982, 2, 19);
		const float assets = 31415926.54F;
		const string email = "bruce@wayneindustries.com";
		
		[SetUp]
		public void Setup()
		{
			DbHelper.ExecuteNonQuery(@"
create table heroes(
	id uniqueidentifier not null default newid(),
	name varchar(255),
	address varchar(255),
	age integer,
	has_powers bit,
	birthday datetime,
	assets float,
	email varchar(255)
)");
			DbHelper.ExecuteNonQuery(
				@"
insert into heroes(name, age, has_powers, birthday, assets, email)
values(@name, @age, @has_powers, @birthday, @assets, @email)",
				new P("@name", name),
				new P("@age", age),
				new P("@has_powers", false),
				new P("@birthday", birthday),
				new P("@assets", assets),
				new P("@email", email)
			);
		}
		
		[TearDown]
		public void Teardown()
		{
			DbHelper.ExecuteNonQuery(@"drop table heroes");
		}
		
		[Test]
		public void TestExecuteReader()
		{
			using (var r = DbHelper.ExecuteReader("select id, name, age, has_powers, birthday, assets, email from heroes where name = @name", new P("@name", name))) {
				Assert.IsTrue(r.Read());
				Assert.IsNotNull(DbHelper.GetGuid(r, 0));
				Assert.IsNotNull(DbHelper.GetGuid(r, 0, Guid.NewGuid()));
				Assert.AreEqual(name, DbHelper.GetString(r, 1));
				Assert.AreEqual(age, DbHelper.GetInt32(r, 2));
				Assert.IsFalse(DbHelper.GetBoolean(r, 3));
				Assert.AreEqual(birthday, DbHelper.GetDateTime(r, 4));
				Assert.AreEqual(assets, DbHelper.GetDouble(r, 5));
				Assert.AreEqual("info@wayneindustries.com", DbHelper.GetString(r, 6, email, "info@wayneindustries.com"));
			}
		}
		
		[Test]
		public void TestExecuteNonQuery()
		{
			DbHelper.ExecuteNonQuery(@"
update heroes set address = @address
where name = @name", new P("@address", address), new P("@name", name));
			
			using (var r = DbHelper.ExecuteReader("select address from heroes where name = @name", new P("@name", name))) {
				Assert.IsTrue(r.Read());
				Assert.AreEqual(address, DbHelper.GetString(r, 0));
			}
		}
		
		[Test]
		public void TestExecuteScalar()
		{
			int count = ConvertHelper.ToInt32(DbHelper.ExecuteScalar("select count(*) from heroes"));
			Assert.IsTrue(count > 0);
		}
	}
}
