using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http;
using Volo.Abp.Imaging;

namespace AvaPMIS.Main.Utility
{
    public class Base64ImageService: IBase64ImageService,ISingletonDependency
    {

        private readonly IImageCompressor _imageCompressor;

        public Base64ImageService(IImageCompressor imageCompressor)
        {
            _imageCompressor = imageCompressor;
        }

        public async Task<byte[]> Compress(string base64Image)
        {
            var base64Obj = FromBase64Image(base64Image);
            var binImage = Convert.FromBase64String(base64Obj.Data);
            var compressedImage = await _imageCompressor.CompressAsync(binImage, base64Obj.MimeType);
            return compressedImage.Result;
        }
        public static IBase64ImageService.Base64Image FromBase64Image([NotNull] string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image))
                throw new ArgumentNullException(nameof(base64Image));
            var res = new IBase64ImageService.Base64Image();
            var data = base64Image;
            string mime = null;
            var pattern = @"data:(?<type>.+?);base64,(?<data>.+)";
            var match = Regex.Match(base64Image, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                data = match.Groups["data"].Value;
                mime = match.Groups["type"].Value;
            }
            res.Data = data;
            res.MimeType = mime;
            return res;
        }
    }
}
