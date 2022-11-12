using CalorieDiaryCalculator.Server.Infrastructure.Extensions;
using CalorieDiaryCalculator.Server.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Services.GetApplicationSettings(builder.Configuration);

builder.Services
    .AddDatabaseDeveloperPageExceptionFilter()
    .AddDatabase(builder.Configuration)
    .AddIdentity()
    .AddJwtAuthentication(appSettings)
    .AddApplicationServices()
    .AddSwagger()
    .AddApiControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}

app
    .UseSwaggerUI()
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
