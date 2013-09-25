using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IIndexRepository : IBaseRepository<Index>
	{
		IList<Index> FindByLanguage(int id, int langID, int yearFrom, int yearTo, string sortString);
		
		Index ReadByIdAndLanguage(int idxID, int langID);
	}
	
	public class IndexRepositoryStub : BaseRepositoryStub<Index>, IIndexRepository
	{
		public IList<Index> FindByLanguage(int id, int langID, int yearFrom, int yearTo, string sortString)
		{
			var indexes = new List<Index>();
			for (int j = 0; j < 10; j++) {
				var i = new Index();
				i.AverageAX = 50;
				i.Languages = new List<IndexLanguage>(
					new IndexLanguage[] {
						new IndexLanguage { IndexName = "Index " + j }
					}
				);
				i.Id = j;
				i.CountDX = 10;
				indexes.Add(i);
			}
			return indexes;
		}
		
		public Index ReadByIdAndLanguage(int idxID, int langID)
		{
			throw new NotImplementedException();
		}
	}
}
