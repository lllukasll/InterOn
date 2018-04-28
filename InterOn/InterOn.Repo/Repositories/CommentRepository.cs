using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IfGroupExist(int groupId) => await _context.Groups.AnyAsync(g => g.Id == groupId);

        public async Task<bool> IfPostExist(int postId) => await _context.Posts.AnyAsync(p => p.Id == postId);

        public async Task<IEnumerable<Comment>> GetCommentsForPostGroup(int postId) =>
            await _context.Comments.Include(u=>u.User).Where(a => a.PostId == postId).OrderBy(a => a.CreateDateTime).ToListAsync();

        public async Task<Comment> GetCommentsForUser(int commentId, int userId) =>
            await _context.Comments.Include(u => u.User).Where(a => a.UserId == userId & a.Id == commentId)
                .SingleOrDefaultAsync(a => a.Id == commentId);

        public async Task<bool> IfUserAddComment(int commentId, int userId) =>
            await _context.Comments.AnyAsync(c => c.UserId == userId & c.Id==commentId);
    }
}