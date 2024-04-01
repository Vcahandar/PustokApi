using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.Exceptions;
using Pustok.Business.Exceptions.AuthorExceptions;
using Pustok.Business.MappingProfiles;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Services.Interfaces;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Implementations;
using Pustok.DataAccess.Repositories.Interfaces;
using System.Net;

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

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Unexpected error occured";

        if(feature.Error is IBaseException)
        {
            var exception = (IBaseException)feature.Error;
            statusCode = exception.StatusCode;
            message = exception.ErrorMessage;
        }


        var response = new ResponseDto(statusCode,message);


        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(response);
        await context.Response.CompleteAsync();

    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();
