using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        public int StatusCode {get;set;}
        public string Message{get;set;}

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request! Please check the parameter passed!!",
                401 => "You are not authorized!",
                404 => "Resource was not found!",
                500 => "Server error!",
                _ => null
            };
        }
    }
}