using System;
using System.Net;

namespace Commons.Models
{
    public class ResponseModel<T>
    {
        public Metadata Metadata { get; set; }
        public T? Payload { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
    }
}

