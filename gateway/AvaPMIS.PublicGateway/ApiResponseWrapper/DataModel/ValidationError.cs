using Newtonsoft.Json;

namespace AvaPMIS.PublicGateway.ApiResponseWrapper.DataModel
{
    public class ValidationError
    {
        /// <summary>
        ///  نام فیلد
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        /// <summary>
        /// متن خطا
        /// </summary>
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
