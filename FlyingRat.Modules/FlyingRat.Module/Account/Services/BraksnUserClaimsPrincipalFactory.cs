using FlyingRat.Module.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Security;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlyingRat.Module.Account.Services
{
    public class BraksnUserClaimsPrincipalFactory : DefaultUserClaimsPrincipalFactory
    {
        public BraksnUserClaimsPrincipalFactory(
            UserManager<IUser> userManager,
            RoleManager<IRole> roleManager,
            IOptions<IdentityOptions> identityOptions)
            : base(userManager, roleManager, identityOptions)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IUser user)
        {
            var claims =await base.GenerateClaimsAsync(user);
            var u = user as User;
            var userInfo = u.As<UserProfile>();
            if (!string.IsNullOrEmpty(userInfo.NickName))
            {
                claims.AddClaim(new Claim("nickname", userInfo.NickName));
            }
            return claims;
        }
    }
}
