using System.Runtime.CompilerServices;
using Ebay.Api.Extensions;
using Ebay.Api.Middleware;
using Ebay.Application;
using Ebay.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    //app.ApplyMigrations();

    //await SeedAsync(app);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.UseAuthentication();

//app.UseAuthorization();

app.MapControllers();

app.Run();

async Task SeedAsync(IApplicationBuilder app)
{
    var scope = app.ApplicationServices.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var randomDataSeed = new RandomDataDbSeed(dbContext);

    await randomDataSeed.InputData();
}
