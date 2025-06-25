using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Tenancy;
using EduNova.Infrastructure.Repositories.Interfaces;

namespace EduNova.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NovaDBContext _context;

        private IGenericRepo<CustomUser> userRepo;
        private ITenantRepo tenantRepo;
        private IGenericRepo<HouseStyle> houseStyleRepo;
        private IGenericRepo<Course> courseRepo;
        private IGenericRepo<CourseTag> courseTagRepo;
        private IGenericRepo<Tag> tagRepo;

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
        public IGenericRepo<Course> CourseRepo
        {
            get
            {
                if (this.courseRepo == null)
                    this.courseRepo
                        = new GenericRepo<Course>(_context);
                return courseRepo;
            }
        }
        public IGenericRepo<CourseTag> CourseTagRepo
        {
            get
            {
                if (this.courseTagRepo == null)
                    this.courseTagRepo
                        = new GenericRepo<CourseTag>(_context);
                return courseTagRepo;
            }
        }
        public IGenericRepo<Tag> TagRepo
        {
            get
            {
                if (this.tagRepo == null)
                    this.tagRepo
                        = new GenericRepo<Tag>(_context);
                return tagRepo;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
