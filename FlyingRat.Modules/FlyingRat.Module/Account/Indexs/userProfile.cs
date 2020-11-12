using FlyingRat.Module.Account.Models;
using OrchardCore.Entities;
using OrchardCore.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using YesSql.Indexes;

namespace FlyingRat.Module.Account.Indexs
{
    public class userProfile:MapIndex
    {
        public string DocumentId { get; set; }
        public string NickName { get; set; }
        public string Photo { get; set; }
        public string Birthday { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; }
    }

    public class UserExtensionProvider : IndexProvider<User>
    {
        public override void Describe(DescribeContext<User> context)
        {
            context.For<userProfile>()
               .Map(x =>
               {
                   var extension = x.As<UserProfile>();
                   return new userProfile()
                   {
                      NickName=extension.NickName,
                      Birthday=extension.Birthday,
                      Photo=extension.Photo,
                      CreatedUtc=extension.CreatedUtc,
                      ModifiedUtc=extension.ModifiedUtc
                   };
               });
        }
    }
}
