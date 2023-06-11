using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PledgeVault.Api.Conventions;
using PledgeVault.Api.Middleware;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Persistence;
using PledgeVault.Services;
using PledgeVault.Services.Validators;
using System;

namespace PledgeVault.Api;

internal sealed class Program
{
    private const string CorsPolicy = "CorsPolicy";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);

        // AspNetCore.HealthChecks.UI.InMemory.Storage nuget required for this to work.
        builder.Services.AddHealthChecksUI(options => { options.AddHealthCheckEndpoint("Healthcheck API", "/_health"); })
            .AddInMemoryStorage();

        builder.Services.AddCors(options => options.AddPolicy(CorsPolicy, policyBuilder =>
        { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build(); }));

        builder.Services
            .AddControllers(options => { options.Conventions.Add(new PluralizeControllerModelConvention()); })
            .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestObjectValidationBehavior<,>));

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        builder.Services.AddValidatorsFromAssemblies(assemblies);
        builder.Services.AddAutoMapper(assemblies);
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assemblies));

        builder.Services.AddDbContextPool<PledgeVaultContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ICountryEntityValidator, CountryEntityValidator>();
        builder.Services.AddScoped<IPartyEntityValidator, PartyEntityValidator>();
        builder.Services.AddScoped<IPledgeEntityValidator, PledgeEntityValidator>();
        builder.Services.AddScoped<IPoliticianEntityValidator, PoliticianEntityValidator>();
        builder.Services.AddScoped<IResourceEntityValidator, ResourceEntityValidator>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGenNewtonsoftSupport();

        using var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(CorsPolicy);
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/_health", new() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
        app.MapHealthChecksUI(options => options.UIPath = "/_dashboard");
        app.Run();
    }
}