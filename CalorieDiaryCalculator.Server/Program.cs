using CalorieDiaryCalculator.Server;
using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Services.GetApplicationSettings(builder.Configuration);

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter()
    .AddDatabase(builder.Configuration)
    .AddIdentity()
    .AddJwtAuthentication(appSettings)
    .AddApplicationServices()
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}

app
    .UseRouting()
    .UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader())
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => {
        endpoints.MapControllers();
    })
    .ApplyMigration();

app.Run();
