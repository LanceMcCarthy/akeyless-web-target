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
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthorization();

// Custom middleware for handling AKeyless request headers.
app.UseMiddleware<AkeylessAuthMiddleware>();

app.MapControllers();

app.Run();
