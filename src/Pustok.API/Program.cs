using Microsoft.EntityFrameworkCore;
using Pustok.Business.MappingProfiles;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Implementations;
using Pustok.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AuthorMapper).Assembly);

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();


builder.Services.AddScoped<IAuthorService, AuthorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
