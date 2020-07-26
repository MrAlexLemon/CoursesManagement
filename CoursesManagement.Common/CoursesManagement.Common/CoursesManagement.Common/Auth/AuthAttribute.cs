using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Auth
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string scheme, string policy = "") : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}
