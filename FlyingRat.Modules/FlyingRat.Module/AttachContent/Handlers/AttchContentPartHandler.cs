using FlyingRat.Module.AttachContent.Models;
using FlyingRat.Module.AttachContent.ViewModel;
using Microsoft.AspNetCore.Html;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Models;
using OrchardCore.Liquid;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FlyingRat.Module.AttachContent.Handlers
{
    public class AttchContentPartHandler : ContentPartHandler<AttachContentPart>
    {
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly HtmlEncoder _htmlEncoder;
        private HtmlString _bodyAspect;
        public AttchContentPartHandler(ILiquidTemplateManager liquidTemplateManager, HtmlEncoder htmlEncoder)
        {
            _liquidTemplateManager = liquidTemplateManager;
            _htmlEncoder = htmlEncoder;
        }
        public override Task GetContentItemAspectAsync(ContentItemAspectContext context, AttachContentPart part)
        {
            return context.ForAsync<BodyAspect>(async bodyAspect =>
            {
                if (_bodyAspect != null)
                {
                    bodyAspect.Body = _bodyAspect;
                    return;
                }

                try
                {
                    var model = new AttachContentPartViewModel()
                    {
                        AttachContentPart = part,
                        ContentItem = part.ContentItem
                    };

                    var result = await _liquidTemplateManager.RenderAsync(part.AttachContent, _htmlEncoder, model,
                        scope => scope.SetValue("ContentItem", model.ContentItem));

                    bodyAspect.Body = _bodyAspect = new HtmlString(result);
                }
                catch
                {
                    bodyAspect.Body = HtmlString.Empty;
                }
            });
        }
    }
}
