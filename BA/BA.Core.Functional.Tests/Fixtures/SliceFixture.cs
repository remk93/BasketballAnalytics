using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BA.Domain;
using BA.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Xunit;

namespace BA.Core.Functional.Tests.Fixtures;

[CollectionDefinition(nameof(SliceFixture))]
public class SliceFixtureCollection : ICollectionFixture<SliceFixture> { }

public class SliceFixture : IAsyncLifetime
{
    private readonly Checkpoint _checkpoint;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly WebApplicationFactory<Program> _factory;

    public SliceFixture()
    {
        _factory = new ContosoTestApplicationFactory();

        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint()
        {
            TablesToIgnore = new Table[] { "VersionInfo" }
        };
    }

    class ContosoTestApplicationFactory 
        : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configBuilder) =>
            {
                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ConnectionStrings:MSSQL", _connectionString}
                });
            });
        }

        private readonly string _connectionString = "Server=.;Database=BA.Tests;Trusted_Connection=True;TrustServerCertificate=true;";
    }

    public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EntitiesContext>();

        try
        {
            await action(scope.ServiceProvider);
        }
        catch (Exception)
        {
            dbContext.RollbackTransaction(); 
            throw;
        }
    }

    public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = _scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<EntitiesContext>();

        try
        {
            var result = await action(scope.ServiceProvider);

            return result;
        }
        catch (Exception ex)
        {
            dbContext.RollbackTransaction();
            throw;
        }
    }

    public Task ExecuteDbContextAsync(Func<EntitiesContext, Task> action) 
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<EntitiesContext>()));

    public Task ExecuteDbContextAsync(Func<EntitiesContext, ValueTask> action) 
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<EntitiesContext>()).AsTask());

    public Task ExecuteDbContextAsync(Func<EntitiesContext, IMediator, Task> action) 
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<EntitiesContext>(), sp.GetRequiredService<IMediator>()));

    public Task<T> ExecuteDbContextAsync<T>(Func<EntitiesContext, Task<T>> action) 
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<EntitiesContext>()));

    public Task<T> ExecuteDbContextAsync<T>(Func<EntitiesContext, ValueTask<T>> action) 
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<EntitiesContext>()).AsTask());

    public Task<T> ExecuteDbContextAsync<T>(Func<EntitiesContext, IMediator, Task<T>> action) 
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<EntitiesContext>(), sp.GetRequiredService<IMediator>()));

    public Task InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }

    public Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
    {
        return ExecuteDbContextAsync(db =>
        {
            db.Set<TEntity>().Add(entity);

            return db.SaveChangesAsync();
        });
    }

    public Task InsertAsync<TEntity, TEntity2>(TEntity entity, TEntity2 entity2) 
        where TEntity : class
        where TEntity2 : class
    {
        return ExecuteDbContextAsync(db =>
        {
            db.Set<TEntity>().Add(entity);
            db.Set<TEntity2>().Add(entity2);

            return db.SaveChangesAsync();
        });
    }

    public Task InsertAsync<TEntity, TEntity2, TEntity3>(TEntity entity, TEntity2 entity2, TEntity3 entity3) 
        where TEntity : class
        where TEntity2 : class
        where TEntity3 : class
    {
        return ExecuteDbContextAsync(db =>
        {
            db.Set<TEntity>().Add(entity);
            db.Set<TEntity2>().Add(entity2);
            db.Set<TEntity3>().Add(entity3);

            return db.SaveChangesAsync();
        });
    }

    public Task InsertAsync<TEntity, TEntity2, TEntity3, TEntity4>(TEntity entity, TEntity2 entity2, TEntity3 entity3, TEntity4 entity4) 
        where TEntity : class
        where TEntity2 : class
        where TEntity3 : class
        where TEntity4 : class
    {
        return ExecuteDbContextAsync(db =>
        {
            db.Set<TEntity>().Add(entity);
            db.Set<TEntity2>().Add(entity2);
            db.Set<TEntity3>().Add(entity3);
            db.Set<TEntity4>().Add(entity4);

            return db.SaveChangesAsync();
        });
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        return await ExecuteScopeAsync(async sp =>
        {
            var mediator = sp.GetRequiredService<IMediator>();

            return await mediator.Send(request);
        });
    }

    public async Task InitializeAsync()
        => await _checkpoint.Reset(_configuration.GetConnectionString("MSSQL"));

    public Task DisposeAsync()
    {
        _factory?.Dispose();
        return Task.CompletedTask;
    }
}