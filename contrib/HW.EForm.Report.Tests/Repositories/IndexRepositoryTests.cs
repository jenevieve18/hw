// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class IndexRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
			var ir = new SqlIndexRepository();
			var ipcr = new SqlIndexPartComponentRepository();
			
			var i = ir.Read(14);
		}
	}
	
	public class IndexRepositoryStub : BaseRepositoryStub<Index>
	{
		public IndexRepositoryStub()
		{
//			var i1 = new Index { Internal = "HME Motivation" };
//			i1.AddPart(new IndexPart { QuestionID = 1, OptionID = 1 });
//			i1.AddPart(new IndexPart { QuestionID = 2, OptionID = 2 });
//			i1.AddPart(new IndexPart { QuestionID = 3, OptionID = 3 });
//			
//			var i2 = new Index { Internal = "HME Leadership" };
//			i2.AddPart(new IndexPart { QuestionID = 4, OptionID = 4 });
//			i2.AddPart(new IndexPart { QuestionID = 5, OptionID = 5 });
//			i2.AddPart(new IndexPart { QuestionID = 6, OptionID = 6 });
//			
//			var i3= new Index { Internal = "HME Styrning" };
//			i3.AddPart(new IndexPart { QuestionID = 7, OptionID = 7 });
//			i3.AddPart(new IndexPart { QuestionID = 8, OptionID = 8 });
//			i3.AddPart(new IndexPart { QuestionID = 9, OptionID = 9 });
//			
//			items.Add(i1);
//			items.Add(i2);
//			items.Add(i3);
			
			items.Add(new Index { IdxID = 1, MaxVal = 12 });
			items.Add(new Index { IdxID = 2, MaxVal = 12 });
			items.Add(new Index { IdxID = 3, MaxVal = 12 });
		}
	}
	
	public class IndexPartRepositoryStub : BaseRepositoryStub<IndexPart>
	{
		public IndexPartRepositoryStub()
		{
			items.Add(new IndexPart { IdxID = 1, QuestionID = 1, OptionID = 1 });
			items.Add(new IndexPart { IdxID = 1, QuestionID = 2, OptionID = 1 });
			items.Add(new IndexPart { IdxID = 1, QuestionID = 3, OptionID = 1 });
			
			items.Add(new IndexPart { IdxID = 2, QuestionID = 4, OptionID = 1 });
			items.Add(new IndexPart { IdxID = 2, QuestionID = 5, OptionID = 1 });
			items.Add(new IndexPart { IdxID = 2, QuestionID = 6, OptionID = 1 });
			
			items.Add(new IndexPart { IdxID = 3, QuestionID = 7, OptionID = 1 });
			items.Add(new IndexPart { IdxID = 3, QuestionID = 8, OptionID = 1 });
			items.Add(new IndexPart { IdxID = 3, QuestionID = 9, OptionID = 1 });
		}
	}
}
