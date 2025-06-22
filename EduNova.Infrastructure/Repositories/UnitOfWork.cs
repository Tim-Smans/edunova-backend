using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Repositories.Interfaces;

namespace EduNova.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NovaDBContext _context;

        private IGenericRepo<CustomUser> userRepo;
        private ITenantRepo tenantRepo;
        private IGenericRepo<HouseStyle> houseStyleRepo;

        public UnitOfWork(NovaDBContext context)
        {
            _context = context;
        }

        public IGenericRepo<CustomUser> UserRepo
        {
            get
            {
                if (this.userRepo == null)
                    this.userRepo
                        = new GenericRepo<CustomUser>(_context);
                return userRepo;
            }
        }
        public ITenantRepo TenantRepo
        {
            get
            {
                if (this.tenantRepo == null)
                    this.tenantRepo
                        = new TenantRepo(_context);
                return tenantRepo;
            }
        }
        public IGenericRepo<HouseStyle> HouseStyleRepo
        {
            get
            {
                if (this.houseStyleRepo == null)
                    this.houseStyleRepo
                        = new GenericRepo<HouseStyle>(_context);
                return houseStyleRepo;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
