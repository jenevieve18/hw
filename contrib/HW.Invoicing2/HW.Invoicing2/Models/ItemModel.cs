using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing2.Models
{
    public class ItemModel
    {
        public IList<Item> Items { get; set; }
    }
}