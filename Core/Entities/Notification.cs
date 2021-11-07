using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class Notification :BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public NotificationType Type { get; set; }
        public bool Read { get; set; }
        public DateTime DateCreated { get; set; }
        public string DataJson
        {
            get { return JsonConvert.SerializeObject(Data); }
            set { Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(value); }
        }
        [NotMapped]
        public Dictionary<string, string> Data { get; set; }
    }
}