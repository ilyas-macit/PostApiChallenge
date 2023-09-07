using Business.Abstracts;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace PostTests
{
    public class PostsControllerTests
    {
        [Test]
        public void GetAll_Return_The_Correct_Number_Of_Post()
        {
            //Arrange
            string postId = "64f9d944fb6c96169681f476";

            var service = new Mock<IPostService>();
            service.Setup(service => service.GetById(postId)).Returns(new Entity.Concrete.Post
            {
                Id = postId,
                Title = "deneme",
                ShortDescription = "deneme",
                Description = "deneme deneme",
                Tags = { },
                CreatedAt = DateTime.Now
            });

            var controller = new PostsController(service.Object);

            //Action

            var result = controller.GetById(postId) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var post = result.Value as Post;
            Assert.IsNotNull(post);
            Assert.AreEqual(postId, post.Id);
            Assert.AreEqual("deneme", post.Title);
        }

        [Test]
        public async Task GetAll_ReturnsCorrectNumberOfPosts()
        {
            // Arrange
            var service = new Mock<IPostService>();
            var posts = new List<Post>
    {
        new Post
        {
            Id = "1",
            Title = "Post 1",
            ShortDescription = "Short description 1",
            Description = "Description 1",
            Tags = new List<string> { "Tag1", "Tag2" },
            CreatedAt = DateTime.Now
        },
        new Post
        {
            Id = "2",
            Title = "Post 2",
            ShortDescription = "Short description 2",
            Description = "Description 2",
            Tags = new List<string> { "Tag3", "Tag4" },
            CreatedAt = DateTime.Now
        }
    };

            service.Setup(service => service.GetAll()).Returns(posts);

            var controller = new PostsController(service.Object);

            // Act
            var actionResult = await controller.GetAll();
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var returnedPosts = result.Value as List<Post>;
            Assert.IsNotNull(returnedPosts);
            Assert.AreEqual(2, returnedPosts.Count);
        }


        [Test]
        public void CreatePost_WithValidDto_ReturnsOkResult()
        {
            // Arrange
            var postDto = new CreatePostDto
            {
                Title = "Sample Title",
                Description = "Sample Description",
                ShortDescription = "Sample Short Description",
                Tags = new List<string> { "tag1", "tag2" }
            };

            var service = new Mock<IPostService>();
            var controller = new PostsController(service.Object);

            // Action
            var result = controller.CreatePost(postDto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

            service.Verify(service => service.Create(It.Is<Post>(post =>
                post.Title == postDto.Title &&
                post.Description == postDto.Description &&
                post.ShortDescription == postDto.ShortDescription &&
                post.Tags.SequenceEqual(postDto.Tags)
            )), Times.Once);
        }

        [Test]
        public void Update_WithValidDto_ReturnsOkResult()
        {
            // Arrange
            var postDto = new UpdatePostDto
            {
                Id = "validId",
                Title = "Updated Title",
                ShortDescription = "Updated Short Description",
                Description = "Updated Description",
                Tags = new List<string> { "updatedTag1", "updatedTag2" }
            };

            var service = new Mock<IPostService>();
            var controller = new PostsController(service.Object);

            // Action
            var result = controller.Update(postDto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

            service.Verify(service => service.Update(It.Is<Post>(post =>
                post.Id == postDto.Id &&
                post.Title == postDto.Title &&
                post.Description == postDto.Description &&
                post.ShortDescription == postDto.ShortDescription &&
                post.Tags.SequenceEqual(postDto.Tags)
            )), Times.Once);
        }
        [Test]
        public void Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            string postId = "validId";

            var service = new Mock<IPostService>();
            var controller = new PostsController(service.Object);

            // Action
            var result = controller.Delete(postId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

            service.Verify(service => service.Delete(postId), Times.Once);
        }

    }
}