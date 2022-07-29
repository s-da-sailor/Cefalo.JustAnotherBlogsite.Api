using Cefalo.JustAnotherBlogsite.Api;
using Cefalo.JustAnotherBlogsite.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Contracts
{
    public interface IAuthService
    {
        public Task<string> SignupAsync(SignupDto request);
        public Task<string> LoginAsync(LoginDto request);
    }
}
