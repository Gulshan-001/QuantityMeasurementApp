using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add YARP Reverse Proxy (reads config from appsettings.json)
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Add MVC for the custom unified history controller
builder.Services.AddControllers();

// HttpClient for downstream calls from history aggregator
builder.Services.AddHttpClient("compare",  client => client.BaseAddress = new Uri(builder.Configuration["Services:Compare"]!));
builder.Services.AddHttpClient("convert",  client => client.BaseAddress = new Uri(builder.Configuration["Services:Convert"]!));
builder.Services.AddHttpClient("arithmetic", client => client.BaseAddress = new Uri(builder.Configuration["Services:Arithmetic"]!));

// JWT Authentication — gateway validates tokens before forwarding
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// CORS — allow Angular frontend (any origin in dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

// Map custom controllers FIRST (history aggregator takes priority over YARP for these routes)
app.MapControllers();

// YARP handles everything else
app.MapReverseProxy();

app.Run();
