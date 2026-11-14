using Ebay.Api.Middleware;
using Ebay.Application;
using Ebay.Infrastructure;
using Typesense;

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

    // Uncomment to seed data and create relevant typesense collections
    await SeedAsync(app);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task SeedAsync(IApplicationBuilder app)
{
    var scope = app.ApplicationServices.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var typesenseClient = scope.ServiceProvider.GetRequiredService<ITypesenseClient>();

    var dbInitializer = new DbInitializer(dbContext, typesenseClient);

    await dbInitializer.Init();
}
