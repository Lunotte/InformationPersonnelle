using InformationPersonnelle.Server.Data;
using InformationPersonnelle.Server.Repositories;
using InformationPersonnelle.Server.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var ConnectionStringInformationPersonnelleDbContext = "Server=localhost;Database=test;Username=postgres;Password=postgres";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connString = builder.Configuration.GetConnectionString(nameof(InformationPersonnelleDbContext));
builder.Services.AddDbContext<InformationPersonnelleDbContext>(options => options.UseNpgsql(connString));

//builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
    });
}

//migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dataContext = services.GetRequiredService<InformationPersonnelleDbContext>();
        dataContext.Database.EnsureDeleted();
        var migrationEnAttente = dataContext.Database.GetPendingMigrations().Count() > 0;
        dataContext.Database.Migrate();
        MigrationTest.Initialize(dataContext);
        //if (migrationEnAttente)
        //{
        //    dataContext.Database.Migrate();
        //  //  MigrationTest.Initialize(dataContext);
        //}
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
