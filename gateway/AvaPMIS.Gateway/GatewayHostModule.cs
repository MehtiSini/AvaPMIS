using System.Text.Json;
using AvaPMIS.Gateway.AutoWrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Formatting = System.Xml.Formatting;
using Yarp.ReverseProxy;
using System.Net;
using System.Net.Security;
using AvaPMIS.Gateway.ApiResponseWrapper;

namespace AvaPMIS.Gateway
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class GatewayHostModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            //TODO alter it later
            var bypassSSLError = !Convert.ToBoolean(GetConfigurationOrDefault(configuration, "AuthServer:RequireHttpsMetadata", false));
       
            context.Services.AddReverseProxy().LoadFromConfig(configuration.GetSection("ReverseProxy")).ConfigureHttpClient((context, handler) =>
            {
                if (bypassSSLError)
                {
                    handler.SslOptions.RemoteCertificateValidationCallback = (sender, cert, cetChain, policyErrors) =>
                    {

                        return true;
                    };
                }
            });
            Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
           
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting =Newtonsoft.Json.Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            context.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = false; //MsDemoConsts.IsMultiTenancyEnabled;
            });
            context.Services.AddMvcCore();
            context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = configuration["AuthServer:Authority"];
                o.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                o.Audience = "AvaPMIS";
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false
                };
            });
            
            
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
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var hostingEnvironment = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();
            app.UseCors();
           
            app.UseAbpSerilogEnrichers();
            //app.UseApiResponseMiddleware();
            //app.UseApiResponseAndExceptionWrapper();
            app.UseResponseCompression();
            app.UseCorrelationId();

            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            app.MapWhen(
                ctx => ctx.Request.Path.ToString().StartsWith("/api/abp/") ||
                       ctx.Request.Path.ToString().StartsWith("/Abp/"),
                app2 =>
                {
                    app2.UseRouting();
                    app2.UseConfiguredEndpoints();
                }
            );
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("AG Welcome!");
                });
            });
            //TODO moved into Program.cs class
            //app.MapReverseProxy();
            

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

}
