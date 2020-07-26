using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Commands
{
    public class RevokeAccessToken : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        public RevokeAccessToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
