using Microsoft.AspNet.SignalR.Hubs;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRWeb
{
	public class HubActivator : IHubActivator
	{
		private readonly IContainer container;

		public HubActivator(IContainer container)
		{
			this.container = container;
		}

		public IHub Create(HubDescriptor descriptor)
		{
			return (IHub)container.GetInstance(descriptor.HubType);
		}
	}
}