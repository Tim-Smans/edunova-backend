using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.MultiTenancy
{
    public interface ITenantProvider
    {
        Guid TenantId { get; }
    }
}
