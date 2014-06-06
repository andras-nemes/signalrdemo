using SignalRWeb.Stocks;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRWeb
{
	public static class IoC
	{
		public static IContainer Initialise()
		{
			ObjectFactory.Initialize(x =>
				{
					x.Scan(scan =>
						{
							scan.AssemblyContainingType<IResultsHub>();
							scan.WithDefaultConventions();
						});
				});
			return ObjectFactory.Container;
		}
	}
}