using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace FileReaders;

class FileReaderApp
{
    private readonly IFixedLengthReader fixedLengthReader;
    private readonly ILogger<FileReaderApp> log;

    public FileReaderApp(IFixedLengthReader fixedLengthReader, ILogger<FileReaderApp> log) =>
        (this.fixedLengthReader, this.log) = (fixedLengthReader, log);

    public void Execute()
    {
        log.LogInformation("Testing fixed-length reader");

        // Test the fixed-length reader.
        fixedLengthReader.readAndPrint(@"sample-files/plain-text.txt");
    }
}