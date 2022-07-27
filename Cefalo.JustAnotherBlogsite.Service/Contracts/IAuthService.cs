using Cefalo.JustAnotherBlogsite.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Contracts
{
    public interface IAuthService
    {
        public Task<string> SignupAsync(User user);
    }
}
