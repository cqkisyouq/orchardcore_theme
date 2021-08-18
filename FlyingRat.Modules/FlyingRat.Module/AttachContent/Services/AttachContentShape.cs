using Fluid.Values;
using FlyingRat.Module.AttachContent.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Implementation;
using OrchardCore.Liquid;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FlyingRat.Module.AttachContent.Services
{
    public class AttachContentShape : IShapeTableProvider
    {
        private readonly HtmlEncoder _htmlEncoder;
        public AttachContentShape(
            HtmlEncoder htmlEncoder
            )
        {
            _htmlEncoder = htmlEncoder;
        }
        private async Task BuildViewModelAsync(ShapeDisplayContext shapeDisplayContext)
        {
            var model = shapeDisplayContext.Shape as AttachContentPartViewModel;
            var liquidTemplateManager = shapeDisplayContext.ServiceProvider.GetRequiredService<ILiquidTemplateManager>();

            model.Html = await liquidTemplateManager.RenderStringAsync(model.AttachContentPart.AttachContent, _htmlEncoder, shapeDisplayContext.DisplayContext.Value,
                new Dictionary<string, FluidValue>() { ["ContentItem"]=new ObjectValue(model.ContentItem) });
        }
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("AttachContentPart").OnProcessing(BuildViewModelAsync);
            builder.Describe("AttachContentPart_Summary").OnProcessing(BuildViewModelAsync);
        }
    }
}
