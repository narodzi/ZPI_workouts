using CatNamespace;
using Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("InMemoryDb")
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.SeedData();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200", "http://localhost:3000")
    .WithMethods("GET", "POST", "PUT", "DELETE")
    .WithHeaders("content-type");
});


app.MapGet("/cats/{id}", async (int id, AppDbContext context) =>
{
    try
    {
        var cat = await context.Cats.FindAsync(id);
        if (cat != null)
        {
            return Results.Ok(cat);
        }
        else
        {
            return Results.NotFound();
        }
    }
    catch
    {
        return Results.Problem(
            title: "Błąd",
            detail: "Wystąpił błąd podczas realizacji tego żądania"
        );
    }
});

app.MapGet("cats", async (AppDbContext context) =>
{
    try
    {
        var cats = await context.Cats.ToListAsync();
        return Results.Ok(cats.Select(cat => (CatDTO)cat));
    }
    catch
    {
        return Results.Problem(
        title: "Błąd",
        detail: "Wystąpił błąd podczas realizacji tego żądania"
        );
    }
});

app.MapPost("cats", async (CatDTO catDTO, AppDbContext context) => {
    try {
        context.Cats.Add(catDTO);
        await context.SaveChangesAsync();
        return Results.Ok("Utworzono");
    }
    catch {
        return Results.Problem(
        title: "Błąd",
        detail: "Wystąpił błąd podczas realizacji tego żądania"
        );
    }
});

app.MapPut("cats", async (CatDTO catDTO, AppDbContext context) => {
    try {
        var cat = context.Cats.FirstOrDefault(cat => cat.Id == catDTO.Id);
        if(cat != null) {
            cat.Name = catDTO.Name;
            cat.Race = catDTO.Race;
            cat.Age = catDTO.Age;
            cat.Color = catDTO.Color;
            await context.SaveChangesAsync();
            return Results.Ok("Zaktualizowano");
        }
        else {
            return Results.NotFound();
        }

    }
    catch {
        return Results.Problem(
            title: "Błąd",
            detail: "Wystąpił błąd podczas realizacji tego żądania"
        );
    }
});

app.MapDelete("cats/{id}", async (int id, AppDbContext context) => {
    try {
        var cat = context.Cats.FirstOrDefault(cat => cat.Id == id);
        if(cat != null) {
            context.Cats.Remove(cat);
            await context.SaveChangesAsync();
            return Results.Ok("Usunięto");
        }
        else {
            return Results.NotFound();
        }
    }
    catch {
                return Results.Problem(
            title: "Błąd",
            detail: "Wystąpił błąd podczas realizacji tego żądania"
        );
    }
});

app.Run();


public class AppDbContext : DbContext
{
    public DbSet<Cat> Cats => Set<Cat>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

static class SeedDataExtensions
{
    public static void SeedData(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetService<AppDbContext>()?.SeedData();
    }

    private static void SeedData(this AppDbContext context)
    {
        context.Database.EnsureCreated();
        var hasData = context.Cats.Any();
        if (!hasData)
        {
            context.Cats.AddRange(
                new Cat(0, "Mimejzi", "Mieszaniec", 1, "Czarno-biały"),
                new Cat(0, "Puszek", "syjamski", 3, "Beżowy"),
                new Cat(0, "Łatka", "norweski leśny", 5, "Płowy")
            );
            context.SaveChanges();
        }
    }
}