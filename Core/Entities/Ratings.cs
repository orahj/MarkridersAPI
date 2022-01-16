using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ratings :BaseEntity
    {
        public Ratings()
        {
        }

        public Ratings(int ratingNumber, string comment, string appUserId)
        {
            RatingNumber = ratingNumber;
            Comment = comment;
            AppUserId = appUserId;
        }

        public int RatingNumber { get; set; }
        public string Comment { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
