using Microsoft.IdentityModel.Tokens;
using Supabase;
using System.Security.Claims;
using System.Text;
using TechNewsOficialBackend.Api.Contracts;
using TechNewsOficialBackend.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    var supabaseAudience = builder.Configuration["AUTHENTICATION_AUDIENCE"] ?? builder.Configuration["Authentication:ValidAudience"];
    var supabaseIssuer = builder.Configuration["AUTHENTICATION_ISSUER"] ?? builder.Configuration["Authentication:ValidIssuer"];
    var jwtSecret = builder.Configuration["AUTHENTICATION_JWT_SECRET"] ?? builder.Configuration["Authentication:JwtSecret"];
    var bytes = Encoding.UTF8.GetBytes(jwtSecret);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(bytes),
        ValidAudience = supabaseAudience,
        ValidIssuer = supabaseIssuer,
    };
});

builder.Services.AddScoped<Client>(_ =>
{
    var supabaseUrl = builder.Configuration["SUPABASE_URL"] ?? builder.Configuration["Supabase:Url"];
    var supabaseKey = builder.Configuration["SUPABASE_KEY"] ?? builder.Configuration["Supabase:Key"];

    var supaOptions = new SupabaseOptions
    {
        AutoConnectRealtime = true,
        AutoRefreshToken = true
    };

    return new Client(supabaseUrl, supabaseKey, supaOptions);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/user", (ClaimsPrincipal principal) =>
{
    var claims = principal.Claims.ToDictionary(c => c.Type, c => c.Value);
    return Results.Ok(claims);
})
.RequireAuthorization();

app.MapGet("/ping", () =>
{
    var ping = new Ping
    {
        Date = DateTime.UtcNow
    };

    return Results.Ok(ping);
});

app.MapGet("/newsletter", async (Client client) =>
{
    var response = await client
         .From<Newsletter>()
         .Get();

    var newsletters = response.Models.Select(newsletter => new NewsletterResponse
    {
        Id = newsletter.Id,
        Name = newsletter.Name,
        Description = newsletter.Description,
        CreatedAt = newsletter.CreatedAt
    });

    return Results.Ok(newsletters);
});

app.MapGet("/newsletter/{id}", async (int id, Client client) =>
{
    var response = await client
        .From<Newsletter>()
        .Where(n => n.Id == id)
        .Get();

    var newsletter = response.Models.FirstOrDefault();

    if (newsletter is null)
    {
        return Results.NotFound();
    }

    var newsletterResponse = new NewsletterResponse
    {
        Id = newsletter.Id,
        Name = newsletter.Name,
        Description = newsletter.Description,
        CreatedAt = newsletter.CreatedAt
    };

    return Results.Ok(newsletterResponse);
});

app.MapPost("/newsletter", async (
    NewsletterRequest request,
    Client client) =>
{
    var newsletter = new Newsletter
    {
        Name = request.Name,
        Description = request.Description,
        CreatedAt = DateTime.Now
    };

    var response = await client.From<Newsletter>().Insert(newsletter);

    var newNewsletter = response.Models.First();

    return Results.Ok(newNewsletter.Id);
})
.RequireAuthorization();

app.MapPut("/newsletter/{id}", async (int id, NewsletterRequest request, Client client) =>
{
    var newsletters = await client
        .From<Newsletter>()
        .Where(n => n.Id == id)
        .Get();

    var newsletter = newsletters.Model;

    if (newsletter is null)
    {
        return Results.NotFound();
    }

    newsletter.Name = request.Name;
    newsletter.Description = request.Description;

    var response = await client
        .From<Newsletter>()
        .Where(n => n.Id == id)
        .Update(newsletter);

    return Results.Ok();
})
.RequireAuthorization();

app.MapDelete("/newsletter/{id}", async (int id, Client client) =>
{
    await client
        .From<Newsletter>()
        .Where(n => n.Id == id)
        .Delete();

    return Results.NoContent();
})
.RequireAuthorization();

app.UseAuthorization();

app.Run();