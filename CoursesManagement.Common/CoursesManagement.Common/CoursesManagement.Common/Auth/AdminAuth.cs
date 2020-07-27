using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Auth
{
    public class AdminAuth : JwtAuthAttribute
    {
        public AdminAuth() : base("admin")
        {
        }
    }
}
