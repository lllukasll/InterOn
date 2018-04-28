using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;

namespace InterOn.Repo.Repositories
{
    public class UserMessageRepository : Repository<Message>, IUserMessageRepository
    {
        public UserMessageRepository(DataContext context) : base(context)
        {
        }
    }
}