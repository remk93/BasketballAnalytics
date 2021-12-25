using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core;
using BA.Core.Exceptions.Extensions;
using BA.Core.Options;
using BA.Domain;
using BA.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionHandlerAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper((serviceProvider, autoMapper) =>
{
    autoMapper.AddCollectionMappers();
    autoMapper.UseEntityFrameworkCoreModel<EntitiesContext>(serviceProvider);
}, Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy
    (
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
    );
});

builder.Services.AddCoreDependencies();
builder.Services.AddDomainDependencies(builder.Configuration);
builder.Services.AddMigrationsDependencies(builder.Configuration);

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorage"));

builder.Services.AddDbContextFactory<EntitiesContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"),
        x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
    ServiceLifetime.Transient);

builder.Services.AddScoped<EntitiesContext>(p =>
    p.GetRequiredService<IDbContextFactory<EntitiesContext>>()
        .CreateDbContext());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
