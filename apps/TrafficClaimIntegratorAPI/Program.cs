using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register background jobs
builder.Services.AddScoped<TrafficPollerJob>();

// Add Hangfire
builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

// Add HTTP client for external traffic API
builder.Services.AddHttpClient("TrafficApi", client =>
{
    client.BaseAddress = new Uri("https://traffic.vendor.com"); // TODO: replace with real API
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard();
}
RecurringJob.AddOrUpdate<TrafficPollerJob>(
    "traffic-poller-job",
    job => job.ExecuteAsync(),
    Cron.Minutely());

app.UseHttpsRedirection();

app.Run();