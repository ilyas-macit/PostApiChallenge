using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IPostService
    {
        void Create(Post post);
        void Update(Post post);
        void Delete(string postId);

        List<Post> GetAll();
        Post GetById(string postId);
    }
}
