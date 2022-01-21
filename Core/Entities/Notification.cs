using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Newtonsoft.Json;
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Notification :BaseEntity
    {
        public Notification() { }
        public Notification(string appUserId, AppUser appUser, NotificationType type, bool read, string dataJson)
        {
            AppUserId = appUserId;
            AppUser = appUser;
            Type = type;
            Read = read;
            DataJson = dataJson;
        }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public NotificationType Type { get; set; }
        public bool Read { get; set; }
        public string DataJson
        {
            get { return JsonConvert.SerializeObject(Data); }
            set { Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(value); }
        }
        [NotMapped]
        public Dictionary<string, string> Data { get; set; }
    }
}