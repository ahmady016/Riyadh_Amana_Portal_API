using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

using DB;
using Common;
using Auth;

using Users;
using Advertisements;
using Articles;
using _News;
using _ContactUs;
using Awards;
using Partners;
using Documents;
using AppFeatures;

var builder = WebApplication.CreateBuilder(args);

#region Configure Serilog Logging
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.AddSerilog(logger);
#endregion

#region Add Services to the DI Container
// pass Configuration object to AuthHelpers static class
AuthHelpers.Initialize(builder.Configuration);

// Register the db context
var dbConnection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(dbConnection));

// Register CRUDService
builder.Services.AddScoped<ICRUDService, CRUDService>();

// Register Features Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IAppFeatureService, AppFeatureService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Allow CORS
builder.Services.AddCors();

// Register Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = AuthHelpers.GetTokenValidationOptions(validateLifetime: true);
        options.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = context => {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    context.Response.Headers.Add("Token-Expired", "true");
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

// Register APIs
builder.Services
    .AddControllers(config => config.Filters.Add(typeof(APIExceptionFilter)))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Register swagger APIs docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Build the App and Configure the HTTP request pipeline
var app = builder.Build();

// global exception handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

// swagger docs
app.UseSwagger(options => options.SerializeAsV2 = true);
app.UseSwaggerUI();

// Allow CORS
app.UseCors(config => config
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
   );

// Serve Static Files
app.UseStaticFiles();

// setup API routes
app.UseRouting();

// API routes
app.MapControllers();

// handle client side routes [catch all routes for SPA]
app.MapFallbackToFile("index.html");

app.Run();
#endregion
