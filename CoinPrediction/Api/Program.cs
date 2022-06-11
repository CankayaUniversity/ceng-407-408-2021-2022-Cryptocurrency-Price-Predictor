using System.Diagnostics;
using System.Text;
using Application.Api;
using Application.IoC;
using Application.Logging;
using Business.Common.IoC;
using Data.Common.IoC;
using Data.Context;
using Authorization.Authorization;
using Shared.Logging;
using Shared.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

ProjectConfiguration.Configuration = configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("*");
            //builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddEndpointsApiExplorer();



#region JWT
var audienceConfig = configuration.GetSection("Audience");
var xmlKey = File.ReadAllText(@"Authorization//rsa-public-key.xml");
var key = JwtKeyHelper.BuildRsaSigningKey(xmlKey);
var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = key,
    ValidateIssuer = true,
    ValidIssuer = audienceConfig["Iss"],
    ValidateAudience = true,
    ValidAudience = audienceConfig["Aud"],
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    RequireExpirationTime = true
};
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = tokenValidationParameters;
});
builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);

    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

#endregion



builder.Services.AddEntityFrameworkSqlServer().AddDbContext<IDbContext, ProjectDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));//,opt=>opt.CommandTimeout(60) timeout s�resini ayarlama

builder.Services.AddBusinessLayer();
builder.Services.AddDataLayer();
builder.Services.AddApplicationLayer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("api", new OpenApiInfo() { Title = "API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Swagger v1"));
app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.UseMiddleware<CurrentUserMiddleware>();
app.UseMiddleware<LogMiddleware>();
app.MapControllers();

app.Run();