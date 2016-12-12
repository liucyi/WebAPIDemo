using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class HttpResponseMessageViewModel
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string StatusCodeDes { get; set; }
        public object Data { get; set; }
    }
}