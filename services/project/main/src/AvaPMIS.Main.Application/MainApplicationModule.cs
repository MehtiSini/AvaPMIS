using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FluentValidation;
using Volo.Abp.Identity;
using Volo.Abp.Imaging;
using Volo.Abp.Modularity;

namespace AvaPMIS.Main;

[DependsOn(
    typeof(MainDomainModule),
    typeof(MainApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpFluentValidationModule),
    typeof(AbpImagingAbstractionsModule),
    typeof(AbpImagingImageSharpModule)
)]


public class MainApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.AddAutoMapperObjectMapper<MainApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MainApplicationModule>(true);
        });
        if (configuration.GetSection("ImageSharp").Exists())
        {
            Configure<ImageSharpCompressOptions>(options => configuration.GetSection("ImageSharp").Bind(options));
        }
        else
        {
            Configure<ImageSharpCompressOptions>(options => 
            {
                options.JpegEncoder = new JpegEncoder
                {
                    Quality = 60
                };
                options.PngEncoder = new PngEncoder
                {
                    CompressionLevel = PngCompressionLevel.BestCompression
                };

            });
        }
       
    }
}