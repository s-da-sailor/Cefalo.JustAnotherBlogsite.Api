using Cefalo.JustAnotherBlogsite.Api.Controllers;
using Cefalo.JustAnotherBlogsite.Api.Filters;
using Cefalo.JustAnotherBlogsite.Api.Wrappers;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.JustAnotherBlogsite.Api.UnitTests.Controllers
{
    public class BlogControllerUnitTests
    {
        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, fakeAuthorId, 
                                fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .PostBlogAsync(fakeRequest))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);    

            // Act
            var actual = await fakeBlogController.PostBlogAsync(fakeRequest);

            // Assert
            actual.Should().BeOfType<ActionResult<Response<BlogDetailsDto>>>();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_InnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, 
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                        .PostBlogAsync(fakeRequest))
                        .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var actual = await fakeBlogController.PostBlogAsync(fakeRequest);

            // Assert
            var actualInnerObject = actual.Result;
            actualInnerObject.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_ValueOfInnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, fakeAuthorId, 
                                fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .PostBlogAsync(fakeRequest))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var actual = await fakeBlogController.PostBlogAsync(fakeRequest);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.Value.Should().BeOfType<Response<BlogDetailsDto>>();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_StatusCodeIs200()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, 
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .PostBlogAsync(fakeRequest))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var actual = await fakeBlogController.PostBlogAsync(fakeRequest);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, 
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .PostBlogAsync(fakeRequest))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.PostBlogAsync(fakeRequest);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            var actualResponse = (Response<BlogDetailsDto>?)actualObject?.Value;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_PostBlogAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                            x.WithArgumentsForConstructor(() => 
                            new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() =>
                        new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, 
                            fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .PostBlogAsync(fakeRequest))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.PostBlogAsync(fakeRequest);

            // Assert
            A.CallTo(() => fakeBlogService.PostBlogAsync(fakeRequest)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogServiceThrowsException_SameExceptionIsCaught()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "DummyUsername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeRequest = A.Fake<BlogPostDto>(x => 
                            x.WithArgumentsForConstructor(() => 
                            new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, 
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            string errorMessage = "Dummy Error Message 1";

            A.CallTo(() => fakeBlogService
                        .PostBlogAsync(fakeRequest))
                        .Throws(new Exception(errorMessage));

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogController.PostBlogAsync(fakeRequest));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x => 
                        x.WithArgumentsForConstructor(() => 
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription, 
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new List<BlogDetailsDto>()));
            
            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x => 
                            x.WithArgumentsForConstructor(() => 
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            actual.Should().BeOfType<ActionResult<PagedResponse<List<BlogDetailsDto>>>>();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_InnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            var actualInnerObject = actual.Result;
            actualInnerObject.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ValueOfInnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.Value.Should().BeOfType<PagedResponse<List<BlogDetailsDto>>>();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_StatusCodeIs200()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;
            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            var actualResponse = (PagedResponse<List<BlogDetailsDto>>?)actualObject?.Value;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_GetBlogsAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            A.CallTo(() => fakeBlogService.GetBlogsAsync(pageNumber, pageSize)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_GetBlogCountAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            A.CallTo(() => fakeBlogService.GetBlogCountAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogsAsync_WhenBlogServiceThrowsException_SameExceptionIsCaught()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 1;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            string errorMessage = "Dummy Error Message 2";

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(pageNumber, pageSize))
                        .Throws(new Exception(errorMessage));

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, pageNumber, pageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogController.GetBlogsAsync(fakeFilter));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Theory]
        [InlineData(1, 100, 1, 12)]
        [InlineData(1, -100, 1, 1)]
        [InlineData(-10, 1, 1, 1)]
        [InlineData(-10, 100, 1, 12)]
        [InlineData(-10, -100, 1, 1)]
        public async void GetBlogsAsync_WhenPaginationFilterIsInvalid_ServiceMethodIsCalledWithValidatedFilter(int pageNumber, int pageSize, int validPageNumber, int validPageSize)
        {
            // Arrange
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "DummyUsername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeFilter = A.Fake<PaginationFilter>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new PaginationFilter(pageNumber, pageSize)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogList.Add(fakeBlog);

            A.CallTo(() => fakeBlogService
                        .GetBlogsAsync(validPageNumber, validPageSize))
                        .Returns(fakeBlogList);

            const int fakeTotalRecords = 1;
            A.CallTo(() => fakeBlogService
                        .GetBlogCountAsync())
                        .Returns(fakeTotalRecords);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<PagedResponse<List<BlogDetailsDto>>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new PagedResponse<List<BlogDetailsDto>>(fakeBlogList, validPageNumber, validPageSize)));
            expectedResponse.TotalRecords = fakeTotalRecords;

            // Act
            var actual = await fakeBlogController.GetBlogsAsync(fakeFilter);

            // Assert
            A.CallTo(() => fakeBlogService.GetBlogsAsync(validPageNumber, validPageSize)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogIdIsValid_InnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.GetBlogAsync(fakeBlogId);

            // Assert
            actual.Should().BeOfType<ActionResult<Response<BlogDetailsDto>>>();
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogIdIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.GetBlogAsync(fakeBlogId);

            // Assert
            var actualInnerObject = actual.Result;
            actualInnerObject.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogIdIsValid_ValueOfInnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.GetBlogAsync(fakeBlogId);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.Value.Should().BeOfType<Response<BlogDetailsDto>>();
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogIdIsValid_StatusCodeIs200()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.GetBlogAsync(fakeBlogId);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogIdIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.GetBlogAsync(fakeBlogId);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            var actualResponse = (Response<BlogDetailsDto>?)actualObject?.Value;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogIdIsValid_GetBlogByBlogIdAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.GetBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogAsync_WhenBlogServiceThrowsException_SameExceptionIsCaught()
        {
            // Arrange
            const int fakeBlogId = 3;
            const string fakeTitle = "Dummy Blog Title 3";
            const string fakeDescription = "Dummy Blog Description 3";
            const int fakeAuthorId = 3;
            const string fakeAuthorUsername = "DummyUsername3";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            string errorMessage = "Dummy Error Message 3";

            A.CallTo(() => fakeBlogService
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Throws(new Exception(errorMessage));

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogController.GetBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog);

            // Assert
            actual.Should().BeOfType<ActionResult<Response<BlogDetailsDto>>>();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_InnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog);

            // Assert
            var actualInnerObject = actual.Result;
            actualInnerObject.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ValueOfInnerObjectOfResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.Value.Should().BeOfType<Response<BlogDetailsDto>>();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_StatusCodeIs200()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            actualObject?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog);

            // Assert
            var actualInnerObject = actual.Result;
            var actualObject = (OkObjectResult?)actualInnerObject;
            var actualResponse = (Response<BlogDetailsDto>?)actualObject?.Value;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_UpdateBlogAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Returns(fakeBlog);

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var actual = await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog);

            // Assert
            A.CallTo(() => fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenBlogServiceThrowsException_SameExceptionIsCaught()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "DummyUsername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeUpdatedBlog = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlogService = A.Fake<IBlogService>();
            var fakeBlog = A.Fake<BlogDetailsDto>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            string errorMessage = "Dummy Error Message 4";

            A.CallTo(() => fakeBlogService
                    .UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog))
                    .Throws(new Exception(errorMessage));

            var fakeBlogController = new BlogController(fakeBlogService);

            var expectedResponse = A.Fake<Response<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new Response<BlogDetailsDto>(fakeBlog)));

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogController.UpdateBlogAsync(fakeBlogId, fakeUpdatedBlog));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 5;


            var fakeBlogService = A.Fake<IBlogService>();

            A.CallTo(() => fakeBlogService
                    .DeleteBlogAsync(fakeBlogId))
                    .Returns(true);

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var actual = await fakeBlogController.DeleteBlogAsync(fakeBlogId);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_StatusCodeIs204()
        {
            // Arrange
            const int fakeBlogId = 5;


            var fakeBlogService = A.Fake<IBlogService>();

            A.CallTo(() => fakeBlogService
                    .DeleteBlogAsync(fakeBlogId))
                    .Returns(true);

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var actual = await fakeBlogController.DeleteBlogAsync(fakeBlogId);

            // Assert
            var actualObject = (NoContentResult?)actual;
            actualObject?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_DeleteBlogAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;


            var fakeBlogService = A.Fake<IBlogService>();

            A.CallTo(() => fakeBlogService
                    .DeleteBlogAsync(fakeBlogId))
                    .Returns(true);

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var actual = await fakeBlogController.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeBlogService.DeleteBlogAsync(fakeBlogId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenBlogServiceThrowsException_SameExceptionIsCaught()
        {
            // Arrange
            const int fakeBlogId = 5;


            var fakeBlogService = A.Fake<IBlogService>();

            string errorMessage = "Dummy Error Message 5";

            A.CallTo(() => fakeBlogService
                    .DeleteBlogAsync(fakeBlogId))
                    .Throws(new Exception(errorMessage));

            var fakeBlogController = new BlogController(fakeBlogService);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogController.DeleteBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }
    }
}
