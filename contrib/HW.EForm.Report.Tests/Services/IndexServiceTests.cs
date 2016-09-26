// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class IndexServiceTests
	{
		IndexService s = new IndexService();
		
		[Test]
		public void TestFindAllIndexes()
		{
			foreach (var i in s.FindAllIndexes()) {
				Console.WriteLine("IdxID: {0}, MaxVal: {1}", i.IdxID, i.MaxVal);
				foreach (var ip in i.Parts) {
					Console.WriteLine("\tQuestion: {0}, Option: {1}", ip.HasQuestion ? ip.Question.Internal : "", ip.HasOption ? ip.Option.Internal : "");
					foreach (var ipc in ip.Components) {
						Console.WriteLine("\t\tOptionComponent: {0}, Val: {1}, ExportValue: {2}", ipc.OptionComponent.Internal, ipc.Val, ipc.OptionComponent.ExportValue);
					}
				}
			}
		}
		
		[Test]
		public void TestReadIndex()
		{
			var i = s.ReadIndex(14);
			Console.WriteLine("IdxID: {0}, MaxVal: {1}", i.IdxID, i.MaxVal);
			foreach (var ip in i.Parts) {
				Console.WriteLine("\tQuestionID: {2}, Question: {0}, Option: {1}", ip.HasQuestion ? ip.Question.Internal : "", ip.HasOption ? ip.Option.Internal : "", ip.QuestionID);
				foreach (var ipc in ip.Components) {
					Console.WriteLine("\t\tVal: {1}, ExportValue: {2}, OptionComponentID: {3}, OptionComponent: {0}", ipc.OptionComponent.Internal, ipc.Val, ipc.OptionComponent.ExportValue, ipc.OptionComponentID);
				}
			}
		}
	}
}
