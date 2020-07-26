using CoursesManagement.Common.Types;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Domain
{
    public class RefreshToken : IIdentifiable
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? RevokeAt { get; private set; }
        public bool Revoked => RevokeAt.HasValue;

        protected RefreshToken()
        {

        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            CreateAt = DateTime.UtcNow;
            Token = CreateToken(user, passwordHasher);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new CoursesManagementException(ExceptionCodes.RefreshTokenAlreadyRevoked, $"Refresh token '{Id}' was already revoked at '{RevokeAt}'.");
            }
            RevokeAt = DateTime.UtcNow;
        }
        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);

    }
}
