using FlyingRat.Module.Atlas.Models;
using FlyingRat.Module.Atlas.ViewModel;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Media;
using OrchardCore.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingRat.Module.Atlas.Drivers
{
    public class AtlasPartDisplayDriver : ContentPartDisplayDriver<AtlasPart>
    {
        private readonly IMediaFileStore _mediaFileStore;
        public AtlasPartDisplayDriver(
            IMediaFileStore mediaFileStore
            )
        {
            _mediaFileStore = mediaFileStore;
        }
        public override IDisplayResult Display(AtlasPart part, BuildPartDisplayContext context)
        {
            return Initialize<AtlasPartViewModel>("AtlasPart", async model =>
             {
                 var pager = new PagerParameters() { Page = 1, PageSize = part.PageSize };
                 await context.Updater.TryUpdateModelAsync(pager);
                 var count = (pager.Page - 1) * pager.PageSize;
                 int totalCount = 0;
                 if (await _mediaFileStore.GetDirectoryInfoAsync(part.Path) != null)
                 {
                     var files = _mediaFileStore.GetDirectoryContentAsync(part.Path);
                     totalCount =await files.Where(x => !x.IsDirectory).CountAsync();
                     model.Medias =await files.Reverse()
                     .Where(x => !x.IsDirectory)
                     .Skip(Math.Max(0, count.Value))
                     .Take(pager.PageSize.Value)
                     .Select(x => new Media() { Name = x.Name, Path = x.Path })
                     .ToListAsync();
                 }
                 model.Cover = part.Cover;
                 model.Path = part.Path;
                 model.Name = part.Name;
                 model.PageSize = part.PageSize;
                 model.UseAtlas = part.UseAtlas;
                 model.Description = part.Description;
                 model.ContentItem = part.ContentItem;
                 model.Pager = (await context.New.Pager(pager)).TotalItemCount(totalCount);
             }).Location("Detail", "Content:2");
        }

        public override IDisplayResult Edit(AtlasPart part)
        {
            return Initialize<AtalsPartEditViewModel>("AtlasPart_Edit", model =>
            {
                model.Cover = part.Cover;
                model.Path = part.Path;
                model.Name = part.Name;
                model.Description = part.Description;
                model.PageSize = part.PageSize;
                model.UseAtlas = part.UseAtlas;
            });
        }
        public override async Task<IDisplayResult> UpdateAsync(AtlasPart part, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(part, Prefix);
            return Edit(part);
        }
    }
}
