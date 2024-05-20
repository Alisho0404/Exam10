using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class Response<T>
    {
        private string data;

        public T Data { get; set; }
        public int? StatusCode { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();


        public Response(T data)
        {
            StatusCode = 200;
            Data = data;
        }

        public Response(T data, List<string> errors, HttpStatusCode statusCode)
        {
            StatusCode = (int)HttpStatusCode.OK;
            Data = data;
            Errors = errors;
        }
        public Response(HttpStatusCode statusCode, List<string> errors)
        {
            StatusCode = (int)statusCode;
            Errors = errors;
        }
        public Response(HttpStatusCode statusCode, string error)
        {
            StatusCode = (int)statusCode;
            Errors.Add(error);
        }

        public Response(string data)
        {
            this.data = data;
        }
    }
}
