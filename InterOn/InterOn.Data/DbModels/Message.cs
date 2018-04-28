using System;
using System.ComponentModel.DataAnnotations;

namespace InterOn.Data.DbModels
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public User SenderUser { get; set; }
        public User ReceiverUser { get; set; }
        public DateTime CreateDateTime { get; set; }
        [Required]
        [StringLength(200)]
        public string Content { get; set; }  
    }
}