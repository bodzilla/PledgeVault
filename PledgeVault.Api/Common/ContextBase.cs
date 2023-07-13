using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Web;
using PledgeVault.Persistence;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PledgeVault.Api.Common;

internal sealed class ContextBase : IContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PledgeVaultContext _dbContext;

    public ContextBase(IHttpContextAccessor httpContextAccessor, PledgeVaultContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<int?> GetCurrentUserId()
    {
        HttpContext httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User.Identity is not ClaimsIdentity user) return null;

        // Extract the user's ID from the claims.
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null) return null;
        if (!int.TryParse(userIdClaim.Value, out var userId)) return null;

        return await _dbContext.Users.AsNoTracking().Where(x => x.Id == userId).Select(x => x.Id).SingleOrDefaultAsync();
    }
}