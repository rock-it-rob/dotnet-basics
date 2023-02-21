using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

// Create a Host for DI.
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

// Test the fixed-length reader.
host.Services
    .GetRequiredService<IFixedLengthReader>()
    .readAndPrint(@"sample-files/plain-text.txt");

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IFixedLengthReader, FixedLengthReader>();
}