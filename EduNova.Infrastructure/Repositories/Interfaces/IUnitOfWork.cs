using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Tenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepo<CustomUser> UserRepo { get; }
        ITenantRepo TenantRepo { get; }
        IGenericRepo<HouseStyle> HouseStyleRepo { get; }
        IGenericRepo<Course> CourseRepo { get; }

        public void SaveChanges();
    }
}
