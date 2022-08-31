using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Database.Context;
using Cefalo.JustAnotherBlogsite.Database.Models;
using Cefalo.JustAnotherBlogsite.Repository.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Repository.UnitTests.Repositories
{
    public class BlogRepositoryUnitTests
    {
        private readonly DataContext fakeContext;

        public BlogRepositoryUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            fakeContext = new DataContext(optionsBuilder.Options);
            fakeContext.Database.EnsureCreated();
        }

        [Fact]
        public async void CreateBlogAsync_WhenRequestIsValid_ReturnTypeIsCorrect()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser01@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogRepository = new BlogRepository(fakeContext);
            await fakeContext.SaveChangesAsync();

            var expected = fakeBlog;

            // Act
            var actual = await fakeBlogRepository.CreateBlogAsync(fakeBlog);

            // Assert
            actual.Should().BeAssignableTo<Blog>();
        }

        [Fact]
        public async void CreateBlogAsync_WhenRequestIsValid_DatabaseContainsNewlyCreatedBlog()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser01@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogRepository = new BlogRepository(fakeContext);
            await fakeContext.SaveChangesAsync();

            var expected = fakeBlog;

            // Act
            var actual = await fakeBlogRepository.CreateBlogAsync(fakeBlog);

            // Assert
            fakeContext.Blogs.Should().ContainEquivalentOf(expected);
        }

        [Fact]
        public async void CreateBlogAsync_WhenRequestIsValid_ReturnedDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser01@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeBlog;

            // Act
            var actual = await fakeBlogRepository.CreateBlogAsync(fakeBlog);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ReturnTypeIsCorrect()
        {
            // Arrange
            const int fakePageNumber = 1;
            const int fakePageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser02@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                x.WithArgumentsForConstructor(() =>
                    new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeBlogList;

            // Act
            var actual = await fakeBlogRepository.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            actual.Should().BeAssignableTo<List<Blog>>();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_DatabaseIsNotModified()
        {
            // Arrange
            const int fakePageNumber = 1;
            const int fakePageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser02@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;
            const int fakeBlogCount = 1;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                x.WithArgumentsForConstructor(() =>
                    new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expectedCount = await fakeContext.Blogs.CountAsync();

            // Act
            var actual = await fakeBlogRepository.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            fakeContext.Blogs.Should().ContainEquivalentOf(fakeBlog);
            expectedCount.Should().Be(fakeBlogCount);
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ReturnedDataIsAsExpected()
        {
            // Arrange
            const int fakePageNumber = 1;
            const int fakePageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser02@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogList = A.Fake<List<Blog>>(x => 
                x.WithArgumentsForConstructor(() => 
                    new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeBlogList;

            // Act
            var actual = await fakeBlogRepository.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            var actualResponse = (List<Blog>)actual;
            actualResponse.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void GetBlogCountAsync_WhenRequestIsValid_ReturnedDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "dummyusername3";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser03@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                x.WithArgumentsForConstructor(() =>
                    new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = await fakeContext.Blogs.CountAsync();

            // Act
            var actual = await fakeBlogRepository.GetBlogCountAsync();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_ReturnTypeIsCorrect()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser04@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeBlog;

            // Act
            var actual = await fakeBlogRepository.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            actual.Should().BeAssignableTo<Blog>();
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_ReturnedDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser04@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeBlog;

            // Act
            var actual = await fakeBlogRepository.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            actual.Should().BeEquivalentTo(fakeBlog);
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_DatabaseIsNotModified()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser04@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;
            const int fakeBlogCount = 1;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expectedCount = await fakeContext.Blogs.CountAsync();

            // Act
            var actual = await fakeBlogRepository.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            fakeContext.Blogs.Should().ContainEquivalentOf(fakeBlog);
            expectedCount.Should().Be(fakeBlogCount);
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_BlogWithGivenIdDoesNotExist_ReturnsNull()
        {
            // Arrange
            const int fakeBlogId = 4;
            const int fakeNotFoundId = 5;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser04@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expectedCount = await fakeContext.Blogs.CountAsync();

            // Act
            var actual = await fakeBlogRepository.GetBlogByBlogIdAsync(fakeNotFoundId);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ReturnTypeIsCorrect()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser05@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            const string updatedTitle = "Updated Dummy Blog Title 5";
            const string updatedDescription = "Updated Dummy Blog Description 5";

            var fakeUpdatedBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, updatedTitle, updatedDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeUpdatedBlog;

            // Act
            var actual = await fakeBlogRepository.UpdateBlogAsync(5, fakeUpdatedBlog);

            // Assert
            actual.Should().BeAssignableTo<Blog>();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ReturnedDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser05@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            const string updatedTitle = "Updated Dummy Blog Title 5";
            const string updatedDescription = "Updated Dummy Blog Description 5";

            var fakeUpdatedBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, updatedTitle, updatedDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            var fakeBlogRepository = new BlogRepository(fakeContext);

            var expected = fakeUpdatedBlog;

            // Act
            var actual = await fakeBlogRepository.UpdateBlogAsync(5, fakeUpdatedBlog);

            // Assert
            actual.Should().BeEquivalentTo(fakeUpdatedBlog);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_DataIsDeletedFromDatabase()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            const string fakeAuthorUsername = "dummyusername6";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser06@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            // Act
            var actual = await fakeBlogRepository.DeleteBlogAsync(fakeBlog);

            // Assert
            fakeContext.Blogs.Should().NotContainEquivalentOf(fakeBlog);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_ReturnedDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            const string fakeAuthorUsername = "dummyusername6";
            const string fakeAuthorFullName = "Dummy User";
            const string fakeAuthorEmail = "dummyuser06@dummymail.com";
            const int fakeAuthorRole = 1;
            byte[] fakePasswordHash = { 1, 2, 3 };
            byte[] fakePasswordSalt = { 1, 2, 3 };
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            DateTime fakePasswordChangedAt = DateTime.UtcNow;

            var fakeAuthor = A.Fake<User>(x =>
                x.WithArgumentsForConstructor(() =>
                    new User(fakeAuthorId, fakeAuthorUsername, fakeAuthorFullName, fakeAuthorEmail,
                        fakePasswordHash, fakePasswordSalt, fakeAuthorRole, fakeCreatedAt, fakeUpdatedAt, fakePasswordChangedAt)));

            var fakeBlog = A.Fake<Blog>(x =>
                x.WithArgumentsForConstructor(() =>
                    new Blog(fakeBlogId, fakeTitle, fakeDescription,
                        fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, fakeAuthor)));

            fakeContext.Blogs.Add(fakeBlog);
            await fakeContext.SaveChangesAsync();

            var fakeBlogRepository = new BlogRepository(fakeContext);

            // Act
            var actual = await fakeBlogRepository.DeleteBlogAsync(fakeBlog);

            // Assert
            actual.Should().Be(true);
        }
    }
}
