using System;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Volo.Abp.Application.Dtos;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPositionDto : ExtensibleEntityDto<Guid>
    {
        public string Name{ get; set; }
        public string Code{ get; set; }

    }
}
