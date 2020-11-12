using OrchardCore.Media.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FlyingRat.Module.Services
{
    public class BraksnMediaCreateEvent : IMediaCreatingEventHandler
    {
        public async Task<Stream> MediaCreatingAsync(MediaCreatingContext context, Stream creatingStream)
        {
            var newStream = new MemoryStream();
            await creatingStream.CopyToAsync(newStream);
            newStream.Position = 0;
            return creatingStream;
        }
    }
}
