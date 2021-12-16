using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Admin.Endpoints;
using BA.Core;
using BA.Domain;
using BA.Migrations;
using FluentMigrator.Runner;
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
builder.Services.AddSingleton<IPersonEndpoints, PersonEndpoints>();

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

app.UseStaticFiles();

app.UseRouting();

app.UseCors();


using (var scope = app.Services.CreateScope())
{
    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    migrator!.MigrateUp();
}

#region Team Endpoints

app.MapPost("/Team/Create", async (BA.Core.Commands.Team.CreateCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Create(command);
}).WithTags("Team");

app.MapPost("/Team/Get", async (BA.Core.Commands.Team.GetCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Get(command);
}).WithTags("Team");

app.MapPost("/Team/Update", async (BA.Core.Commands.Team.UpdateCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Update(command);
}).WithTags("Team");

app.MapPost("/Team/Delete", async (BA.Core.Commands.Team.DeleteCommand command, [FromServices] ITeamEndpoints teamEndpoints) =>
{
    return await teamEndpoints.Delete(command);
}).WithTags("Team");

#endregion

#region Person Endpoints

app.MapPost("/Person/Create", async (BA.Core.Commands.Person.CreateCommand command, [FromServices] IPersonEndpoints personEndpoints) =>
{
    return await personEndpoints.Create(command);
}).WithTags("Person");

app.MapPost("/Person/Get", async (BA.Core.Commands.Person.GetCommand command, [FromServices] IPersonEndpoints personEndpoints) =>
{
    return await personEndpoints.Get(command);
}).WithTags("Person");

app.MapPost("/Person/Update", async (BA.Core.Commands.Person.UpdateCommand command, [FromServices] IPersonEndpoints personEndpoints) =>
{
    return await personEndpoints.Update(command);
}).WithTags("Person");

app.MapPost("/Person/Delete", async (BA.Core.Commands.Person.DeleteCommand command, [FromServices] IPersonEndpoints personEndpoints) =>
{
    return await personEndpoints.Delete(command);
}).WithTags("Person");

#endregion

app.Run();