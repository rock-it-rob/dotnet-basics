using FileBasics.RecordLayouts;
using Microsoft.Extensions.Logging;

namespace FileBasics.FileWriters;

public class FileWriterApp
{
    private readonly ILogger<FileWriterApp> log;
    private readonly IFixedLengthWriter<FixedLayout> writer;

    public FileWriterApp(ILogger<FileWriterApp> log, IFixedLengthWriter<FixedLayout> writer) =>
        (this.log, this.writer) = (log, writer);

    public void Execute()
    {
        log.LogInformation("Testing fixed-length writer");

        // Test the fixed-length writer.
        writer.writeToStream(layouts());
    }

    private IEnumerable<FixedLayout> layouts()
    {
        yield return new FixedLayout() { Id = 1, Name = "layout1", Amount = 33.13m, WhenCreated = DateTime.Today };
        yield return new FixedLayout() { Id = 4, Name = "Layout 4", Amount = 2.01m, WhenCreated = new DateTime(2023, 1, 2) };
    }
}