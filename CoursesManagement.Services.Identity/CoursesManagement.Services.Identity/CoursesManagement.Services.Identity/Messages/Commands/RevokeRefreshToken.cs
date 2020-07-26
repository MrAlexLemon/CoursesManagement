using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Commands
{
    public class RevokeRefreshToken : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        public RevokeRefreshToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
