using System;

namespace InterOn.Data.ModelsDto.User
{
    public class FriendDto
    {
        public int Id { get; set; }
        public int UserAId { get; set; }
        public int UserBId { get; set; }
        public UserDto UserA { get; set; }
        public UserDto UserB { get; set; }
        public DateTime Established { get; set; }
        public bool Confirmed { get; set; }
    }
}