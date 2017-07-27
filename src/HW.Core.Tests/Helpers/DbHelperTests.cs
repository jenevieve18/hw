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
		const string name = "Joshua M. Densmore";
		const string address = "2289 Holly Street Charlotte, NC 28202";
		
		[SetUp]
		public void Setup()
		{
			DbHelper.ExecuteNonQuery(@"
create table tests(
	id integer not null primary key identity,
	name varchar(255),
	address varchar(255)
)");
			DbHelper.ExecuteNonQuery(@"
insert into tests(name)
values(@name)", new P("@name", name));
		}
		
		[TearDown]
		public void Teardown()
		{
			DbHelper.ExecuteNonQuery(@"drop table tests");
		}
		
		[Test]
		public void TestExecuteReader()
		{
			using (var r = DbHelper.ExecuteReader("select name from tests where name = @name", new P("@name", name))) {
				Assert.IsTrue(r.Read());
				Assert.AreEqual(name, DbHelper.GetString(r, 0));
			}
		}
		
		[Test]
		public void TestExecuteNonQuery()
		{
			DbHelper.ExecuteNonQuery(@"
update tests set address = @address
where name = @name", new P("@address", address), new P("@name", name));
			
			using (var r = DbHelper.ExecuteReader("select address from tests where name = @name", new P("@name", name))) {
				Assert.IsTrue(r.Read());
				Assert.AreEqual(address, DbHelper.GetString(r, 0));
			}
		}
		
		[Test]
		public void TestExecuteScalar()
		{
			int count = ConvertHelper.ToInt32(DbHelper.ExecuteScalar("select count(*) from tests"));
			Assert.IsTrue(count > 0);
		}
	}
}
