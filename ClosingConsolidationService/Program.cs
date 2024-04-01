using Application;
using Microsoft.AspNetCore.Authentication;
using MockFinancialInstitutionWeb.Api_Clients;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Services.AddAuthentication("ApiKey")
        .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthHandler>("ApiKey", options => { });

var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .Build();

builder.Services.AddHttpClient<CreditServiceClient>(client =>
{
    var baseUrl = configuration["APISettings:CreditService:BaseUrl"];
    var apiKey = configuration["APISettings:CreditService:ApiClientKey"];

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
});

builder.Services.AddHttpClient<AppraisalServiceClient>(client =>
{
    var baseUrl = configuration["APISettings:AppraisalService:BaseUrl"];
    var apiKey = configuration["APISettings:AppraisalService:ApiClientKey"];

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
