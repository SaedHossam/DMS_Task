using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace DAL
{
    public class HttpUnitOfWork : UnitOfWork
    {
        public HttpUnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpAccessor) : base(context)
        {
            context.CurrentUserId = httpAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; }
    }
}
