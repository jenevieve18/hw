﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Customer : BaseModel
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Number { get; set; }
	}
	
	public class Unit : BaseModel
	{
		public string Name { get; set; }
		public bool Inactive { get; set; }
	}
	
	public class CustomerItem : BaseModel
	{
		public Customer Customer { get; set; }
		public Item Item { get; set; }
		public decimal Price { get; set; }
		public bool Inactive { get; set; }
	}
	
	public class CustomerContact : BaseModel
	{
		public Customer Customer { get; set; }
		public string Contact { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public bool Inactive { get; set; }
		public int Type { get; set; }
		
		public string GetContactType()
		{
			switch (Type) {
					case 1: return "<span class='label label-success'>Primary</span>";
					case 2: return "<span class='label label-warning'>Secondary</span>";
					default: return "<span class='label label-default'>Other</span>";
			}
		}
	}
	
	public class CustomerNotes : BaseModel
	{
		public Customer Customer { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedAt { get; set; }
		public User CreatedBy { get; set; }
		public bool Inactive { get; set; }
	}
	
	public class CustomerTimebook : BaseModel
	{
		public DateTime? Date { get; set; }
		public CustomerContact Contact { get; set; }
		public Item Item { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public string Consultant { get; set; }
		public string Comments { get; set; }
		public string Department { get; set; }
        public bool Inactive { get; set; }
        public int Status { get; set; }
		public decimal Amount {
			get { return Price * Quantity; }
		}

        public string GetStatus()
        {
            switch (Status)
            {
                case 1: return "<span class='label label-warning'>INVOICED</span>";
                case 2: return "<span class='label label-success'>PAID</span>";
                default: return "";
            }
        }
	}
}
