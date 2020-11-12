using FlyingRat.Module.Account.Models;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using System.Threading.Tasks;

namespace FlyingRat.Module.Account.Services
{
    public interface IAccountProfileService
    {
        UserProfile CacheUserExtension(string key,UserProfile profile);
        UserProfile FindUser(string userName);
        ValueTask<int> UpdateAuthorOf(User user);
        ValueTask<bool> ValidationNickName(string nickName, string passName=null);
    }
}