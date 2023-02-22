using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace FileReaders;

class FileReaderApp : BackgroundService
{
    private readonly IHostApplicationLifetime applicationLifetime;
    private readonly IFixedLengthReader fixedLengthReader;
    private readonly ILogger<FileReaderApp> log;

    public FileReaderApp(IFixedLengthReader fixedLengthReader, ILogger<FileReaderApp> log, IHostApplicationLifetime applicationLifetime) =>
        (this.fixedLengthReader, this.log, this.applicationLifetime) = (fixedLengthReader, log, applicationLifetime);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        log.LogInformation("Testing fixed-length reader");

        // Test the fixed-length reader.
        await Task.Run(() => fixedLengthReader
            .readAndPrint(@"sample-files/plain-text.txt"));

        // Stop when finished.
        applicationLifetime.StopApplication();
    }

}