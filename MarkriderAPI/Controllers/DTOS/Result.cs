using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Controllers.DTOS
{
    public class Result
    {
        public bool IsSuccessful;
        public string Message;
        public object ReturnedObject;
        public string ReturnedCode;
    }
}