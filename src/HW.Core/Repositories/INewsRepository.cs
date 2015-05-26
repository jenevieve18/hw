using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface INewsRepository
	{
		IList<AdminNews> FindTop3AdminNews();
	}
	
	public class NewsRepositoryStub : BaseRepositoryStub<AdminNews>, INewsRepository
	{
		public IList<AdminNews> FindTop3AdminNews()
		{
			return new [] {
				new AdminNews {},
				new AdminNews {},
				new AdminNews {}
			};
		}
	}
}
