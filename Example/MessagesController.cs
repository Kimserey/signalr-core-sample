using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    [Route("messages")]
    public class MessagesController: ControllerBase
    {
        private IHubContext<ChatHub> _hub;

        public MessagesController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public async Task Post()
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", "System", "Hello World");
        }
    }
}
