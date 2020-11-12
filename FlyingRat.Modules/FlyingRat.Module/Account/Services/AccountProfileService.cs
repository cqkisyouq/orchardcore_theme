using FlyingRat.Module.Account.Indexs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using OrchardCore.ContentManagement;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YesSql;
using OrchardCore.Entities;
using Dapper;
using FlyingRat.Module.Account.Models;
using OrchardCore.ContentManagement.Records;
using System.Runtime.CompilerServices;
using OrchardCore.Modules;
using System.Linq;
using OrchardCore.Users.Indexes;

namespace FlyingRat.Module.Account.Services
{
    public class AccountProfileService : IAccountProfileService
    {
        private readonly ISession _session;
        private readonly IMemoryCache _memoryCache;
        public readonly static string cache_prefix = "userprofile";
        public AccountProfileService(
            IMemoryCache memoryCache,
            ISession session
            )
        {
            _session = session;
            _memoryCache = memoryCache;
        }

        public UserProfile CacheUserExtension(string key, UserProfile profile)
        {
            if (profile == null) return profile;
            profile = _memoryCache.Set<UserProfile>(CreateCacheKey(key), profile);
            return profile;
        }

        public UserProfile FindUser(string userName)
        {
            if (!_memoryCache.TryGetValue<UserProfile>(CreateCacheKey(userName), out UserProfile user))
            {
                var userItem = _session.Query<User, UserIndex>(x => x.NormalizedUserName == userName)
                        .FirstOrDefaultAsync()
                        .GetAwaiter().GetResult();
                user = CacheUserExtension(userName, userItem?.As<UserProfile>());
            }

            return user;
        }

        public ValueTask<int> UpdateAuthorOf(User user)
        {
            int count = 0;
            var extension = user?.As<UserProfile>();
            var items = _session.Query<ContentItem, ContentItemIndex>()
                     .Where(x => x.Owner == user.UserName && x.Author != extension.NickName)
                     .ListAsync().GetAwaiter().GetResult();
            if (items?.Any() ?? false)
            {
                count = items.Count();
                items.ForEachAsync(item =>
                {
                    item.Author = extension.NickName;
                    _session.Save(item);
                    return Task.CompletedTask;
                }).GetAwaiter().GetResult();
                CacheUserExtension(user.UserName, extension);
            }
            return new ValueTask<int>(count);
        }

        public async ValueTask<bool> ValidationNickName(string nickName, string passName)
        {
            if (string.IsNullOrWhiteSpace(nickName)) return await new ValueTask<bool>(true);

            var query = _session.Query<User, userProfile>(x => x.NickName == nickName);

            if (string.IsNullOrWhiteSpace(passName) == false)
            {
                query.Where(x => x.NickName != passName);
            }

            var count = await query.CountAsync();

            return await new ValueTask<bool>(count == 0);
        }


        private string CreateCacheKey(string name)
        {
            return $"{cache_prefix}_{name?.ToLower()}";
        }
    }
}
