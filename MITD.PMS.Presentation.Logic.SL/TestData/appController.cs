using System;
using System.Collections.Generic;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        public static List<CustomFieldEntity> CustomFieldEntityList = new List<CustomFieldEntity>
        {

            new CustomFieldEntity {Id = 1, Title = "Job"},
            new CustomFieldEntity {Id = 2, Title = "JobIndex"},
            new CustomFieldEntity {Id = 4, Title = "Employee"},

        };

    }
}
