using AspNetCoreQuartz;
using AspNetCoreQuartz.QuartzServices;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    //var conconcurrentJobKey = new JobKey("ConconcurrentJob");
    //q.AddJob<ConconcurrentJob>(opts => opts.WithIdentity(conconcurrentJobKey));
    //q.AddTrigger(opts => opts
    //    .ForJob(conconcurrentJobKey)
    //    .WithIdentity("ConconcurrentJob-trigger")
    //    .WithCronSchedule("0/4 * * * * ?"));

    var conconcurrentJobKey = new JobKey("ConconcurrentJob");
    q.AddJob<ConconcurrentJob>(opts => opts.WithIdentity(conconcurrentJobKey));
    q.AddTrigger(opts => opts
        .ForJob(conconcurrentJobKey)
        .WithIdentity("ConconcurrentJob-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(5)
            .RepeatForever()));

    //var nonConconcurrentJobKey = new JobKey("NonConconcurrentJob");
    //q.AddJob<NonConconcurrentJob>(opts => opts.WithIdentity(nonConconcurrentJobKey));
    //q.AddTrigger(opts => opts
    //    .ForJob(nonConconcurrentJobKey)
    //    .WithIdentity("NonConconcurrentJob-trigger")
    //    .WithCronSchedule("0/4 * * * * ?"));

    //var nonConconcurrentJobKey = new JobKey("NonConconcurrentJob");
    //q.AddJob<NonConconcurrentJob>(opts => opts.WithIdentity(nonConconcurrentJobKey));
    //q.AddTrigger(opts => opts
    //    .ForJob(nonConconcurrentJobKey)
    //    .WithIdentity("NonConconcurrentJob-trigger")
    //    .WithSimpleSchedule(x => x
    //        .WithIntervalInSeconds(5)
    //        .RepeatForever()));

});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<JobsHub>("/jobshub");
});

app.MapRazorPages();

app.Run();
