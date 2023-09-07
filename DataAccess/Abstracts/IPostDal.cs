using Entity.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IPostDal
    {
        public void Create(Post post);
        public Post Get(FilterDefinition<Post> filter);
        public ICollection<Post> GetAll(FilterDefinition<Post> filter = null);
        public void Update(Post post);
        public void Delete(string postId);
    }
}
