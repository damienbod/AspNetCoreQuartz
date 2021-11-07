using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreQuartz
{
    public class JobsHub : Hub
    {
        public Task SendMessage(string message)
        {
            return Clients.All.SendAsync("JobInfo",  message);
        }

    }
}
