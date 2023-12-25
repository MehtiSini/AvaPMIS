using AutoMapper;
using Volo.Abp.Threading;

namespace AvaPMIS.Main.Utility
{
    public class SetBase64Image: IMappingAction<string, byte[]>
    {
        private readonly IBase64ImageService _base64ImageService;

        public SetBase64Image(IBase64ImageService base64ImageService)
        {
            _base64ImageService = base64ImageService;
        }

        public void Process(string source, byte[] destination, ResolutionContext context)
        {
            var res = AsyncHelper.RunSync(() => _base64ImageService.Compress(source));
            destination = res;
        }
    }
}