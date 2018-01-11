using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTask.Domain.DbEntities;


namespace TestTask.Domain.DbServices.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _appDbContext;

        public DepartmentService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IList<Department> GetDepartments()
        {
            return _appDbContext.Departments.ToList();
        }

        public IList<Department> GetDepartments(int count)
        {
            throw new NotImplementedException();
        }

        public bool AddDepartment(Department department)
        {
            try
            {
                _appDbContext.Departments.Add(department);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
