using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Types
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
