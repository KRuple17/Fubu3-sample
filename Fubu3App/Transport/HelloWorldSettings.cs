using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fubu3App.Endpoints;
using FubuMVC.Core.Security.Authentication.Saml2.Xml;
using FubuMVC.Core.ServiceBus.Configuration;
using FubuMVC.Core.ServiceBus.Runtime.Cascading;

namespace Fubu3App.Transport
{
    public class PingMessage
    {
        public string Message { get; set; }

        public PingMessage()
        {
        }
    }

    public class PongMessage
    {
        public string Message { get; set; }

        public PongMessage()
        {
        }
    }

    public class PresenterMessage
    {
        public string Message { get; set; }

        public PresenterMessage()
        {
        }
    }

    public class PingHandler
    {
        public RespondToSender Consume(PingMessage ping)
        {
            var pingText = ping.Message;
            Console.WriteLine("Ping Text... " + pingText);
            return new RespondToSender(new PongMessage {Message = "Received Message: " + pingText});
        }
    }

    public class PongHandler
    {
        //public string Consume(PresenterMessage pong)
        //{
        //    Console.WriteLine("Received Pong..." + pong.Message);
        //    return pong.Message;
        //}
        //public PresenterMessage Consume(PongMessage pong)
        //{
        //    Console.WriteLine("Received Pong..." + pong.Message);
        //    return new PresenterMessage {Message = pong.Message};
        //}
    }


    public class HelloWorldSettings
    {
        public Uri Pinger { get; set; } =
            "lq.tcp://localhost:2117/pinger".ToUri();

        public Uri Ponger { get; set; } =
            "lq.tcp://localhost:2118/ponger".ToUri();

        public Uri Presenter { get; set; } =
            "lq.tcp://localhose:2212/presenter".ToUri();
    }

    public class PingApp : FubuTransportRegistry<HelloWorldSettings>
    {
        public PingApp()
        {
            Channel(x => x.Pinger)
                .AcceptsMessage<PingMessage>();

            Channel(x => x.Pinger)
                .ReadIncoming();
        }
    }

    public class PongApp : FubuTransportRegistry<HelloWorldSettings>
    {
        public PongApp()
        {
            //Channel(x => x.Ponger)
            //    .ReadIncoming();

            Channel(x => x.Presenter)
                .ReadIncoming();

            Channel(x => x.Presenter)
                .AcceptsMessage<PongMessage>();
        }
    }
}