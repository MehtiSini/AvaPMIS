using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AvaPMIS.SaaS.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}