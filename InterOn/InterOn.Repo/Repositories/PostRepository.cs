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
        public async Task<bool> IfEventExist(int eventId) => await _context.Events.AnyAsync(a => a.Id == eventId);
        
        public async Task<bool> IfUserAddPost(int postId, int userId) =>
            await _context.Posts.AnyAsync(p => p.UserId == userId & p.Id == postId);

        public async Task<Post> GetPostGroupAsync(int postId)
        {
            return await _context.Posts
                .Include(u => u.User)
                .Include(a => a.Comments)
                .ThenInclude(ac => ac.User)
                .FirstOrDefaultAsync(a => a.Id == postId);
        }
    }
}