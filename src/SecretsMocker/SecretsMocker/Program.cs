using Microsoft.AspNetCore.HttpOverrides;
using SecretsMocker.Authorization;
using SecretsMocker.Helpers;
using Serilog;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.File("Logs/Accessors.log")
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddHttpClient();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.6"));
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.7"));
    options.KnownProxies.Add(IPAddress.Parse("172.17.0.1"));
    options.KnownProxies.Add(IPAddress.Parse("172.21.0.1"));
    options.KnownProxies.Add(IPAddress.Parse("172.22.0.1"));
    options.KnownProxies.Add(IPAddress.Parse("172.23.0.1"));
    options.KnownProxies.Add(IPAddress.Parse("192.168.1.214"));
});

builder.Services
    .AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
    })
    .AddJsonOptions(options =>
    {
        options.AllowInputFormatterExceptionMessages = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseAuthorization();

// Custom middleware for handling AKeyless request headers.
app.UseMiddleware<AkeylessAuthMiddleware>();

app.MapControllers();

//var option = new RewriteOptions();
//option.AddRedirect("^$", "swagger/index.html");
//app.UseRewriter(option);

app.MapGet("/", async (HttpContext context, HttpResponse response) =>
{
    try
    {
        var ip = "";
        var host = "Client";

        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            ip = context.GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();

            if (context.Request.Headers.ContainsKey("X-Forwarded-Host"))
            {
                host = context.GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();
            }
        }
        else
        {
            ip = context.Connection.RemoteIpAddress?.ToString();
        }

        Log.Warning($" ==> {host} at {ip} <==");
    }
    catch (Exception e)
    {
        Log.Error($"Could not read IP addresses or forwarded headers.");
        Log.Error(e.ToString());
    }

    await response.WriteAsync(@"<p>This app does not have a frontend, please go to the <a href=""swagger/index.html""> swagger page</a> instead.</p>");
});

app.Run();
