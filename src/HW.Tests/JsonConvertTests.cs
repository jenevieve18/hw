using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HW.Tests
{
	[TestFixture]
	public class JsonConvertTests
	{
		[Test]
		public void TestMethod()
		{
			Dictionary<string, string> udemy = new Dictionary<string, string>();
			udemy.Add("name", "Media Relations");
			udemy.Add("email", "press@udemy.com");
			udemy.Add("notes", "Member of the press? We'd love to hear from you.");
			string js = JsonConvert.SerializeObject(udemy);
			Console.WriteLine(js);
		}
		
		[Test]
		public void TestMethod3()
		{
			List<Dictionary<string, string>> contacts = new List<Dictionary<string, string>>();
			Dictionary<string, string> c1 = new Dictionary<string, string>();
			c1.Add("name", "Media Relations");
			c1.Add("email", "press@udemy.com");
			c1.Add("notes", "Member of the press? We'd love to hear from you.");
			contacts.Add(c1);
			
			Dictionary<string, string> c2 = new Dictionary<string, string>();
			c2.Add("name", "Business Development");
			c2.Add("email", "bizdev@udemy.com");
			c2.Add("notes", "For \"general\" business development inquiries.");
			contacts.Add(c2);
			
			string js = JsonConvert.SerializeObject(contacts);
			Console.WriteLine(js);
		}
		
		[Test]
		public void TestMethod5()
		{
			var contacts = new List<object>();
			contacts.Add(new { name = "Media Relations", email = "press@udemy.com", notes = "\"Member\" of the press? We'd love to hear from you." });
			contacts.Add(new { name = "Business Development", email = "bizdev@udemy.com", notes = "For general business development inquiries." });
			string js = JsonConvert.SerializeObject(contacts);
			Console.WriteLine(js);
		}
		
		[Test]
		public void TestMethod2()
		{
			string source = @"[
{
'name': 'Media Relations',
'email': 'press@udemy.com',
'notes': 'Member of the media? Wed love to hear from you.'
},
{
'name': 'Business Development',
'email': 'bizdev@udemy.com',
'notes': 'For general business development inquiries.'
}
]";
			Dictionary<string, string>[] contacts = JsonConvert.DeserializeObject<Dictionary<string,string>[]>(source);
			
			Console.WriteLine(contacts[0]["name"]);
		}
	}
}
