using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaPMIS.Main.Utility
{
    public interface IBase64ImageService
    {
        public class Base64Image
        {
            public string Data { get; set; }
            public string MimeType { get; set; }
        }

        Task<byte[]> Compress(string base64Image);
    }
}
