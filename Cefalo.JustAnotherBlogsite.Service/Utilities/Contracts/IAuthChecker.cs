using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Utilities.Contracts
{
    public interface IAuthChecker
    {
        public bool IsUserAuthorized(string? currentUser, string? userToBeUpdated, string? role);
        public bool IsTokenExpired(DateTime tokenGenerationTime, DateTime? passwordChangedAt);
    }
}
