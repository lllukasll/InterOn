﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Comments;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> IfGroupExistAsync(int groupId) => await _repository.IfGroupExist(groupId);
        public async Task<bool> IfCommentExistAsync(int commentId) => await _repository.Exist(a => a.Id == commentId);
        public async Task<bool> IfPostExistAsync(int postId) => await _repository.IfPostExist(postId);

        public async Task<bool> IfUserAddCommentAsync(int commentId, int userId) =>
            await _repository.IfUserAddComment(commentId, userId);

        public async Task UpdateCommentForGroupAsync(int commentId, UpdateGroupPostCommentDto commentsDto)
        {
            var comment = await _repository.GetAsync(commentId);
            commentsDto.UpdateDateTime = DateTime.Now;
            _mapper.Map(commentsDto, comment);
            await _repository.SaveAsync();
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _repository.GetAsync(commentId);
            _repository.Remove(comment);
            await _repository.SaveAsync();
        }

        public async Task<CommentDto> GetCommentPostGroupAsync(int postId,int commentId)
        {
            var comment = await _repository
                .GetAllIncluding(u => u.User)
                .Where(p => p.PostId == postId)
                .SingleOrDefaultAsync(a => a.Id == commentId);
            var resultComment = _mapper.Map<Comment, CommentDto>(comment);
            return resultComment;
        }

        public async Task<CommentDto> CreateCommentForGroupAsync(int groupId, int userId, int postId,
            CreateGroupPostCommentDto createGroupComment)
        {
            var comment = _mapper.Map<CreateGroupPostCommentDto, Comment>(createGroupComment);
            comment.PostId = postId;
            comment.UserId = userId;
            comment.CreateDateTime = DateTime.Now;
            await _repository.AddAsyn(comment);
            await _repository.SaveAsync();
            return await GetCommentPostGroupAsync(postId, comment.Id);
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsForPostGroup(int postId)
        {
            var comments = await _repository.GetCommentsForPostGroup(postId);
            var resultComments = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
            return resultComments;
        }
    }
}