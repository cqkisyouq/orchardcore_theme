using FlyingRat.Module.Setting.Models;
using FlyingRat.Module.Setting.ViewModel;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlyingRat.Module.Setting.Drivers
{
    public class PublishSettingDisplayDriver: SectionDisplayDriver<ISite, PublishSetting>
    {
        public static readonly string groupId = "publishSetting";
        public override IDisplayResult Edit(PublishSetting section)
        {
            return Initialize<PublishSettingViewModel>("PublishSetting_Edit", model =>
            {
                model.ListContentItemIds = section.ListContentItemIds;
            }).Location("Content:2").OnGroup(groupId);
        }
        public override async Task<IDisplayResult> UpdateAsync(ISite model, PublishSetting section, IUpdateModel updater, BuildEditorContext context)
        {
            if(groupId== context.GroupId)
            {
                if (!await updater.TryUpdateModelAsync(section,Prefix))
                {
                    return null;
                }
            }
            return await EditAsync(section, context);
        }
    }
}
