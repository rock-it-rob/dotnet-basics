using Microsoft.Extensions.Logging;
using FileHelpers;
using RecordLayouts;

namespace FileWriters;

// Will be registered in the DI container with a scoped lifetime. So each
// instance can only write to exactly one file.
class FixedLengthWriter : IFixedLengthWriter
{
    private readonly ILogger<FixedLengthWriter> log;

    public FixedLengthWriter(ILogger<FixedLengthWriter> log) =>
        this.log = log;

    public void writeToFile(string filename, IEnumerable<FixedLayout> records)
    {
        var engine = new FileHelperAsyncEngine<FixedLayout>();

        using var _ = engine.BeginWriteFile(filename);

        foreach (FixedLayout layout in records)
            engine.WriteNext(layout);
    }
}