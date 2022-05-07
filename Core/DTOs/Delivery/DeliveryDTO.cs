using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs.Delivery
{
    public class DeliveryDTO
    {
        [Required]
        public string Email { get; set; }
        public virtual IList<DeliveryItemDTO> DeliveryItems { get; set; }
    }
    public class DeliveryReturnDTO
    {
        public int Id {get;set;}
        public string DeliveryNo { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public int? transactionId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public virtual IReadOnlyList<DeliveryItemReturnDTO> DeliveryItems { get; set; }
    }
    public class DeliveryAignmentDTO
    {
        public int Id { get; set; }
        public string DeliveryNo { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public int? transactionId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }
    public class AsigndeliveryDTO
    {
        public string AppUserId { get; set; }
        public int DeliveriesId { get; set; }
    }
    public class DeliveryDetailDTO
    {
        public string AppUserId { get; set; }
        public int DeliveriesId { get; set; }
        public string Reason { get; set; }
    }
    public class DeliveryCompletionDTO
    {
        public string AppUserId { get; set; }
        public int DeliveriesId { get; set; }
        public int? Rating { get; set; }
        public string Ratingcomment { get; set; }
        [JsonIgnore]
        public string Email { get; set; }
    }
    public class DeliverydeliveredDTO
    {
        public string AppUserId { get; set; }
        public int DeliveriesId { get; set; }
    }
    public class DeliveryDisputedDTO
    {
        public string AppUserId { get; set; }
        public int DeliveriesId { get; set; }
        public int? Rating { get; set; }
        public string Ratingcomment { get; set; }
        public string DisputdeReason { get; set; }
    }
    public class RdiersalesDetailsDTO
    {
        public decimal Totalsales { get; set; }
        public decimal Monthlysales { get; set; }
        public decimal Weeklysales { get; set; }
    }
}