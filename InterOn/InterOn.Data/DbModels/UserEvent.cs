namespace InterOn.Data.DbModels
{
    public class UserEvent
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }
    }
}