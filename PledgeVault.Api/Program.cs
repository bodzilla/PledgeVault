using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PledgeVault.Api.Conventions;
using PledgeVault.Api.Middleware;
using PledgeVault.Core.Contracts;
using PledgeVault.Persistence;
using PledgeVault.Services;

namespace PledgeVault.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options => { options.Conventions.Add(new PluralizeControllerModelConvention()); });
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
        builder.Services.AddDbContextPool<PledgeVaultContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ICountryService, CountryService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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