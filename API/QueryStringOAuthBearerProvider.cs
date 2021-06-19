using Microsoft.AspNetCore.SignalR;

namespace API
{
    public class QueryStringOAuthBearerProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
            //return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}