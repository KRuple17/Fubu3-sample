using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using Fubu3App.Transport;
using FubuMVC.Core;
using FubuMVC.Core.ServiceBus;

namespace Fubu3App.Endpoints
{

    public class HomeEndpoint
    {
        public IServiceBus _serviceBus;
        private string _transportMsg = "It didnt work..";

        public HomeEndpoint(IServiceBus bus)
        {
            _serviceBus = bus;
        }

        //overides the default translation of the action to a url (in this case, the get_index() with a default url pattern of "/index" will now have a blank url pattern, effectively turning it into the home page)
        public HomeViewModel Index()
        {
            //_serviceBus.Send(new PingMessage { Message = "Hello Transport World"});
            var pongMsg = GetAsyncResponse("Hello Transport").Result;
            if (pongMsg != null)
            {
                return new HomeViewModel { Text = pongMsg.Message };
            }
            return new HomeViewModel { Text = _transportMsg };
        }

        public async Task<PongMessage> GetAsyncResponse(string message)
        {
            var response = await _serviceBus.Request<PongMessage>(new PingMessage {Message = message});
            this._transportMsg = response.Message;
            return response;
        }

        public LinkViewModel LinkAction()
        {
            return new LinkViewModel { Text = "Keenen", Number = 17};
        }

        public TimeViewModel get_Time()
        {
            return new TimeViewModel {Time = System.DateTime.Now};
        }
    }

    public class HomeViewModel
    {
        public string Text { get; set; }
    }

    public class LinkViewModel
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }

    public class TimeViewModel
    {
        public DateTime Time { get; set; }
    }

    public class TimeInputModel
    {
        
    }
}