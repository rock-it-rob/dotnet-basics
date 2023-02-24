using FileHelpers;
using Microsoft.Extensions.Logging;
using RecordLayouts;

namespace FileReaders;

public class FixedLengthReader<T> : IFixedLengthReader<T>
    where T : class
{
    private readonly ILogger<FixedLengthReader<T>> log;

    public FixedLengthReader(ILogger<FixedLengthReader<T>> log) =>
        this.log = log;

    public event Action<T>? OnRead;

    public void readAndPrint(TextReader reader)
    {
        log.LogInformation("Reading");

        var engine = new FileHelperAsyncEngine<T>();

        using var _ = engine.BeginReadStream(reader);

        foreach (T record in engine)
        {
            OnRead?.Invoke(record);
            log.LogInformation($"{record}");
        }
    }
}