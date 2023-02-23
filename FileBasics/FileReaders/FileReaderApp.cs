using Microsoft.Extensions.Logging;

namespace FileReaders;

public class FileReaderApp
{
    private readonly IFixedLengthReader fixedLengthReader;
    private readonly ILogger<FileReaderApp> log;

    public FileReaderApp(IFixedLengthReader fixedLengthReader, ILogger<FileReaderApp> log) =>
        (this.fixedLengthReader, this.log) = (fixedLengthReader, log);

    public void Execute()
    {
        log.LogInformation("Testing fixed-length reader");

        // Test the fixed-length reader.
        var stream = new StreamReader(@"sample-files/plain-text.txt");
        fixedLengthReader.readAndPrint(stream);
    }
}