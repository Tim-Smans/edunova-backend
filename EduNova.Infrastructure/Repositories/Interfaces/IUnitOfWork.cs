using EduNova.Infrastructure.Entities;
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
        IGenericRepo<Tenant> TenantRepo { get; }
        IGenericRepo<HouseStyle> HouseStyleRepo { get; }

        public void SaveChanges();
    }
}
