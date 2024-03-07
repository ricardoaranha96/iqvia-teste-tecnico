using Microsoft.EntityFrameworkCore;
using TesteTecnico.Data;
using Microsoft.AspNetCore.Identity;
using TesteTecnico.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<TesteTecnicoUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<EcommerceContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Logging.AddFile("Logs/IQVIA-{Date}.txt");

var app = builder.Build();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<EcommerceContext>();
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}
catch (Exception e)
{
    app.Logger.LogError(e, "Erro ao criar banco de dados");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
