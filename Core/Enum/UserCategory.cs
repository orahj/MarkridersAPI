using System.Runtime.Serialization;

namespace Core.Enum
{
    public enum UserCategory
    {
        [EnumMember(Value = "Individual")]
         Individual = 1,
         [EnumMember(Value = "SME")]
        SME =2,
        [EnumMember(Value = "Company")]
        Company=3,
        [EnumMember(Value = "Rider")]
        Rider = 4
    }
}
