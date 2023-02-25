using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using FileBasics.FileReaders;
using FileBasics.RecordLayouts;

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

    private FixedLengthReader<FixedLayout> createFixedLengthReader() =>
        new FixedLengthReader<FixedLayout>(loggerFactory!.CreateLogger<FixedLengthReader<FixedLayout>>());

    [TestCase("IdName  AmountWhenCreated\n0 name  00.0000001-01-01 ")]
    public void TestReadDataMatches(string record)
    {
        var fixedLengthReader = createFixedLengthReader();
        var reader = new StringReader(record);

        fixedLengthReader.readAndPrint(reader);
    }

    [TestCase("IdName  AmountWhenCreated\n0 name  00.0000001-01-01 ")]
    public void TestEventOnRead(string record)
    {
        var fixedLengthReader = createFixedLengthReader();
        var reader = new StringReader(record);

        var layout = new FixedLayout();

        fixedLengthReader.OnRead += rec =>
            layout = rec;

        fixedLengthReader.readAndPrint(reader);

        Assert.That(layout.Id, Is.EqualTo(0));
        Assert.That(layout.Name, Is.EqualTo("name"));
        Assert.That(layout.Amount, Is.EqualTo(0));
        Assert.That(layout.WhenCreated, Is.EqualTo(new DateTime(1, 1, 1)));
    }
}