using CoursesManagement.Common.Types;
using CoursesManagement.Services.People.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.People.Queries
{
    public class BrowsePersons : PagedQueryBase, IQuery<PagedResult<PersonDto>>
    {
    }
}
