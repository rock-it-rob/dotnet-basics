using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FileReaders;

// Create a Host for DI.
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IFixedLengthReader, FixedLengthReader>();
    services.AddHostedService<FileReaderApp>();
}

host.Run();