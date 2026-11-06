using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OmniReserve.Application;
using OmniReserve.Infrastructure;
using OmniReserve.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var jwtSettingsSection = builder.Configuration.GetSection(JwtSettings.SectionName);
var secret = jwtSettingsSection["Secret"];
var issuer = jwtSettingsSection["Issuer"];
var audience = jwtSettingsSection["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            
            ValidateAudience = true,
            ValidAudience = audience,
            
            ValidateLifetime = true,
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<OmniReserve.Api.Middlewares.GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();