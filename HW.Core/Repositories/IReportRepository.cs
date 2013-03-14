//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IReportRepository : IBaseRepository<Report>
	{
		ReportPart ReadReportPart(int reportPartID);
		
		ReportPartComponent ReadComponentByPartAndLanguage(int reportPartID, int langID);
		
		IList<ReportPartComponent> FindComponentsByPartAndLanguage(int reportPartID, int langID);
		
		IList<ReportPartComponent> FindComponentsByPartAndLanguage2(int reportPartID, int langID);
		
		IList<ReportPartComponent> FindComponentsByPart(int reportPartID);
		
		IList<ReportPartComponent> FindComponents(int reportPartID);
		
		IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID);
		
		IList<ReportPartLanguage> FindPartLanguagesByReport(int reportID);
	}
	
	public class ReportRepositoryStub : BaseRepositoryStub<Report>, IReportRepository
	{
		public ReportPartComponent ReadComponentByPartAndLanguage(int reportPartID, int langID)
		{
			var c = new ReportPartComponent();
			c.QuestionOption = new WeightedQuestionOption {
				Id = 1,
				YellowLow = 40,
				GreenLow = 60,
				GreenHigh = 101,
				YellowHigh = 101,
				Question = new Question { Id = 1 },
				Option = new Option { Id = 1 }
			};
			return c;
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage(int reportPartID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage2(int reportPartID, int langID)
		{
			var components = new List<ReportPartComponent>();
			for (int i = 0; i < 10; i++) {
				var c = new ReportPartComponent();
				var q = new WeightedQuestionOption {
					Id = i,
					TargetValue = 10,
					YellowLow = 40,
					GreenLow = 60,
					GreenHigh = 101,
					YellowHigh = 101,
					Question = new Question { Id = 1 },
					Option = new Option { Id = 1 }
				};
				q.Languages = new List<WeightedQuestionOptionLanguage>(
					new WeightedQuestionOptionLanguage[] {
						new WeightedQuestionOptionLanguage { Question = "Question " + i }
					}
				);
				c.QuestionOption = q;
				components.Add(c);
			}
			return components;
		}
		
		public IList<ReportPartComponent> FindComponentsByPart(int reportPartID)
		{
			var components = new List<ReportPartComponent>();
			for (int i = 0; i < 10; i++) {
				var c = new ReportPartComponent();
				c.QuestionOption = new WeightedQuestionOption {
					Id = i,
					YellowLow = 40,
					GreenLow = 60,
					GreenHigh = 101,
					YellowHigh = 101,
					Question = new Question { Id = 1 },
					Option = new Option { Id = 1 }
				};
				components.Add(c);
			}
			return components;
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			var languages = new List<ReportPartLanguage>();
			var r = new Random();
			int[] types = new int[] { 2, 8, 9};
			for (int i = 0; i < 10; i++) {
				var l = new ReportPartLanguage {
					ReportPart = new ReportPart { Id = i, Type = types[r.Next(0, types.Length)] },
					Subject = "Subject " + i,
					Header = "Header " + i,
					Footer = "Footer " + i
				};
				languages.Add(l);
			}
			return languages;
		}
		
		public ReportPart ReadReportPart(int reportPartID)
		{
			int[] types = new int[] { 2, 8, 9 };
			return new ReportPart {
//				Type = types[new Random().Next(0, types.Length)],
				Type = 8,
				Components = new List<ReportPartComponent>(10),
				Question = new Question { Id = 1 },
				Option = new Option { Id = 1 },
				RequiredAnswerCount = 1,
				PartLevel = 1
			};
		}
		
		public IList<ReportPartComponent> FindComponents(int reportID)
		{
			var components = new List<ReportPartComponent>();
			var r = new Random();
			for (int i = 0; i < 10; i++) {
				var c = new ReportPartComponent();
				c.Id = i;
				c.Index = new Index {
					TargetValue = r.Next(0, 10),
					YellowLow = 40,
					GreenLow = 60,
					GreenHigh = 101,
					YellowHigh = 101
				};
				c.Index.Parts = new List<IndexPart>(10);
				components.Add(c);
			}
			return components;
		}
		
		public IList<ReportPartLanguage> FindPartLanguagesByReport(int reportID)
		{
			var languages = new List<ReportPartLanguage>();
			for (int i = 0; i < 10; i++) {
				var p = new ReportPartLanguage {
					ReportPart = new ReportPart { Id = i, Type = 3 },
					Subject = "Subject " + i,
					Header = "Header " + i,
					Footer = "Footer " + i,
				};
				languages.Add(p);
			}
			return languages;
		}
	}
}
