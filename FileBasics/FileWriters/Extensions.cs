
namespace FileBasics.FileWriters;

public static class Extensions
{
    public const string DefaultFilename = @"fixed-file-out.txt";

    public static void writeToStream<T>(this IFixedLengthWriter<T> fixedLengthWriter, IEnumerable<T> records)
        where T : class
    {
        var streamWriter = new StreamWriter(DefaultFilename);

        fixedLengthWriter.writeToStream(streamWriter, records);
    }
}