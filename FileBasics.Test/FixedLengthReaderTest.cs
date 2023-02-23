using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FileReaders;

namespace FileBasics.Test;

public class FixedLengthReaderTest
{
    private FixedLengthReader fixedLengthReader;

    public FixedLengthReaderTest()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<FixedLengthReader>()
            .AddLogging()
            .BuildServiceProvider();

        fixedLengthReader = serviceProvider.GetService<FixedLengthReader>()
            ?? throw new ArgumentNullException(nameof(fixedLengthReader));
    }

    [TestCase("IdName  AmountWhenCreated\n0 name  00.0000001-01-01 ")]
    public void TestReadDataMatches(string record)
    {
        var reader = new StringReader(record);

        fixedLengthReader.readAndPrint(reader);
    }
}