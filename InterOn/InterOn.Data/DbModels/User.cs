using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterOn.Data.DbModels
{
    public class User : BaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<UserGroup> Groups { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<UserEvent> Events { get; set; }
        public User()
        {
            Events=new Collection<UserEvent>();
            Posts = new Collection<Post>();
            Groups = new Collection<UserGroup>();           
        }
    }
}
