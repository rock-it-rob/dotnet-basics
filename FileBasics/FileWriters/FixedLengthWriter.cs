using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using FileHelpers;
using RecordLayouts;

namespace FileWriters;

// Will be registered in the DI container with a scoped lifetime. So each
// instance can only write to exactly one file.
public class FixedLengthWriter<T> : IFixedLengthWriter<T>
    where T : class
{
    private readonly ILogger<FixedLengthWriter<T>> log;

    public FixedLengthWriter(ILogger<FixedLengthWriter<T>> log) =>
        this.log = log;

    public void writeToStream(TextWriter writer, IEnumerable<T> records)
    {
        var engine = new FileHelperAsyncEngine<T>();

        using var _ = engine.BeginWriteStream(writer);

        foreach (T rec in records)
            engine.WriteNext(rec);
    }
}