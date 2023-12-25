namespace AvaPMIS.Gateway.ApiResponseWrapper
{
    public class ApiError
    {
        /// <summary>
        /// آیا فراخوانی سرویس با خطا مواجه شده است؟
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// متن خطای ایجاد شده
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// چزییات خطا
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// کد خطای مرجع
        /// </summary>
        public string ReferenceErrorCode { get; set; }

        /// <summary>
        /// مستندات خطای مرجع
        /// </summary>
        public string ReferenceDocumentLink { get; set; }

        /// <summary>
        /// خطاهای اعتبارسنجی مدل داده
        /// </summary>
        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public ApiError() { }

        public ApiError(string message)
        {
            ExceptionMessage = message;
            IsError = true;
        }

        public ApiError(string message, IEnumerable<ValidationError> validationErrors)
        {
            ExceptionMessage = message;
            ValidationErrors = validationErrors;
        }

        //public ApiError(object modelStateObj)
        //{
        //    IsError = true;
        //    if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
        //    {
        //        ExceptionMessage = "Please correct the specified validation errors and try again.";
        //        ValidationErrors = modelState.Keys
        //        .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
        //        .ToList();
        //    }
        //}
    }
}
