using System;

namespace InterOn.Data.DbModels
{
    public class Friend : BaseEntity
    {
        public int UserAId { get; set; }
        public int UserBId { get; set; }
        public User UserA { get; set; }
        public User UserB { get; set; }
        public DateTime Established { get; set; }
        public string ConversationName { get; set; }
        public bool Confirmed { get; set; }
    }
}