using Microsoft.AspNetCore.SignalR;

namespace Example
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("sub")?.Value;
        }
    }
}
