using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using videoApp.VideoChat;

await WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build()
    .RunAsync();