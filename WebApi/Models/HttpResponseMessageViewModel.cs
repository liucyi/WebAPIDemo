using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class HttpResponseMessageViewModel
    {
        public HttpResponseMessageViewModel()
        {
            IsSuccess = false;
            StatusCode = (int) System.Net.HttpStatusCode.NotFound;
            StatusCodeDes = "";
            Data = "";
        }
        /// <summary>
        /// true
        /// </summary>
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string StatusCodeDes { get; set; }
        public dynamic Data { get; set; }
    }
}