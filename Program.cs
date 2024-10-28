using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços necessários
builder.Services.AddControllersWithViews();

// Adicione o HttpClient
builder.Services.AddHttpClient();

// Adicione serviços de autorização
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure o middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adicione o uso de autorização antes de `UseEndpoints`
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
