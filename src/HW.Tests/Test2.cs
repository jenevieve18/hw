using System;
using System.Drawing;
using System.IO;
using HW.Core.Repositories.Sql;
using NUnit.Framework;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace HW.Tests
{
	[TestFixture]
	public class Test2
	{
		[Test]
		public void TestMethod()
		{
			string path = "http://localhost/hw/";
			string personalLink = "" + path + "";
			int reminderLink = 1;
			string userKey = "DODONG";
			string id = "ID";
			string body = "<LINK/>";
			if (reminderLink > 0)
			{
				personalLink += "/c/" + userKey.ToLower() + id.ToString();
			}
			if (body.IndexOf("<LINK/>") >= 0)
			{
				body = body.Replace("<LINK/>", personalLink);
			}
		}
		
		[Test]
		public void TestMethod2()
		{
			SqlSponsorRepository r = new SqlSponsorRepository();
			r.FindExtendedSurveysBySuperAdmin(1);
		}
	}
}
