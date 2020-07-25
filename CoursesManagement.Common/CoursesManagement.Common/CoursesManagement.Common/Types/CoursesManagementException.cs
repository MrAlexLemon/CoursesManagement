using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Types
{
    public class CoursesManagementException : Exception
    {
        public string Code { get; }

        public CoursesManagementException()
        {

        }

        public CoursesManagementException(string code)
        {
            Code = code;
        }

        public CoursesManagementException(string message, params object[] args)
            : this(string.Empty, message, args)
        {

        }

        public CoursesManagementException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {

        }

        public CoursesManagementException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {

        }

        public CoursesManagementException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
