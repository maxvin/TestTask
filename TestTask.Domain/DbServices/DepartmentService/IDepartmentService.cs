﻿using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain.DbServices.DepartmentService
{
    public interface IDepartmentService
    {
        IList<Department> GetDepartments();

        IList<Department> GetDepartments(int count);

    }
}
