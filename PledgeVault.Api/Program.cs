using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PledgeVault.Api.Conventions;
using PledgeVault.Api.Middleware;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models.Validators;
using PledgeVault.Persistence;
using PledgeVault.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using FluentValidation;
using FluentValidation.AspNetCore;
using PledgeVault.Core.Dtos.MappingProfiles;

namespace PledgeVault.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options => { options.Conventions.Add(new PluralizeControllerModelConvention()); });
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        builder.Services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter()); //TODO: investigate enum serialization in API requests.
        });

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<AddCountryRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AddPartyRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AddPledgeRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AddPoliticianRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AddPositionRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<AddResourceRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateCountryRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdatePartyRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdatePledgeRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdatePoliticianRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdatePositionRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateResourceRequestValidator>();

        builder.Services.AddAutoMapper(
            typeof(AddCountryRequestProfile), typeof(UpdateCountryRequestProfile),
            typeof(AddPartyRequestProfile), typeof(UpdatePartyRequestProfile),
            typeof(AddPledgeRequestProfile), typeof(UpdatePledgeRequestProfile),
            typeof(AddPoliticianRequestProfile), typeof(UpdatePoliticianRequestProfile),
            typeof(AddPositionRequestProfile), typeof(UpdatePositionRequestProfile),
            typeof(AddResourceRequestProfile), typeof(UpdateResourceRequestProfile));

        builder.Services.AddDbContextPool<PledgeVaultContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddScoped<IPartyService, PartyService>();
        builder.Services.AddScoped<IPledgeService, PledgeService>();
        builder.Services.AddScoped<IPoliticianService, PoliticianService>();
        builder.Services.AddScoped<IPositionService, PositionService>();
        builder.Services.AddScoped<IResourceService, ResourceService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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