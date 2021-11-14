using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum UserTypes
    {
        [EnumMember(Value = "Super Admin")]
         SuperAdmin = 1,
         [EnumMember(Value = "Admin User")]
        AdminUser =2,
        [EnumMember(Value = "Rider")]
        Riders=3,
        [EnumMember(Value = "User")]
        Users = 4
    }
}
