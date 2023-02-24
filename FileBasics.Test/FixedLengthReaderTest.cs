using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using FileReaders;

namespace FileBasics.Test;

public class FixedLengthReaderTest
{
    private ILoggerFactory? loggerFactory;

    [SetUp]
    public void SetUp()
    {
        loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(opts =>
            {
                opts.ColorBehavior = LoggerColorBehavior.Disabled;
            });
        });
    }

    [TearDown]
    public void TearDown()
    {
        if (loggerFactory is not null)
            loggerFactory.Dispose();
    }

    private FixedLengthReader createFixedLengthReader() =>
        new FixedLengthReader(loggerFactory!.CreateLogger<FixedLengthReader>());

    [TestCase("IdName  AmountWhenCreated\n0 name  00.0000001-01-01 ")]
    public void TestReadDataMatches(string record)
    {
        var fixedLengthReader = createFixedLengthReader();
        var reader = new StringReader(record);

        fixedLengthReader.readAndPrint(reader);
    }
}