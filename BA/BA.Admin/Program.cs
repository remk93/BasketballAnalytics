using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Admin.Endpoints;
using BA.Core;
using BA.Core.Commands.Team;
using BA.Core.Exceptions.Extensions;
using BA.Domain;
using BA.Migrations;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddSingleton<ITeamEndpoints, TeamEndpoints>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors();


using (var scope = app.Services.CreateScope())
{
    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    migrator!.MigrateUp();
}

app.MapPost("/Team/Create", async (CreateCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Create(command);
}).WithTags("Team");

app.MapPost("/Team/Get", async (GetCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Get(command);
}).WithTags("Team");

app.MapPost("/Team/Update", async (UpdateCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Update(command);
}).WithTags("Team");

app.MapPost("/Team/Delete", async (DeleteCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Delete(command);
}).WithTags("Team");


app.Run();