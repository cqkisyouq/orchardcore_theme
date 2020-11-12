using FlyingRat.Module.Account.Indexs;
using OrchardCore.Data.Migration;
using System;
using YesSql.Sql;
namespace FlyingRat.Module.Account
{
    public class Migrations : DataMigration
    {
        public int Create()
        {
            SchemaBuilder.CreateMapIndexTable<userProfile>(table =>
            {
                table.Column<string>(nameof(userProfile.NickName), x => x.WithLength(20))
                    .Column<string>(nameof(userProfile.Photo), x => x.WithLength(150))
                    .Column<string>(nameof(userProfile.Birthday), x => x.WithLength(8))
                    .Column<DateTime>(nameof(userProfile.CreatedUtc), x => x.Nullable())
                    .Column<DateTime>(nameof(userProfile.ModifiedUtc), x => x.Nullable());
            });

            SchemaBuilder.AlterTable(nameof(userProfile), table =>
            {
                table.CreateIndex("IDX_UserProfileIndex_NickName", "NickName");
            });

            return 1;
        }
    }
}
