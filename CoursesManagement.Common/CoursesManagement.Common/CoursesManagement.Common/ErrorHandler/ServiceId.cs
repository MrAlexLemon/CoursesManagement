using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesManagement.Common.ErrorHandler
{
    public class ServiceId : IServiceId
    {
        private static readonly string UniqueId = $"{Guid.NewGuid():N}";

        public string Id => UniqueId;
    }
}
