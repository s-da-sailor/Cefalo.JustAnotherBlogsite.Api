using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.JustAnotherBlogsite.Service.Utilities
{
    public static class AuthChecker
    {
        public static bool IsUserAuthorized(string? currentUser, string userToBeUpdated, string? role)
        {
            if ((role != null && role.Equals('2')) || (currentUser == userToBeUpdated)) return true; 
            return false;
            
        }
        public static bool IsTokenExpired(DateTime tokenGenerationTime, DateTime passwordChangedAt)
        {
            return tokenGenerationTime.AddSeconds(1) < passwordChangedAt;
        }
    }
}

