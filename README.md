# Just-Another-Blogsite

[https://just-another-blogsite.netlify.app](https://just-another-blogsite.netlify.app)

A RestAPI implementation of a blog site that supports authentication and authorization and can provide response data in a variety of formats with content-negotiation. This is a .NET-based backend of this application.

## Quick Overview

- **API ENDPOINTS**

  **_/api/Blog_**

      Supported Methods and Functionalities:
                              GET (Get all blogs)
                              POST (Post a blog)

  **_/api/Blog/:blogId_**

      Supported Methods and Functionalities:
                              GET (Get a specific blog)
                              PUT (Update a blog)
                              DELETE (Delete a blog)

  **_/api/User_**

      Supported Methods and Functionalities:
                              GET (Get all users)

  **_/api/User/signup_**

      Supported Methods and Functionalities:
                              POST (User signup)

  **_/api/User/login_**

      Supported Methods and Functionalities:
                              POST (User login)

  **_/api/User/logout_**

      Supported Methods and Functionalities:
                              POST (User logout)

  **_/api/User/verify_**

      Supported Methods and Functionalities:
                              POST (Verify if a user is logged in)

  **_/api/User/:userId_**

      Supported Methods and Functionalities:
                              GET (Get a specific user)
                              PUT (Update a user)
                              DELETE (Delete a user)

  **_/api/User/search/{searchParam}_**

      Supported Methods and Functionalities:
                              POST (User search with a search parameter)

  **_/api/User/:userId/blogs_**

      Supported Methods and Functionalities:
                              GET (Get user specific blogs)

- **Types of Contents Served**

          Accept: application/json
          Accept: application/xml
          Accept: text/plain
          Accept: text/html

## Quick start

1.  **Get to the workplace.**

    After cloning this repository navigate into to Controller project directory (Cefalo.JustAnotherBlogsite.Api/) and load it in your IDE/Editor

    ```shell
    cd directory_name
    code .  #to open in vscode
    ```

2.  **Open the source code and start editing**

    - If project folder is first opened in VS Code, a notification will apprear: "Required assets to build and debug are missing. Add them?". Select **Yes**.
    - Run the app by entering the following command in the command shell

      ```shell
      dotnet run
      ```

    By default this server will be running at `http://localhost:7234`!

## Project Structure

    ├── Cefalo.JustAnotherBlogsite.Api.UnitTests/
      ├── Controllers/
        ├── BlogControllerUnitTests.cs
      ├── Cefalo.JustAnotherBlogsite.Api.UnitTests.csproj
      ├── Usings.cs
    ├── Cefalo.JustAnotherBlogsite.Api/
      ├── Controllers/
        ├── AuthController.cs
        ├── BlogController.cs
        ├── UserController.cs
      ├── Filters/
        ├── PaginationFilter.cs
      ├── Middlewares/
        ├── CustomExceptionHandlingMiddleware.cs
      ├── Properties/
        ├── launchSettings.json
      ├── Utils/
        ├── Formatters/
          ├── BlogHtmlOutputFormatter.cs
          ├── BlogPlainTextOutputFormatter.cs
          ├── BlogXmlOutputFormatter.cs
          ├── UserHtmlOutputFormatter.cs
          ├── UserPlainTextOutputFormatter.cs
          ├── UserXmlOutputFormatter.cs
      ├── Wrappers/
        ├── PagedResponse.cs
        ├── Response.cs
      ├── Cefalo.JustAnotherBlogsite.Api.csproj
      ├── Program.cs
      ├── appsettings.Development.json
      ├── appsettings.json
    ├── Cefalo.JustAnotherBlogsite.Database/
      ├── Configurations/
        ├── BlogConfiguration.cs
        ├── UserConfiguration.cs
      ├── Context/
        ├── DataContext.cs
      ├── Migrations/
        ├── 20220811050543_v1.Designer.cs
        ├── 20220811050543_v1.cs
        ├── DataContextModelSnapshot.cs
      ├── Models/
        ├── Blog.cs
        ├── User.cs
      ├── Cefalo.JustAnotherBlogsite.Database.csproj
    ├── Cefalo.JustAnotherBlogsite.Repository.UnitTests/
      ├── Repositories/
        ├── BlogRepositoryUnitTests.cs
      ├── Cefalo.JustAnotherBlogsite.Repository.UnitTests.csproj
      ├── Usings.cs
    ├── Cefalo.JustAnotherBlogsite.Repository/
      ├── Contracts/
        ├── IBlogRepository.cs
        ├── IUserRepository.cs
      ├── Repositories/
        ├── BlogRepository.cs
        ├── UserRepository.cs
      ├── Cefalo.JustAnotherBlogsite.Repository.csproj
    ├── Cefalo.JustAnotherBlogsite.Service.UnitTests/
      ├── Services/
        ├── BlogServiceUnitTests.cs
      ├── Cefalo.JustAnotherBlogsite.Service.UnitTests.csproj
      ├── Usings.cs
    ├── Cefalo.JustAnotherBlogsite.Service/
      ├── AutoMapperProfiles/
        ├── BlogProfile.cs
        ├── UserProfile.cs
      ├── Contracts/
        ├── IAuthService.cs
        ├── IBlogService.cs
        ├── IUserService.cs
      ├── DtoValidators/
        ├── BaseDtoValidator.cs
        ├── BlogPostDtoValidator.cs
        ├── BlogUpdateDtoValidator.cs
        ├── LoginDtoValidator.cs
        ├── SignupDtoValidator.cs
        ├── UserUpdateDtoValidator.cs
      ├── Dtos/
        ├── BlogDetailsDto.cs
        ├── BlogPostDto.cs
        ├── BlogUpdateDto.cs
        ├── LoginDto.cs
        ├── SignupDto.cs
        ├── UserDetailsDto.cs
        ├── UserUpdateDto.cs
      ├── Exceptions/
        ├── BadRequestException.cs
        ├── ForbiddenException.cs
        ├── NotFoundException.cs
        ├── UnauthorizedException.cs
      ├── Services/
        ├── AuthService.cs
        ├── BlogService.cs
        ├── UserService.cs
      ├── Utilities/
        ├── Contracts/
          ├── IAuthChecker.cs
        ├── AuthChecker.cs
        ├── PasswordHash.cs
      ├── Cefalo.JustAnotherBlogsite.Service.csproj
    ├── Cefalo.JustAnotherBlogsite.Api.sln

1.  **`Cefalo.JustAnotherBlogsite.Api/`**: This directory contains Controller project for Just-Another-Blogsite. This project interacts with Service Project. Pagination filter, error handling middleware, custom response formatters, pagination wrappers are also included here. 

2.  **`Cefalo.JustAnotherBlogsite.Service/`**: This directory contains Service project for Just-Another-Blogsite. All the business logics are included here. DTOS, DTO validators, AutoMapper profiles, exceptions, service interfaces and implementations are included here.

3.  **`Cefalo.JustAnotherBlogsite.Repository/`**: This directory contains Repository project for Just-Another-Blogsite. Interaction with database is done here and this project interacts with the Database project. Repository interfaces and implementations are included here.

4.  **`Cefalo.JustAnotherBlogsite.Database/`**: This directory contains Database project for Just-Another-Blogsite. Database models, configurations, context and migrations scripts are included here.

5.  **`Cefalo.JustAnotherBlogsite.Api.UnitTests/`**: This directory contains unit tests for the Controller project.

6.  **`Cefalo.JustAnotherBlogsite.Service.UnitTests/`**: This directory contains unit tests for the Service project.

7.  **`Cefalo.JustAnotherBlogsite.Repository.UnitTests/`**: This directory contains unit tests for the Repository project.
