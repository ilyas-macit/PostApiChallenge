using DataAccess.Abstracts;
using Entity.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class PostDal : IPostDal
    {
        private readonly ApplicationContext _context;
        public PostDal(ApplicationContext context)
        {
            _context = context;
        }
        public void Create(Post post)
        {
            _context.Posts.InsertOne(post);
        }

        public void Delete(string postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", ObjectId.Parse(postId));
            _context.Posts.DeleteOne(filter);
        }

        public Post Get(FilterDefinition<Post> filter)
        {
            return _context.Posts.Find(filter).FirstOrDefault();
        }

        public ICollection<Post> GetAll(FilterDefinition<Post> filter = null)
        {
            return filter == null
                ? _context.Posts.Find(new BsonDocument()).ToList()
                : _context.Posts.Find(filter).ToList();
        }

        public void Update(Post post)
        {
            var filter = Builders<Post>.Filter.Eq("_id", ObjectId.Parse(post.Id));
            _context.Posts.ReplaceOne(filter, post);
        }
    }
}
