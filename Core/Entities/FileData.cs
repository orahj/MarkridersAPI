using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FileData : BaseEntity
    {
        public FileData()
        {
        }

        public FileData(string name, string uRL)
        {
            Name = name;
            URL = uRL;
        }

        public string Name{get;set;}
        public string URL{get;set;}
    }
}