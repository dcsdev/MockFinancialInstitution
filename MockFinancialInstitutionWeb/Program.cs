using MockFinancialInstitutionWeb.Api_Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var env = builder.Environment;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .Build();

builder.Services.AddHttpClient<ClosingCoordinatorClient>(client =>
{
    var baseUrl = configuration["ClosingCoordiatorAPISettings:BaseUrl"];
    var apiKey = configuration["ClosingCoordiatorAPISettings:ApiSecretKey"];
   
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
