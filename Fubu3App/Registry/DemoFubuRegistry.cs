using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fubu3App.Transport;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.ServiceBus;
using FubuMVC.Core.ServiceBus.Configuration;
using FubuMVC.Core.ServiceBus.Events;
using FubuMVC.Core.ServiceBus.Runtime;
using FubuMVC.Core.ServiceBus.Runtime.Cascading;
using FubuMVC.Core.ServiceBus.Runtime.Invocation;
using FubuMVC.Core.ServiceBus.Runtime.Serializers;
using FubuMVC.Core.ServiceBus.Subscriptions;
using FubuMVC.LightningQueues;
using FubuMVC.RavenDb.RavenDb;

namespace Fubu3App.Registry
{
    public class DemoFubuRegistry : FubuTransportRegistry<HelloWorldSettings>
    {
        public DemoFubuRegistry()
        {
            //grab all classes that are suffixed with Endpoint and turn the public methods into actions
            //This will occur by default, only placed in here for understanding
            Actions.IncludeClassesSuffixedWithEndpoint();
            
            ConfigureServiceBus();

            //Setup for the IOC (Inverson of Control) container using StructureMap (Fubu3 only supports StructureMap)
            Services.IncludeRegistry<CoreRegistry>();
            //Services.IncludeRegistry<LightningQueuesServiceRegistry>();
            //Services.IncludeRegistry<ServiceRegistry>();
            //Services.For<IServiceBus>().Use<ServiceBus>();
            //Services.For<IServiceBus>().Use(x => x.GetInstance<ServiceBus>());
            //Services.For<IEnvelopeSender>().Use(x => x.GetInstance<EnvelopeSender>());
            //Services.For<IEnvelopeSerializer>().Use(x => x.GetInstance<EnvelopeSerializer>());
            //Services.For<ISubscriptionCache>().Use(x => x.GetInstance<SubscriptionCache>());
            //Services.For<IEventAggregator>().Use(x => x.GetInstance<EventAggregator>());
            //Services.For<IChainInvoker>().Use(x => x.GetInstance<ChainInvoker>());
            //Services.For<IOutgoingSender>().Use(x => x.GetInstance<OutgoingSender>()); 
            //Services.For<ISubscriptionRepository>().Use(x => x.GetInstance<SubscriptionRepository>());


            //Enables the Fubu3 Diagnostics - Very useful for troubleshooting your application (access via "localhost:port#/_fubu")
            Features.Diagnostics.Enable(TraceLevel.Verbose);
        }

        private void ConfigureServiceBus()
        {
            Channel(x => x.Ponger)
                .AcceptsMessage<PingMessage>();

            Channel(x => x.Ponger)
                .ReadIncoming();

            Channel(x => x.Pinger)
                .AcceptsMessage<PongMessage>();

            Channel(x => x.Pinger)
                .ReadIncoming();

        }
    }
}