using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarkriderAPI.Helpers
{
    public class FileHelper
    {
        public static string GenerateFileName(string httpPostedFileName)
        {
            var fileName = $"{Guid.NewGuid().ToString()}-{Guid.NewGuid()}{Path.GetExtension(httpPostedFileName)}";

            if (File.Exists(fileName))
                return GenerateFileName(httpPostedFileName);

            return fileName;
        }

        public static string GetTempFolderPath()
        {
            var folderName = Path.Combine("wwwroot/images/temp");
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }
        public static string GetProfileFolderPath()
        {
            var folderName = Path.Combine("images/profile");
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }
        public static string GetKYCFolderPath()
        {
            var folderName = Path.Combine("images/kyc");
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }
    }
}