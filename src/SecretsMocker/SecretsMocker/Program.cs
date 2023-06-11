using Microsoft.AspNetCore.Rewrite;
using SecretsMocker.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

// Custom middleware for handling AKeyless request headers.
app.UseMiddleware<AkeylessAuthMiddleware>();

app.MapControllers();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger/index.html");
app.UseRewriter(option);

app.Run();
