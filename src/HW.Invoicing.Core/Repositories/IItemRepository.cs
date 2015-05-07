using System;
using System.Collections.Generic;
using HW.Core.Repositories;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories
{
	public interface IItemRepository : IBaseRepository<Item>
	{
	}
	
	public class ItemRepositoryStub : BaseRepositoryStub<Item>, IItemRepository
	{
		public override IList<Item> FindAll()
		{
			return new[] {
				new Item { Name = "Item1" },
				new Item { Name = "Item2" },
				new Item { Name = "Item3" },
			};
		}
	}
}
