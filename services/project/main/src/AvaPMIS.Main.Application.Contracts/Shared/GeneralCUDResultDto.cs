using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Json.SystemTextJson.Modifiers;

namespace AvaPMIS.Main.Shared
{
    public class GeneralCUDResultDto
    {
        public GeneralCUDResultDto()
        {
            Result = false;
            RecordId = null;
            ExtraProperties = new List<KeyValuePair<string, string>>();
        }
        public GeneralCUDResultDto(bool result, string recordId)
        {
            Result = result;
            RecordId = recordId;
        }
        public GeneralCUDResultDto(bool result, string recordId, List<KeyValuePair<string, string>> extraProperties)
        {
            Result = result;
            RecordId = recordId;
            extraProperties = new List<KeyValuePair<string, string>>();
        }
        public bool Result { get; set; } = false;
        public string RecordId { get; set; }

        public List<KeyValuePair<string,string>> ExtraProperties { get; set; }=new List<KeyValuePair<string,string>>();
    }
}
