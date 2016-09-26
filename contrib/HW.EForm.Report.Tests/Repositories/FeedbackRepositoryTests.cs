// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using System.Collections.Generic;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class FeedbackRepositoryTests
	{
		IFeedbackRepository fr = new FeedbackRepositoryStub();
		
		[Test]
		public void TestMethod()
		{
			var f = fr.Read(1);
			Assert.AreEqual("HME", f.FeedbackText);
		}
	}
	
	public class FeedbackRepositoryStub : BaseRepositoryStub<Feedback>, IFeedbackRepository
	{
		public FeedbackRepositoryStub()
		{
			items.Add(new Feedback { FeedbackID = 1, FeedbackText = "HME" });
		}
		
		public override Feedback Read(int id)
		{
			return items.Find(x => x.FeedbackID == id);
		}
	}
	
	public class FeedbackQuestionRepositoryStub : BaseRepositoryStub<FeedbackQuestion>, IFeedbackQuestionRepository
	{
		public FeedbackQuestionRepositoryStub()
		{
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 1, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 2, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 3, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 4, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 5, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 6, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 7, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 8, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 9, OptionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 10, OptionID = 1 });
			
			items.Add(new FeedbackQuestion { FeedbackID = 1, IdxID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, IdxID = 2 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, IdxID = 3 });
		}
		
		public IList<FeedbackQuestion> FindByFeedback(int feedbackID)
		{
			return items.FindAll(x => x.FeedbackID == feedbackID);
		}
		
		public IList<FeedbackQuestion> FindByQuestions(int feedbackID, int[] questionIDs)
		{
			throw new NotImplementedException();
		}
	}
}
