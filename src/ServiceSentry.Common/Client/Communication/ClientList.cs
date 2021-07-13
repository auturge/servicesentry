﻿using System.Collections.Generic;
using ServiceSentry.Common.Communication;

namespace ServiceSentry.Common.Client
{
    public abstract class ClientList
    {
        public abstract int Count { get; }
        public abstract ServiceClient<IMonitorService> GetClient(string machineName);
        public abstract ServiceClient<IMonitorService> RefreshClient(string machineName);

        public static ClientList GetInstance()
        {
            return GetInstance(ClientBuilder.GetInstance());
        }

        internal static ClientList GetInstance(ClientBuilder builder)
        {
            return new ClientListImplementation(builder);
        }

        private sealed class ClientListImplementation : ClientList
        {
            private readonly ClientBuilder _builder;
            private readonly Dictionary<string, ServiceClient<IMonitorService>> _clients;

            internal ClientListImplementation(ClientBuilder builder)
            {
                _builder = builder;
                _clients = new Dictionary<string, ServiceClient<IMonitorService>>();
            }

            public override int Count => _clients.Count;

            public override ServiceClient<IMonitorService> GetClient(string machineName)
            {
                foreach (var client in _clients)
                {
                    if (client.Key == machineName) return client.Value;
                }

                var newClient =
                    _builder.GetClient<IMonitorService>(machineName, Extensibility.Strings._AgentServiceName);
                _clients.Add(machineName, newClient);
                return newClient;
            }

            public override ServiceClient<IMonitorService> RefreshClient(string machineName)
            {
                _clients.Remove(machineName);
                return GetClient(machineName);
            }
        }
    }
}