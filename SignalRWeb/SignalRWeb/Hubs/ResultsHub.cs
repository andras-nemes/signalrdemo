using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using SignalRWeb.Stocks;

namespace SignalRWeb.Hubs
{
	public class ResultsHub : Hub, IResultsHub
    {
		private readonly IStockService _stockService;

		public ResultsHub(IStockService stockService)
		{
			if (stockService == null) throw new ArgumentNullException("StockService");
			_stockService = stockService;
			//StartStockMonitoring();			
		}

		private void StartStockMonitoring()
		{
			Task stockMonitoringTask = Task.Factory.StartNew(async () =>
				{					
					while(true)
					{
						dynamic stockPriceCollection = _stockService.GetStockPrices();
						
						Clients.All.newStockPrices(stockPriceCollection);
						await Task.Delay(5000);
					}
				}, TaskCreationOptions.LongRunning);
		}

        public void Hello()
        {			
			Clients.All.hello("Welcome from SignalR!");
        }

		public void SendMessage(String message)
		{
			IRequest req = Context.Request;
			string completeMessage = string.Concat(Context.ConnectionId
				, " has registered the following message: ", message);

			Clients.All.registerMessage(completeMessage);
		}

		public void JoinAppropriateRoom(int age)
		{
			string roomName = FindRoomName(age);
			string connectionId = Context.ConnectionId;
			JoinRoom(connectionId, roomName);
			string completeMessage = string.Concat("Connection ", connectionId, " has joined the room called ", roomName);
			Clients.All.registerMessage(completeMessage);
		}

		public void DispatchMessage(string message, int age)
		{			
			string roomName = FindRoomName(age);			
			string completeMessage = string.Concat(Context.ConnectionId
				, " has registered the following message: ", message, ". Their age is ", age, ". ");
			Clients.Group(roomName).registerMessage(completeMessage);
		}

		private Task JoinRoom(string connectionId, string roomName)
		{			
			return Groups.Add(connectionId, roomName);
		}		

		private string FindRoomName(int age)
		{
			string roomName = "Default";
			if (age < 18)
			{
				roomName = "The young ones";
			}
			else if (age < 65)
			{
				roomName = "Still working";
			}
			else
			{
				roomName = "Old age pensioners";
			}
			return roomName;
		}
    }
}