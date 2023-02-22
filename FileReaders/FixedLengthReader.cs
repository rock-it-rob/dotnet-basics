using FileHelpers;
using Microsoft.Extensions.Logging;

class FixedLengthReader : IFixedLengthReader
{
    private readonly ILogger<FixedLengthReader> log;

    public FixedLengthReader(ILogger<FixedLengthReader> log) =>
        this.log = log;

    public void readAndPrint(string filename)
    {
        Console.WriteLine($"Reading filename {filename}");

        var engine = new FileHelperAsyncEngine<FixedLayout>();

        using var _ = engine.BeginReadFile(filename);

        foreach (var record in engine)
            log.LogInformation($"{record}");
    }
}