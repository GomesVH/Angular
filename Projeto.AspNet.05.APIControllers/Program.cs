using Projeto.AspNet._05.APIControllers.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// adicionar a referencia do repositorio de dados que comp�em a aplica��o. � importante est� referencia pois o repositorio assume o papel de um "servi�o" de armazenamento dos dados
builder.Services.AddSingleton<IRepository, Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
