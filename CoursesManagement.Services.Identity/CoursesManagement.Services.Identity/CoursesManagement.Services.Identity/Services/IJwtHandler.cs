using CoursesManagement.Common.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Services
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
