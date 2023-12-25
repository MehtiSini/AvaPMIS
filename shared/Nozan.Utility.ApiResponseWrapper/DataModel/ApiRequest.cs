using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AvaPMIS.Gateway.ApiResponseWrapper
{
    public enum RequestType
    {
        PatientRelevant,
        NotPatientRelevant,
        Both
    }

    /// <summary>
    /// کلاس ورودی سرویس
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class ApiRequest<T> where T : class, new()
    {
        public ApiRequest()
        {

        }
        /// <summary>
        /// آیا سرویس در حالت تست می باشد؟
        /// </summary>
        [JsonIgnore]
        public bool TestMode { get; set; } = false;

        /// <summary>
        /// آیا سرویس شخص محور می باشد؟
        /// </summary>
        [JsonIgnore]
        public RequestType RequestType { get; set; } = RequestType.PatientRelevant;

        /// <summary>
        /// ورودی سرویس
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public T Data { get; set; } = new T();


    }
}