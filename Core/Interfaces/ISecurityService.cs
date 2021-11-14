using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.Interfaces
{
    public interface ISecurityService
    {
         string GetGuid();
        string GetHashWithBase64Regular(string text, HashType hashType__1);
        string GetUniqueKeyRNG(int KeyLength, NumberType Type = NumberType.ALPHANUMERIC);
        string GetHashMini(string toHash, HashType SHA512);
        string GenerateSHA512String(string inputString);
        string GetCode(string prefix);
    }
}