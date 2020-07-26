using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Commands
{
    public class RefreshAccessToken : ICommand
    {
        public string Token { get; }

        public RefreshAccessToken(string token)
        {
            Token = token;
        }
    }
}
