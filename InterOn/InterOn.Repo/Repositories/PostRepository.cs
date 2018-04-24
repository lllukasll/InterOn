using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class PostRepository : Repository<Post>,IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IfGroupExist(int groupId) => await _context.Groups.AnyAsync(a => a.Id == groupId);

        public async Task<bool> IfUserAddPost(int postId, int userId) =>
            await _context.Posts.AnyAsync(p => p.UserId == userId & p.Id == postId);

        public async Task<Post> GetPostGroup(int groupId, int postId)
        {
            return await _context.Posts
                .Where(s => s.Id ==postId  && s.GroupId== groupId)
                .SingleOrDefaultAsync();
        }
    }
}