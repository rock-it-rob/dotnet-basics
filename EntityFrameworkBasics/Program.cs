using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

void ConfigureServices(IServiceCollection services)
{
}