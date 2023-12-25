﻿namespace AvaPMIS.PublicGateway.AutoWrapper.Models
{
    public class ApiResultResponse<T> where T : class
    {
        public bool IsError { get; set; } = false;
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
