using System.Runtime.Serialization;
using Core.Entities;

namespace Core.Enum
{
    public enum Gender
    {
       [EnumMember(Value = "Male")]
        Male = 1 ,
      [EnumMember(Value = "Female")]
        Female = 2
    }
}
