using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
