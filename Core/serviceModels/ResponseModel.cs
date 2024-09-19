﻿namespace QuickVisualWebWood.Core.serviceModels
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic? Data { get; set; }
    }
}
