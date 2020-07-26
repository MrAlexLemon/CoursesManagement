using CoursesManagement.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Messages.Commands
{
    public class ChangePassword : ICommand
    {
        public Guid UserId { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }

        public ChangePassword(Guid userId,
            string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}
