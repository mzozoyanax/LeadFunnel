using LeadFunnel.Domain;
using LeadFunnel.Interface.Repositories;
using LeadFunnel.Interface.Services;
using LeadFunnel.Repository;
using LeadFunnel.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Sql Dependency Injection
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

//My dependencies injection.
builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
builder.Services.AddScoped<ITwilioRepository, TwilioRepository>();
builder.Services.AddScoped<ITwilioService, TwilioService>();

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
