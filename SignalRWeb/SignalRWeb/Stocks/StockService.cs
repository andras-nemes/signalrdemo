using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRWeb.Stocks
{
	public class StockService : IStockService
	{
		private List<Stock> _stocks;

		public StockService()
		{
			_stocks = new List<Stock>();
			_stocks.Add(new Stock("GreatCompany"));
			_stocks.Add(new Stock("NiceCompany"));
			_stocks.Add(new Stock("EvilCompany"));
		}

		public dynamic GetStockPrices()
		{
			return _stocks.Select(s => new { name = s.Name, price = s.CurrentPrice });
		}
	}
}