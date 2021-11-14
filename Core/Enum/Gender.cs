using System.Runtime.Serialization;
using Core.Entities;

namespace Core.Enum
{
    public enum Gender
    {
       [EnumMember(Value = "Male")]
        Male,
      [EnumMember(Value = "Female")]
        Female
    }
}
