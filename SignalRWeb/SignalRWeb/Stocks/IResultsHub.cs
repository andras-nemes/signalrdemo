using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWeb.Stocks
{
	public interface IResultsHub
	{
		void Hello();
		void SendMessage(String message);
	}
}
