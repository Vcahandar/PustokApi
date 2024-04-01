using Pustok.API.Extensions;
using Pustok.DataAccess.ServiceRegisterations;
using Pustok.Business.ServiceRegisterations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.DataAccessServiceRegister(builder.Configuration);
builder.Services.ServiceLayerServiceRegister();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.AddExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
