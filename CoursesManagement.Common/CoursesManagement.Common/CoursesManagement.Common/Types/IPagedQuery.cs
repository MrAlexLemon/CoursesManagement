using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.Types
{
    public interface IPagedQuery : IQuery
    {
        int Page { get; }
        int Results { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}
