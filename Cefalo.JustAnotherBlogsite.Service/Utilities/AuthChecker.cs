using Cefalo.JustAnotherBlogsite.Service.Utilities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Utilities
{
    public class AuthChecker : IAuthChecker
    {
        public bool IsUserAuthorized(string? currentUser, string? userToBeUpdated, string? role)
        {
            if ((role != null && role.Equals('2')) || (currentUser == userToBeUpdated)) return true; 
            return false;
            
        }
        public bool IsTokenExpired(DateTime tokenGenerationTime, DateTime? passwordChangedAt)
        {
            return tokenGenerationTime.AddSeconds(1) < passwordChangedAt;
        }
    }
}

