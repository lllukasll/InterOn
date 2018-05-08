using System;

namespace InterOn.Data.ModelsDto.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public UserDto SenderUser { get; set; }
        public UserDto ReceiverUser { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Content { get; set; }
    }
}