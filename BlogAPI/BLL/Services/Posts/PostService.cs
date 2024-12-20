﻿using BlogAPI.BLL.Services.FileStorage;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Entities.PostsHashtags;
using BlogAPI.DAL.Uow;
using BlogAPI.PL.Common.Middlewares;
using BlogAPI.PL.Models.Posts;
using System.Net;

namespace BlogAPI.BLL.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorage _fileStorage;

        public PostService(IUnitOfWork uow, IFileStorage fileStorage)
        {
            _uow = uow;
            _fileStorage = fileStorage;
        }

        public async Task AddPostAsync(CreatePostRequest request, int authorId)
        {
            string fileName = await _fileStorage.AddFileAsync(request.Photo);

            var newPost = new Post()
            {
                Title = request.Title,
                Description = request.Description,
                PhotoFileName = fileName,
                AuthorId = authorId,
                CategoryId = request.CategoryId
            };
            await _uow.PostRepository.AddAsync(newPost);

            if(request.HashtagIds != null && request.HashtagIds.Any())
            {
                List<PostHashtag> newPostsHashtags = request.HashtagIds.Select(hashtagId => new PostHashtag()
                {
                    Post = newPost,
                    HashtagId = hashtagId
                }).ToList();

                await _uow.PostHashtagRepository.AddRangeAsync(newPostsHashtags);
            }

            await _uow.CompleteAsync();
        }

        public async Task UpdatePostAsync(UpdatePostRequest request, int currentUserId)
        {
            Post post = await _uow.PostRepository.FindAsync(request.Id);
            if(post == null)
            {
                throw new BusinessException(HttpStatusCode.NotFound, $"Поста з ідентифікатором {request.Id} не існує");
            }

            if(post.AuthorId != currentUserId)
            {
                throw new BusinessException(HttpStatusCode.Forbidden, $"Ви не маєте права на редагування поста, оскільки не є автором");
            }

            if(!string.IsNullOrEmpty(request.Title))
            {
                post.Title = request.Title;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                post.Description = request.Description;
            }

            if(request.Photo != null)
            {
                await _fileStorage.DeleteFileAsync(post.PhotoFileName);

                string newFilePhotoName = await _fileStorage.AddFileAsync(request.Photo);
                post.PhotoFileName = newFilePhotoName;
            }

            if (request.CategoryId.HasValue)
            {
                post.CategoryId = request.CategoryId.Value;
            }

            if(request.HashtagIds != null && request.HashtagIds.Any())
            {
                await UpdatePostHashtagsAsync(post, request.HashtagIds);//rempl method
            }

            post.UpdatedAt = DateTime.Now;
            await _uow.CompleteAsync();
        }

        public async Task<List<Post>> GetPostsByAuthorIdAsync(int authorId)
        {
            return await _uow.PostRepository.GetPostsByAuthorIdAsync(authorId);
        }

        public async Task<List<Post>> GetPostsByFilterAsync(PostFilterRequest request)
        {
            return await _uow.PostRepository.GetPostsByFilterAsync(request);
        }
        public async Task DeletePostAsync(int postId, int userId)
        {
            Post post = await _uow.PostRepository.FindAsync(postId);
            if (post == null)
            {
                throw new BusinessException(HttpStatusCode.NotFound, $"Поста з ідентифікатором {postId} не існує");
            }

            if (post.AuthorId != userId)
            {
                throw new BusinessException(HttpStatusCode.Forbidden, $"Ви не маєте права на редагування поста, оскільки не є автором");
            }

            await _fileStorage.DeleteFileAsync(post.PhotoFileName);

            await _uow.PostRepository.RemoveAsync(post);
            await _uow.CompleteAsync();
        }

        private async Task UpdatePostHashtagsAsync(Post post, List<int> hashtagIds)
        {
            List<int> existingHashtagIds = post.PostHashtags.Select(ph => ph.HashtagId).ToList();

            List<PostHashtag> tagsToRemove = post.PostHashtags
                .Where(ph => !hashtagIds.Contains(ph.HashtagId)).ToList();
            if (tagsToRemove.Any())
            {
                await _uow.PostHashtagRepository.RemoveRangeAsync(tagsToRemove);
            }

            List<int> tagsToAdd = hashtagIds
                .Where(id => !existingHashtagIds.Contains(id)).ToList();
            if (tagsToAdd.Any())
            {
                var newPostsHashtags = tagsToAdd.Select(tagId => new PostHashtag()
                {
                    PostId = post.Id,
                    HashtagId = tagId
                });

                await _uow.PostHashtagRepository.AddRangeAsync(newPostsHashtags);
            }
        }
    }
}
