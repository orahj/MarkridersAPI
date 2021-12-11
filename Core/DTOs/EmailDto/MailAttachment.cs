using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EmailDto
{
    public class MailAttachment
    {
        public byte[] Bytes { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
    }
}
