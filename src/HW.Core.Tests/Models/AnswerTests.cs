using System;
using System.IO;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Core.Tests.Models
{
	[TestFixture]
	public class AnswerTests
	{
		[Test]
		public void TestMethod()
		{
			string x = @"4	4	4	4	4	4	4	4	4
3	3	3	3	3	3	3	3	3
2	2	2	2	2	2	2	2	2
1	1	1	1	1	1	1	1	1
0	0	0	0	0	0	0	0	0
1	1	1	1	1	1	1	1	1
2	2	2	2	2	2	2	2	2
3	3	3	3	3	3	3	3	3
4	3	2	1	0	1	2	3	4
2	3	1	3	4	0	4	0	4
";
			using (var r = new StringReader(x)) {
				string line = "";
				while ((line = r.ReadLine()) != null) {
					string[] s = line.Split('\t');
					for (int i = 0; i < 9; i++) {
						Console.Write(s[i] + ";");
					}
					Console.WriteLine();
				}
			}
		}
	}
}
