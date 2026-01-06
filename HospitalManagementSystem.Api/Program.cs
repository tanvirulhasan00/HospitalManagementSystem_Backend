using System.Text;
using Asp.Versioning;
using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Services.IService;
using HospitalManagementSystem.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Services
// ---------------------------
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

// ---------------------------
// Database
// ---------------------------
builder.Services.AddDbContext<HMSDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnectionString"))
);

// ---------------------------
// Identity
// ---------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<HMSDbContext>()
    .AddDefaultTokenProviders();

// ---------------------------
// Scoped services
// ---------------------------
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<ICheckerService, CheckerService>();
builder.Services.AddScoped<IDbInitializerService, DbInitializerService>();

// ---------------------------
// OpenAPI (default .NET 10) - JSON only
builder.Services.AddEndpointsApiExplorer(); // generates /openapi/v1.json
// ⚠ Do NOT call AddSwaggerGen() or MapSwaggerUI()

// ---------------------------
// API Versioning
// ---------------------------
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddApiVersioning()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// ---------------------------
// JWT Authentication
// ---------------------------
var key = builder.Configuration.GetValue<string>("TokenSetting:SecretKey") ?? "";

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
    ValidateIssuer = false,
    ValidateAudience = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParams;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", "")
                        ?? context.Request.Cookies["access_token"];
            context.Token = token;
            return Task.CompletedTask;
        }
    };
});

// ---------------------------
// CORS
// ---------------------------
const string allowedOrigin = "http://localhost:51452";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ---------------------------
// Middleware pipeline
// ---------------------------
app.UseHttpsRedirection();
app.UseCors("AllowCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();

// ---------------------------
// Map OpenAPI JSON only
app.MapOpenApi(); // generates /openapi/v1.json automatically
// ❌ Do NOT use Swagger UI

// ---------------------------
// DB Initialization
// ---------------------------
var cString = builder.Configuration.GetConnectionString("LocalConnectionString") ?? "";
var isOk = await DbHelperService.ChecksDbConnection(app.Services, cString);
if (!isOk)
    return;

await DbHelperService.SeedDatabaseAsync(app.Services);

// ---------------------------
// Run
// ---------------------------
app.Run();