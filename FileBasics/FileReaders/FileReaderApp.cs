using Microsoft.Extensions.Logging;
using RecordLayouts;

namespace FileReaders;

public class FileReaderApp
{
    private readonly IFixedLengthReader<FixedLayout> fixedLengthReader;
    private readonly ILogger<FileReaderApp> log;

    public FileReaderApp(IFixedLengthReader<FixedLayout> fixedLengthReader, ILogger<FileReaderApp> log) =>
        (this.fixedLengthReader, this.log) = (fixedLengthReader, log);

    public void Execute()
    {
        log.LogInformation("Testing fixed-length reader");

        fixedLengthReader.OnRead += (layout) => Console.WriteLine($"Read {layout}");
        
        // Test the fixed-length reader.
        var stream = new StreamReader(@"sample-files/plain-text.txt");
        fixedLengthReader.readAndPrint(stream);
    }
}