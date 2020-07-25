using CoursesManagement.Common.Types;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Domain
{
    public class User : IIdentifiable
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        protected User()
        {

        }

        public User(Guid id, string email, string role)
        {
            if(!EmailRegex.IsMatch(email))
            {
                throw new CoursesManagementException(ExceptionCodes.InvalidEmail, $"Invalid email: '{email}'.");
            }

            if(Domain.Role.IsValid(role))
            {
                throw new CoursesManagementException(ExceptionCodes.InvalidRole, $"Invalid role: '{role}'.");
            }

            Id = id;
            Email = email.ToLowerInvariant();
            Role = role.ToLowerInvariant();
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordhasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new CoursesManagementException(ExceptionCodes.InvalidPassword, "Password can not be empty.");
            }

            PasswordHash = passwordhasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;

    }
}
