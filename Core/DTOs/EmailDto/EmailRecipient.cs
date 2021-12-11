using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EmailDto
{
    public class EmailRecipient
    {
        public List<string> SoftwareDevelopers { get; set; }
        public List<string> Managements { get; set; }
        public List<string> Admins { get; set; }
        public List<string> HumanResources { get; set; }
    }
}
