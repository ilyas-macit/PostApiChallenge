using Business.Abstracts;
using DataAccess.Abstracts;
using Entity.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class PostService : IPostService
    {
        private readonly IPostDal _postDal;

        public PostService(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public void Create(Post post)
        {
            if (post != null)
            {
                _postDal.Create(post);
                return;
            }

            throw new NullReferenceException("Post is empty!");
        }

        public void Delete(string postId)
        {
            var post = _postDal.Get(Builders<Post>.Filter.Eq("_id", ObjectId.Parse(postId)));
            if (post != null)
            {
                _postDal.Delete(postId);
                return;
            }
            throw new Exception("The post has not be found!");
        }

        public List<Post> GetAll()
        {
            return _postDal.GetAll().ToList();
        }

        public Post GetById(string postId)
        {
            var post = _postDal.Get(Builders<Post>.Filter.Eq("_id", ObjectId.Parse(postId)));
            if (post == null)
                throw new Exception("The post has not be found!");
            return post;
        }

        public void Update(Post post)
        {
            if (post != null)
            {
                var filter = Builders<Post>.Filter.Eq("_id", post.Id);
                var updatedPost = _postDal.Get(filter);
                if (updatedPost != null)
                {
                    updatedPost.Title = post.Title;
                    updatedPost.Description = post.Description;
                    updatedPost.Tags = post.Tags;
                    updatedPost.UpdatedAt = post.UpdatedAt;
                    _postDal.Update(updatedPost);
                }
                return;
            }
            throw new NullReferenceException("Post is null");
        }
    }
}
