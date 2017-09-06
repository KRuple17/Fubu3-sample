using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FubuMVC.Core.ServiceBus;

namespace Fubu3App.Transport
{
    public class SendingExample
    {
        public async Task SendPingsAndPongs(IServiceBus serviceBus, string msg)
        {
            serviceBus.Send(new PingMessage());

            var pong = await serviceBus.Request<PongMessage>(new PingMessage {Message = msg});

            serviceBus.DelaySend(new PingMessage(), TimeSpan.FromSeconds(15));
        }
    }
}