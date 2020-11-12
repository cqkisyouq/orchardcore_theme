using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "FlyingRat Module",
    Category = "FlyingRat"
)]

[assembly: Feature(
    Id = "FlyingRat.Module",
    Name = "FlyingRat Module",
    Description = "FlyingRat Module",
    Category = "FlyingRat"
)]

[assembly: Feature(
    Id = "FlyingRat.AttachContent",
    Name = "AttachContent",
    Description = "The  module allows you to attach a content",
    Dependencies = new[]
    {
        "FlyingRat.Module"
    },
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "FlyingRat.ContentSetting",
    Name = "ContentSetting",
    Description = "The  module allows you to attach a content setting",
    Dependencies = new[]
    {
        "FlyingRat.Module"
    },
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "FlyingRat.Atlas",
    Name = "Atlas",
    Description = "The  module allows you to attach a atlas",
    Dependencies = new[]
    {
        "FlyingRat.Module"
    },
    Category = "Content Management"
)]