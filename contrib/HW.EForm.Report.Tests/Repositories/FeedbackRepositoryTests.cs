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
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class FeedbackRepositoryStub : BaseRepositoryStub<Feedback>, IFeedbackRepository
	{
		public FeedbackRepositoryStub()
		{
			items.Add(new Feedback { FeedbackID = 1, FeedbackText = "Feedback1" });
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
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 1 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 2 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 3 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 4 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 5 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 6 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 7 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 8 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 9 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 10 });
			items.Add(new FeedbackQuestion { FeedbackID = 1, QuestionID = 11 });
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
