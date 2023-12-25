using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using StackExchange.Redis;
using Volo.Abp;

using Volo.Abp.Autofac;

using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using System.Text.Json;
using AvaPMIS.PublicGateway.ApiResponseWrapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Volo.Abp.AspNetCore.Serilog;
using AvaPMIS.PublicGateway.AutoWrapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;

namespace AvaPMIS.PublicGateway
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class PublicGatewayHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = false; //MsDemoConsts.IsMultiTenancyEnabled;
            });

            context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = configuration["AuthServer:Authority"];
                o.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                o.Audience = "AvaPMIS";
                o.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateAudience = false
                };
                o.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        (message, certificate, chain, sslPolicyErrors) => true
                };
            });
            context.Services.AddOcelot(context.Services.GetConfiguration());

            context.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
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

            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var hostingEnvironment = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();
            app.UseCors();
            app.UseResponseCompression();
            app.UseAbpSerilogEnrichers();

            //app.UseApiResponseMiddleware();
            //app.UseApiResponseAndExceptionWrapper();
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
            app.MapWhen((ctx) => !ctx.Request.Path.HasValue || ctx.Request.Path!=@"/" || ctx.Request.Path != @"/mydl", (app) =>
            {
                app.UseOcelot();
            });
            //app.UseOcelot().Wait();
            
        }
    }
}