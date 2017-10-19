﻿using Gluon.Relay.Contracts;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public class AppServiceHost<TService> : IServiceHost /*, IServiceHost<TService>*/ where TService : class, IServiceType
    {
        public ICommunicationClient Hub { get; private set; }
        public string InstanceId { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceHost() { }
        public AppServiceHost(string instanceId, string subscriptionChannel)
        {
            this.InstanceId = instanceId ?? typeof(TService).Name;
            var qs = $"?{ClientSpecEnum.ClientId}={InstanceId}";
            this.SubscriptionChannel = (subscriptionChannel ?? "http://localhost:5000/messagehub") + qs;
            this.Hub = new MessageHubServiceClient<TService>(this.InstanceId, this.SubscriptionChannel, this);
        }
        //public TService CreateServiceInstance()
        //{
        //    return Activator.CreateInstance<TService>();
        //}

        IServiceType IServiceHost.CreateServiceInstance(Type serviceType)
        {
            return (IServiceType)Activator.CreateInstance(serviceType);
        }
    }
}

