using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DB;
using Common;

var builder = WebApplication.CreateBuilder(args);

#region Configure Serilog Logging
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.AddSerilog(logger);
#endregion

#region Add Services to the DI Container
// Register the db context
var dbConnection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(dbConnection));

// Register CRUDService and Others
builder.Services.AddScoped<ICRUDService, CRUDService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

// Allow CORS
builder.Services.AddCors();
#endregion

#region Build the App and Configure the HTTP request pipeline
var app = builder.Build();
// log the db connection
app.Logger.LogInformation($"Db Connection: {dbConnection}");

// swagger docs
app.UseSwagger(options => options.SerializeAsV2 = true);
app.UseSwaggerUI(options =>
{
    var swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
    options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Riyadh Amana API");
});

// global exception handler
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
            await context.Response.WriteAsync(JsonSerializer.Serialize(contextFeature.Error));
    });
});

app.UseStaticFiles();

app.UseRouting();

app.UseCors(config => config
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
   );

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion 
