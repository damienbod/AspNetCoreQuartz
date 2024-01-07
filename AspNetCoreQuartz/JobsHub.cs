using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreQuartz;

public class JobsHub : Hub
{
    public Task SendConcurrentJobsMessage(string message)
    {
        return Clients.All.SendAsync("ConcurrentJobs", message);
    }

    public Task SendNonConcurrentJobsMessage(string message)
    {
        return Clients.All.SendAsync("NonConcurrentJobs", message);
    }

}
