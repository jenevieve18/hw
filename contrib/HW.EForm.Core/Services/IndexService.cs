// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class IndexService
	{
		SqlIndexRepository indexRepo = new SqlIndexRepository();
		SqlIndexLangRepository indexLangRepo = new SqlIndexLangRepository();
		SqlIndexPartRepository indexPartRepo = new SqlIndexPartRepository();
		SqlIndexPartComponentRepository indexPartComponentRepo = new SqlIndexPartComponentRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		
		public IndexService()
		{
		}
		
		public Index ReadIndex(int indexID)
		{
			var i = indexRepo.Read(indexID);
			i.Languages = indexLangRepo.FindByIndex(indexID);
			i.Parts = indexPartRepo.FindByIndex(indexID);
			foreach (var ip in i.Parts) {
				ip.Components = indexPartComponentRepo.FindByPart(ip.IdxPartID);
				ip.Question = questionRepo.Read(ip.QuestionID);
				ip.Option = optionRepo.Read(ip.OptionID);
				foreach (var ipc in ip.Components) {
					ipc.OptionComponent = optionComponentRepo.Read(ipc.OptionComponentID);
				}
			}
			return i;
		}
		
		public Index Lalala(int indexID, int langID, int groupID, string sql)
		{
			return indexRepo.Lalala(indexID, langID, groupID, sql);
		}
		
		public IList<Index> FindAllIndexes()
		{
			var indexes = indexRepo.FindAll();
			foreach (var i in indexes) {
				i.Parts = indexPartRepo.FindByIndex(i.IdxID);
				foreach (var ip in i.Parts) {
					ip.Components = indexPartComponentRepo.FindByPart(ip.IdxPartID);
					ip.Question = questionRepo.Read(ip.QuestionID);
					ip.Option = optionRepo.Read(ip.OptionID);
					foreach (var ipc in ip.Components) {
						ipc.OptionComponent = optionComponentRepo.Read(ipc.OptionComponentID);
					}
				}
			}
			return indexes;
		}
	}
}
