using Pustok.API.Extensions;
using Pustok.DataAccess.ServiceRegisterations;
using Pustok.Business.ServiceRegisterations;
using Pustok.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Pustok.DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.DataAccessServiceRegister(builder.Configuration);
builder.Services.ServiceLayerServiceRegister();


builder.Services.AddControllers();
builder.Services.AddIdentityService();

builder.Services.AddJwtServiceExtension(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {

            //you can configure your custom policy
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.AddExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
