using AspNetCoreQuartz.QuartzServices;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

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
});

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
