using CalorieDiaryCalculator.Server;
using CalorieDiaryCalculator.Server.Data;
using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddDatabaseDeveloperPageExceptionFilter();

var applicationSettingsConfiguration = builder.Configuration.GetSection("ApplicationSettingsSection");

var appSettings = builder.Services.GetApplicationSettings(builder.Configuration);


builder.Services
    .AddDatabase(builder.Configuration)
    .AddIdentity()
    .AddJwtAuthentication(appSettings)
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.ApplyMigration();

app.Run();
