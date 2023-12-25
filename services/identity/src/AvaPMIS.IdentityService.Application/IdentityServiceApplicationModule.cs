using System;
using System.Net;
using System.Net.Http;
using AvaPMIS.IdentityService.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nozhan.Abp.Utilities;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace AvaPMIS.IdentityService;

[DependsOn(
    typeof(IdentityServiceDomainModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
)]
[DependsOn(typeof(AbpIdentityApplicationModule))]
[DependsOn(typeof(AbpAccountApplicationModule))]
//[DependsOn(typeof(EasyAbp.Abp.VerificationCode.AbpVerificationCodeIdentityModule))]
[DependsOn(typeof(NozhanUtilitiesModule))]
[DependsOn(typeof(AbpHttpClientIdentityModelModule))]

public class IdentityServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.AddAutoMapperObjectMapper<IdentityServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<IdentityServiceApplicationModule>(true);
        });
        //in order to avoid ssl error in http client we bypass ssl validation
        var bypassSSLError = !Convert.ToBoolean(GetConfigurationOrDefault(configuration, "AuthServer:RequireHttpsMetadata", false));
        if (bypassSSLError)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            context.Services.AddHttpClient(PhoneNumberLoginConsts.IdentityServerHttpClientName)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler {

                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        }
                });
        }


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