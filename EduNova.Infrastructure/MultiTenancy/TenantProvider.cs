using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EduNova.Infrastructure.MultiTenancy
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TenantProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid TenantId
        {
            get
            {
                var httpContext = _contextAccessor.HttpContext;

                // Geen HTTP-context = fallback (bijv. tijdens seeding/migrations)
                if (httpContext == null || httpContext.User?.Identity == null || !httpContext.User.Identity.IsAuthenticated)
                {
                    return Guid.Parse("11111111-1111-1111-1111-11111111111f");
                }

                var claim = httpContext.User.FindFirst("tenant_id");

                if (claim == null || !Guid.TryParse(claim.Value, out Guid tenantId))
                {
                    throw new UnauthorizedAccessException("TenantId claim is missing or invalid.");
                }

                return tenantId;
            }
        }

    }
}
