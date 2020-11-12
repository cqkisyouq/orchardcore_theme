using FlyingRat.Module.Account.Services;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlyingRat.Module.Account.Handlers
{
    public class BraksnUpdateContentsHandler : UpdateContentsHandler
    {
        private readonly IAccountProfileService _extensionService;
        private readonly IHttpContextAccessor _httpContentAccessor;
        public BraksnUpdateContentsHandler(
            IClock clock,
            IHttpContextAccessor httpContextAccessor,
            IAccountProfileService extensionService
            )
            : base(clock, httpContextAccessor)
        {
            _extensionService = extensionService;
            _httpContentAccessor = httpContextAccessor;
        }

        public override Task CreatedAsync(CreateContentContext context)
        {
            base.CreatedAsync(context).GetAwaiter().GetResult();
            var httpContent = _httpContentAccessor.HttpContext;

            if (httpContent?.User?.Identity?.IsAuthenticated ?? false)
            {
                context.ContentItem.Author = _extensionService.FindUser(httpContent.User.Identity.Name)
                    ?.NickName ?? context.ContentItem.Author;
            }
            return Task.CompletedTask;
        }

        public override Task UpdatedAsync(UpdateContentContext context)
        {
            base.UpdatedAsync(context).GetAwaiter().GetResult();
            var httpContent = _httpContentAccessor.HttpContext;

            if (httpContent?.User?.Identity?.IsAuthenticated ?? false)
            {
                context.ContentItem.Author = _extensionService.FindUser(httpContent.User.Identity.Name)
                    ?.NickName ?? context.ContentItem.Author;
            }

            return Task.CompletedTask;
        }
    }
}
