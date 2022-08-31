using AutoMapper;
using Cefalo.JustAnotherBlogsite.Database.Models;
using Cefalo.JustAnotherBlogsite.Repository.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Contracts;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using Cefalo.JustAnotherBlogsite.Service.DtoValidators;
using Cefalo.JustAnotherBlogsite.Service.Exceptions;
using Cefalo.JustAnotherBlogsite.Service.Services;
using Cefalo.JustAnotherBlogsite.Service.Utilities.Contracts;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.UnitTests.Services
{
    public class BlogServiceUnitTests
    {
        private readonly IBlogRepository fakeBlogRepository;
        private readonly IMapper fakeMapper;
        private readonly IHttpContextAccessor fakeHttpContextAccessor;
        private readonly BaseDtoValidator<BlogPostDto> fakeBlogPostDtoValidator;
        private readonly BaseDtoValidator<BlogUpdateDto> fakeBlogUpdateDtoValidator;
        private readonly IAuthChecker fakeAuthChecker;

        public BlogServiceUnitTests()
        {
            fakeBlogRepository = A.Fake<IBlogRepository>();
            fakeMapper = A.Fake<IMapper>();
            fakeHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            fakeBlogPostDtoValidator = A.Fake<BlogPostDtoValidator>();
            fakeBlogUpdateDtoValidator = A.Fake<BlogUpdateDtoValidator>();
            fakeAuthChecker = A.Fake<IAuthChecker>();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_ResponseDataIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            actual.Should().BeAssignableTo<BlogDetailsDto>();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_BlogPostDtoValidatorIsProperlyCalled()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            var actualResponse = (BlogDetailsDto)actual;
            actualResponse.Should().BeEquivalentTo(expectedResponse);

            A.CallTo(() => fakeBlogPostDtoValidator
                            .ValidateDto(fakeNewBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_MapperFromBlogPostDtoToBlogIsProperlyCalled()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            A.CallTo(() => fakeMapper
                            .Map<Blog>(fakeNewBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_MethodForGettingClaimFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_CreateBlogAsyncFromBlogRepositoryIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .CreateBlogAsync(fakeBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_MapperFromBlogToBlogDetailsDtoIsProperlyCalled()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            A.CallTo(() => fakeMapper
                        .Map<BlogDetailsDto>(fakeBlog))
                        .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogPostDtoIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var actual = await fakeBlogService.PostBlogAsync(fakeNewBlog);

            // Assert
            var actualResponse = (BlogDetailsDto)actual;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void PostBlogAsync_WhenDtoValidationFails_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            string errorMessage = "Dummy Error Message 1";

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            A.CallTo(() => fakeBlogPostDtoValidator
                    .ValidateDto(fakeNewBlog))
                    .Throws(new Exception(errorMessage));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();


            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Returns(fakeBlog);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.PostBlogAsync(fakeNewBlog));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void PostBlogAsync_WhenBlogRepositoryThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 1;
            const string fakeTitle = "Dummy Blog Title 1";
            const string fakeDescription = "Dummy Blog Description 1";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername1";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeNewBlog = A.Fake<BlogPostDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogPostDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            A.CallTo(() => fakeMapper
                    .Map<Blog>(fakeNewBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));


            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Dummy Error Message 1";
            A.CallTo(() => fakeBlogRepository
                    .CreateBlogAsync(fakeBlog))
                    .Throws(new Exception(errorMessage));

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));
            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(expectedResponse);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.PostBlogAsync(fakeNewBlog));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakePageNumber = 2;
            const int fakePageSize = 2;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogDetailsList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogDetailsList.Add(fakeBlogDetails);

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogsAsync(fakePageNumber, fakePageSize))
                    .Returns(fakeBlogList);

            A.CallTo(() => fakeMapper
                    .Map<List<BlogDetailsDto>>(fakeBlogList))
                    .Returns(fakeBlogDetailsList);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetailsList;

            // Act
            var actual = await fakeBlogService.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            actual.Should().BeAssignableTo<List<BlogDetailsDto>>();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_GetBlogsAsyncOfBlogRepositoryIsCalledCorrectly()
        {
            // Arrange
            const int fakePageNumber = 2;
            const int fakePageSize = 2;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogDetailsList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogDetailsList.Add(fakeBlogDetails);

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogsAsync(fakePageNumber, fakePageSize))
                    .Returns(fakeBlogList);

            A.CallTo(() => fakeMapper
                    .Map<List<BlogDetailsDto>>(fakeBlogList))
                    .Returns(fakeBlogDetailsList);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetailsList;

            // Act
            var actual = await fakeBlogService.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .GetBlogsAsync(fakePageNumber, fakePageSize))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_MapFromListOfBlogsToListOfBlogDetailsDtosIsCalledCorrectly()
        {
            // Arrange
            const int fakePageNumber = 2;
            const int fakePageSize = 2;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogDetailsList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogDetailsList.Add(fakeBlogDetails);

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogsAsync(fakePageNumber, fakePageSize))
                    .Returns(fakeBlogList);

            A.CallTo(() => fakeMapper
                    .Map<List<BlogDetailsDto>>(fakeBlogList))
                    .Returns(fakeBlogDetailsList);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetailsList;

            // Act
            var actual = await fakeBlogService.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            A.CallTo(() => fakeMapper
                            .Map<List<BlogDetailsDto>>(fakeBlogList))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogsAsync_WhenRequestIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakePageNumber = 2;
            const int fakePageSize = 2;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 1;
            const string fakeAuthorUsername = "dummyusername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogDetailsList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogDetailsList.Add(fakeBlogDetails);

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogsAsync(fakePageNumber, fakePageSize))
                    .Returns(fakeBlogList);

            A.CallTo(() => fakeMapper
                    .Map<List<BlogDetailsDto>>(fakeBlogList))
                    .Returns(fakeBlogDetailsList);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetailsList;

            // Act
            var actual = await fakeBlogService.GetBlogsAsync(fakePageNumber, fakePageSize);

            // Assert
            var actualResponse = (List<BlogDetailsDto>)actual;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void GetBlogsAsync_WhenBlogRepositoryThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakePageNumber = 2;
            const int fakePageSize = 2;
            const int fakeBlogId = 2;
            const string fakeTitle = "Dummy Blog Title 2";
            const string fakeDescription = "Dummy Blog Description 2";
            const int fakeAuthorId = 2;
            const string fakeAuthorUsername = "dummyusername2";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogList = A.Fake<List<Blog>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<Blog>()));

            fakeBlogList.Add(fakeBlog);

            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogDetailsList = A.Fake<List<BlogDetailsDto>>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new List<BlogDetailsDto>()));

            fakeBlogDetailsList.Add(fakeBlogDetails);

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Dummy Error Message 2";

            A.CallTo(() => fakeBlogRepository
                    .GetBlogsAsync(fakePageNumber, fakePageSize))
                    .Throws(new Exception(errorMessage));

            A.CallTo(() => fakeMapper
                    .Map<List<BlogDetailsDto>>(fakeBlogList))
                    .Returns(fakeBlogDetailsList);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetailsList;

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                await fakeBlogService
                                        .GetBlogsAsync(fakePageNumber, fakePageSize));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void GetBlogCountAsync_WhenRequestIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogCount = 3;

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogCountAsync())
                    .Returns(fakeBlogCount);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expected = fakeBlogCount;

            // Act
            var actual = await fakeBlogService.GetBlogCountAsync();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public async void GetBlogCountAsync_WhenRequestIsValid_GetBlogCountAsyncOfBlogRepositoryIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogCount = 3;

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogCountAsync())
                    .Returns(fakeBlogCount);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.GetBlogCountAsync();

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .GetBlogCountAsync())
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogCountAsync_WhenBlogRepositoryThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Dummy Error Message 3";

            A.CallTo(() => fakeBlogRepository
                    .GetBlogCountAsync())
                    .Throws(new Exception(errorMessage));

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                await fakeBlogService
                                        .GetBlogCountAsync());

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_ResponseShouldBeOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));


            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeBlogDetails);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetails;

            // Act
            var actual = await fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            actual.Should().BeAssignableTo<BlogDetailsDto>();
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_GetBlogByBlogIdAsyncOfBlogRepositoryIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));


            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeBlogDetails);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetails;

            // Act
            var actual = await fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .GetBlogByBlogIdAsync(fakeBlogId))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_MapperFromBlogToBlogDetailsDtoIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));


            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeBlogDetails);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetails;

            // Act
            var actual = await fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeMapper
                            .Map<BlogDetailsDto>(fakeBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenRequestIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));


            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeBlogDetails);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetails;

            // Act
            var actual = await fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId);

            // Assert
            var actualResponse = (BlogDetailsDto)actual;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenBlogIsNull_NotFoundExceptionIsThrown()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));


            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(null as Blog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeBlogDetails);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetails;

            string errorMessage = "Blog not found.";

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                await fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void GetBlogByBlogIdAsync_WhenBlogRepositoryThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 4;
            const string fakeTitle = "Dummy Blog Title 4";
            const string fakeDescription = "Dummy Blog Description 4";
            const int fakeAuthorId = 4;
            const string fakeAuthorUsername = "dummyusername4";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));


            var fakeBlogDetails = A.Fake<BlogDetailsDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                                    fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Dummy Error Message 4";

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Throws(new Exception(errorMessage));

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeBlogDetails);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            var expectedResponse = fakeBlogDetails;

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                                await fakeBlogService.GetBlogByBlogIdAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ResponseIsOfCorrectType()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            actual.Should().BeAssignableTo<BlogDetailsDto>();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_BlogUpdateDtoValidatorIsProperlyCalled()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeBlogUpdateDtoValidator
                            .ValidateDto(fakeBlogDetails))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_GetBlogByBlogIdAsyncOfBlogRepositoryIsProperlyCalled()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .GetBlogByBlogIdAsync(fakeBlogId))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_MethodForGettingCurrentUserIdFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                            .HttpContext
                            .User
                            .FindFirst(ClaimTypes.NameIdentifier))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_MethodForGettingCurrentUserRoleFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                            .HttpContext
                            .User
                            .FindFirst(ClaimTypes.Role))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_MethodForGettingTokenGenerationTimeFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                            .HttpContext
                            .User
                            .FindFirst(ClaimTypes.Expiration))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_IsUserAuthorizedOfAuthCheckerIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeAuthChecker
                            .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_IsTokenExpiredOfAuthCheckerIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeAuthChecker
                            .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_UpdateBlogAsyncOfBlogRepositoryIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .UpdateBlogAsync(fakeBlogId, fakeBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_MapperFromBlogToBlogDetailsDtoIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            A.CallTo(() => fakeMapper
                            .Map<BlogDetailsDto>(fakeBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void UpdateBlogAsync_WhenBlogIsNull_NotFoundExceptionIsThrownCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(null as Blog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            string errorMessage = "Blog not found.";

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenUserIsNotAuthorized_ForbiddenExceptionIsThrownCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(false);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            string errorMessage = "You are not authorized.";

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ForbiddenException>();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenTokenIsExpired_UnauthorizedExceptionIsThrownCorrectly()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(true);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            string errorMessage = "Token is expired. Log in again.";

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<UnauthorizedException>();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenDtoValidationFails_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            string errorMessage = "Dummy Error Message 5";

            A.CallTo(() => fakeBlogUpdateDtoValidator
                    .ValidateDto(fakeBlogDetails))
                    .Throws(new Exception(errorMessage));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenGetBlogByBlogIdAsyncOfBlogServiceThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Dummy Error Message 5";

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Throws(new Exception(errorMessage));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenUpdateBlogAsyncOfBlogServiceThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            string errorMessage = "Dummy Error Message 5";

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Throws(new Exception(errorMessage));

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void UpdateBlogAsync_WhenRequestIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 5;
            const string fakeTitle = "Dummy Blog Title 5";
            const string fakeDescription = "Dummy Blog Description 5";
            const int fakeAuthorId = 5;
            const string fakeAuthorUsername = "dummyusername5";
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlogDetails = A.Fake<BlogUpdateDto>(x =>
                            x.WithArgumentsForConstructor(() =>
                                new BlogUpdateDto(fakeTitle, fakeDescription)));

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            var fakeUpdatedBlogDetails = A.Fake<BlogDetailsDto>(x =>
                x.WithArgumentsForConstructor(() =>
                    new BlogDetailsDto(fakeBlogId, fakeTitle, fakeDescription,
                        fakeAuthorId, fakeAuthorUsername, fakeCreatedAt, fakeUpdatedAt)));

            A.CallTo(() => fakeBlogRepository
                    .UpdateBlogAsync(fakeBlogId, fakeBlog))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeMapper
                    .Map<BlogDetailsDto>(fakeBlog))
                    .Returns(fakeUpdatedBlogDetails);

            var expectedResponse = fakeUpdatedBlogDetails;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.UpdateBlogAsync(fakeBlogId, fakeBlogDetails);

            // Assert
            var actualResponse = (BlogDetailsDto)actual;
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        // ..................................ON PROGRESS......................................................
        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_GetBlogByBlogIdAsyncOfBlogRepositoryIsCalledCorrecty()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .GetBlogByBlogIdAsync(fakeBlogId))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_MethodForGettingCurrentUserIdFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                            .HttpContext
                            .User
                            .FindFirst(ClaimTypes.NameIdentifier))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_MethodForGettingCurrentUserRoleFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                            .HttpContext
                            .User
                            .FindFirst(ClaimTypes.Role))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_MethodForGettingTokenGenerationTimeFromHttpContextIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(fakeTokenGenerationTime, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeHttpContextAccessor
                            .HttpContext
                            .User
                            .FindFirst(ClaimTypes.Expiration))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_IsUserAuthorizedOfAuthCheckerIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeAuthChecker
                            .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_IsTokenExpiredOfAuthCheckerIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeAuthChecker
                            .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_DeleteBlogAsyncOfBlogServiceIsCalledCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            A.CallTo(() => fakeBlogRepository
                            .DeleteBlogAsync(fakeBlog))
                            .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void DeleteBlogAsync_WhenRequestIsValid_ResponseDataIsAsExpected()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var expectedResponse = true;

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var actual = await fakeBlogService.DeleteBlogAsync(fakeBlogId);

            // Assert
            actual.Should().Be(expectedResponse);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenBlogIsNull_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Blog not found.";

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(null as Blog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.DeleteBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenUserIsNotAuthorized_ForbiddenExceptionIsThrownCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            string errorMessage = "You are not authorized.";

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(false);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.DeleteBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ForbiddenException>();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenTokenIsExpired_UnauthorizedExceptionIsThrownCorrectly()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            string errorMessage = "Token is expired. Log in again.";

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(true);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.DeleteBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<UnauthorizedException>();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenGetBlogByBlogIdAsyncOfBlogRepositoryThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            string errorMessage = "Dummy Error Message 6";

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Throws(new Exception(errorMessage));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Returns(true);

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.DeleteBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async void DeleteBlogAsync_WhenDeleteBlogAsyncOfBlogRepositoryThrowsException_SameExceptionIsRaised()
        {
            // Arrange
            const int fakeBlogId = 6;
            const string fakeTitle = "Dummy Blog Title 6";
            const string fakeDescription = "Dummy Blog Description 6";
            const int fakeAuthorId = 6;
            DateTime fakeCreatedAt = DateTime.UtcNow;
            DateTime fakeUpdatedAt = DateTime.UtcNow;
            const int fakeRole = 1;
            DateTime fakeTokenGenerationTime = DateTime.UtcNow;

            var fakeBlog = A.Fake<Blog>(x =>
                        x.WithArgumentsForConstructor(() =>
                            new Blog(fakeBlogId, fakeTitle, fakeDescription,
                                fakeCreatedAt, fakeUpdatedAt, fakeAuthorId, null)));

            var fakeBlogRepository = A.Fake<IBlogRepository>();

            A.CallTo(() => fakeBlogRepository
                    .GetBlogByBlogIdAsync(fakeBlogId))
                    .Returns(fakeBlog);

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.NameIdentifier, fakeAuthorId.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Role))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Role, fakeRole.ToString()))));

            A.CallTo(() => fakeHttpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(ClaimTypes.Expiration))
                    .Returns(A.Fake<Claim>(x => x.WithArgumentsForConstructor(() =>
                        new Claim(ClaimTypes.Expiration, fakeTokenGenerationTime.ToString()))));

            A.CallTo(() => fakeAuthChecker
                    .IsUserAuthorized(fakeAuthorId.ToString(), null as string, fakeRole.ToString()))
                    .Returns(true);

            A.CallTo(() => fakeAuthChecker
                    .IsTokenExpired(A<DateTime>.Ignored, null as DateTime?))
                    .Returns(false);

            string errorMessage = "Dummy Error Message 6";

            A.CallTo(() => fakeBlogRepository
                    .DeleteBlogAsync(fakeBlog))
                    .Throws(new Exception(errorMessage));

            var fakeBlogService = new BlogService(fakeBlogRepository, fakeMapper, fakeHttpContextAccessor,
                                    fakeBlogPostDtoValidator, fakeBlogUpdateDtoValidator, fakeAuthChecker);

            // Act
            var exception = await Record.ExceptionAsync(async () => await fakeBlogService.DeleteBlogAsync(fakeBlogId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errorMessage);
        }
    }
}
