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
using AvaPMIS.Hosting.Shared;
using AvaPMIS.IdentityService.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.Mvc;
using Nozhan.Abp.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AvaPMIS.Administration.EntityFrameworkCore;
using AvaPMIS.SaaS.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;

using Volo.Abp.AspNetCore.Serilog;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography.X509Certificates;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using AvaPMIS.Gateway.AutoWrapper;

namespace AvaPMIS.IdentityService;

[DependsOn(
    typeof(AvaPMISHostingModule),
    typeof(IdentityServiceApplicationModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceHttpApiModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(NozhanUtilitiesModule)
)]
public class IdentityServiceHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        PreConfigure<Microsoft.AspNetCore.Identity.IdentityBuilder>(builder =>
        {
            builder.AddDefaultTokenProviders();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        
        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        context.Services.AddAutoWrapperMiddleware(configuration.GetSection("AutoResponseWrapper"));
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.IdentityService.Domain.Shared",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.IdentityService.Domain",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.IdentityService.Application.Contracts",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}AvaPMIS.IdentityService.Application",
                            Path.DirectorySeparatorChar)));
            });
        }

        //ConfigureConventionalControllers();
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string> {
                { "IdentityService", "IdentityService API" }
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityService API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        context.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
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
                Convert.ToBoolean(GetConfigurationOrDefault(configuration, "AuthServer:RequireHttpsMetadata", false));
            o.Audience = "AvaPMIS";
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false
            };
          

        });
     
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "IdentityService:";

        });



        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("IdentityService");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "IdentityService-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                //todo set it later
                builder
                    //.WithOrigins(
                    //    configuration["App:CorsOrigins"]
                    //        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    //        .Select(o => o.RemovePostFix("/"))
                    //        .ToArray()
                    //)
                    //
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
                .Create(typeof(IdentityServiceApplicationModule).Assembly, opts =>
                {

                    //opts.RootPath = "api/identity";
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
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            
            ////TODO remove it or add leptonx theme
            //app.UseDeveloperExceptionPage();
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
            options.OAuthScopes("IdentityService");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
    private X509Certificate2 GetSigningCertificate(IWebHostEnvironment hostingEnv)
    {
        const string fileName = "AvaPMIS.pfx";
        const string passPhrase = "@^%ULTRA)(";
        var file = Path.Combine(hostingEnv.ContentRootPath, fileName);

        if (!File.Exists(file))
        {
            throw new FileNotFoundException($"Signing Certificate couldn't found: {file}");
        }

        return new X509Certificate2(file, passPhrase);
    }
private string GetConfigurationOrDefault<T>(IConfiguration configuration, string configurationName, T defaultValue = default)
{
    if (configuration[configurationName] != null)
        return configuration[configurationName];
    else
    {
        return defaultValue == null ? null : defaultValue.ToString();
    }
}
}
public class SimapleTokenValidator : JwtSecurityTokenHandler
{

    public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters,
        out SecurityToken validatedToken)
    {
        validatedToken = null;
        var rst = base.ValidateToken(token, validationParameters, out validatedToken);
        //var info = validatedToken;
        var claims = rst.Claims;

        return rst;
    }
    
}