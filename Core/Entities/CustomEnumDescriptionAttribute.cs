using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomEnumDescriptionAttribute : Attribute
    {
         public CustomEnumDescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

        public string GetDescription()
        {
            return Description;
        }
    }
}