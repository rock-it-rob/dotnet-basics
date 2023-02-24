using FileWriters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using RecordLayouts;

public class FixedLengthWriterTest
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

    private FixedLengthWriter<FixedLayout> createFixedLengthWriter() =>
        new FixedLengthWriter<FixedLayout>(loggerFactory!.CreateLogger<FixedLengthWriter<FixedLayout>>());

    [Test]
    public void TestWriteToString()
    {
        var layout = new FixedLayout() { Id = 0, Name = "name", Amount = 00.000m, WhenCreated = new DateTime(1, 1, 1) };
        var layouts = new List<FixedLayout>();
        layouts.Add(layout);
        var stringWriter = new StringWriter();
        const string expected = " 0name   0.0000001-01-01 \n";
        var fixedLengthWriter = createFixedLengthWriter();

        fixedLengthWriter.writeToStream(stringWriter, layouts);

        Assert.That(stringWriter.ToString(), Is.EqualTo(expected));
    }
}