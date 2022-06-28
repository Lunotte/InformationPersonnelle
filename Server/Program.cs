using InformationPersonnelle.Server.Data;
using InformationPersonnelle.Server.Entities.User;
using InformationPersonnelle.Server.Repositories;
using InformationPersonnelle.Server.Repositories.Contracts;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var ConnectionStringInformationPersonnelleDbContext = "Server=localhost;Database=test;Username=postgres;Password=postgres";

string connString = builder.Configuration.GetConnectionString(nameof(InformationPersonnelleDbContext));
builder.Services.AddDbContext<InformationPersonnelleDbContext>(options => options.UseNpgsql(connString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<InformationPersonnelleDbContext>();

// Obligatoire pour le lazy loading
builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
        //var dataContext = services.GetRequiredService<InformationPersonnelleDbContext>();
        //dataContext.Database.EnsureDeleted();
        //var migrationEnAttente = dataContext.Database.GetPendingMigrations().Count() > 0;
        //dataContext.Database.Migrate();
        //MigrationTest.Initialize(dataContext);
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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
