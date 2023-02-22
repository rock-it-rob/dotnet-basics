using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using RecordLayouts;

namespace FileWriters;

class FileWriterApp
{
    private readonly ILogger<FileWriterApp> log;
    private readonly IFixedLengthWriter writer;

    public FileWriterApp(ILogger<FileWriterApp> log, IFixedLengthWriter writer) =>
        (this.log, this.writer) = (log, writer);

    public void Execute()
    {
        log.LogInformation("Testing fixed-length writer");

        // Test the fixed-length writer.
        writer.writeToFile(@"fixed-file-out.txt", layouts());
    }

    private IEnumerable<FixedLayout> layouts()
    {
        yield return new FixedLayout() { Id = 1, Name = "layout1", Amount = 33.13m, WhenCreated = DateTime.Today };
        yield return new FixedLayout() { Id = 4, Name = "Layout 4", Amount = 2.01m, WhenCreated = new DateTime(2023, 1, 2) };
    }
}