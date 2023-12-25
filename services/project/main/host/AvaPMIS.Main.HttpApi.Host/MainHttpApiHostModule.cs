using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.Mvc;
using System.Text.Json;
using AvaPMIS.Administration.EntityFrameworkCore;
using AvaPMIS.Hosting.Shared;
using AvaPMIS.Main.EntityFrameworkCore;
using AvaPMIS.SaaS.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AvaPMIS.Gateway.AutoWrapper;

namespace AvaPMIS.Main;

[DependsOn(
    typeof(AvaPMISHostingModule),
    typeof(MainApplicationModule),
    typeof(MainEntityFrameworkCoreModule),
    typeof(MainHttpApiModule),
    typeof(AbpAspNetCoreSerilogModule),

    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(SaaSEntityFrameworkCoreModule)

)]
public class MainHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        Configure<AbpDbContextOptions>(options =>
        {
            //options.UseNpgsql();
            options.UseSqlServer(x => x.UseNetTopologySuite());
        });
        Configure<JsonOptions>(options =>
        {
           options.JsonSerializerOptions.PropertyNamingPolicy=JsonNamingPolicy.CamelCase;
        });
        context.Services.AddAutoWrapperMiddleware(configuration.GetSection("AutoResponseWrapper"));
        context.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<MainDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.Main.Domain.Shared",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.Main.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.Main.Application.Contracts",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<MainApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.Main.Application",
                            Path.DirectorySeparatorChar)));
            });
        }
        //ConfigureConventionalControllers();
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string> {
                {"Main", "Main API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Main API", Version = "v1"});
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        Configure<DataProtectionTokenProviderOptions>(options =>
        {
            //options.TokenLifespan


        });
        Configure<IdentityOptions>(options =>
        {
          
        });
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
        context.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = configuration["AuthServer:Authority"];
            o.RequireHttpsMetadata =
                Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            o.Audience = "AvaPMIS";
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false
            };


        });
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Main:";

        });

       

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Main");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Main-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                         //.WithOrigins(
                         //    configuration["App:CorsOrigins"]
                         //        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                         //        .Select(o => o.RemovePostFix("/"))
                         //        .ToArray()
                         //)
                         //.WithAbpExposedHeaders()
                         //.SetIsOriginAllowedToAllowWildcardSubdomains()
                         //.AllowAnyHeader()
                         //.AllowAnyMethod()
                         //.AllowCredentials();
                         .SetIsOriginAllowed((host) => true)
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials();
            });
        });
    }
    private void ConfigureConventionalControllers()
    {
       
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers
                .Create(typeof(MainApplicationModule).Assembly, opts =>
                {
                    

                });
        });


    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        IdentityModelEventSource.ShowPII = true;
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        app.UseCors();
        app.UseResponseCompression();
        app.UseApiResponseAndExceptionWrapper();
        //app.UseApiResponseMiddleware();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        //if (MultiTenancyConsts.IsEnabled)
        //{
            app.UseMultiTenancy();
        //}

        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            options.OAuthScopes("Main");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}