using System;
using FileHelpers;
using Microsoft.Extensions.Logging;
using RecordLayouts;

namespace FileReaders;

public class FixedLengthReader : IFixedLengthReader
{
    private readonly ILogger<FixedLengthReader> log;

    public FixedLengthReader(ILogger<FixedLengthReader> log) =>
        this.log = log;

    public void readAndPrint(TextReader reader)
    {
        log.LogInformation("Reading");

        var engine = new FileHelperAsyncEngine<FixedLayout>();

        using var _ = engine.BeginReadStream(reader);

        foreach (var record in engine)
            log.LogInformation($"{record}");
    }
}