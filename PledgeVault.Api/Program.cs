using FluentValidation;
using FluentValidation.AspNetCore;
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
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Handlers;
using PledgeVault.Services.Queries;
using System;

namespace PledgeVault.Api;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllers(options => { options.Conventions.Add(new PluralizeControllerModelConvention()); })
            .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        builder.Services.AddValidatorsFromAssemblies(assemblies);
        builder.Services.AddAutoMapper(assemblies);
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assemblies));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddTransient<IRequestHandler<GetAllQuery<CountryResponse>, PageResponse<CountryResponse>>, GetAllQueryHandler<Country, CountryResponse>>();
        builder.Services.AddTransient<IRequestHandler<GetAllQuery<PartyResponse>, PageResponse<PartyResponse>>, GetAllQueryHandler<Party, PartyResponse>>();
        builder.Services.AddTransient<IRequestHandler<AddCommand<AddCountryRequest, CountryResponse>, CountryResponse>, AddCommandHandler<Country, AddCountryRequest, CountryResponse>>();
        builder.Services.AddTransient<IRequestHandler<AddCommand<AddPartyRequest, PartyResponse>, PartyResponse>, AddCommandHandler<Party, AddPartyRequest, PartyResponse>>();

        builder.Services.AddDbContextPool<PledgeVaultContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IPartyService, PartyService>();
        builder.Services.AddScoped<IPledgeService, PledgeService>();
        builder.Services.AddScoped<IPoliticianService, PoliticianService>();
        builder.Services.AddScoped<IPositionService, PositionService>();
        builder.Services.AddScoped<IResourceService, ResourceService>();

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

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}