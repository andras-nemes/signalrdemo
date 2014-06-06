using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRWeb.Stocks
{
	public class Stock
	{
		public Stock(String name)
		{
			Name = name;
		}

		public string Name { get; set; }
		public double CurrentPrice 
		{
			get
			{
				Random random = new Random();				
				return random.NextDouble() * 10.0;
			}
		}

	}
}