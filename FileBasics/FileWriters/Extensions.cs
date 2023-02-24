using RecordLayouts;

namespace FileWriters;

public static class Extensions
{
    public const string DefaultFilename = @"fixed-file-out.txt";

    public static void writeToStream(this IFixedLengthWriter fixedLengthWriter, IEnumerable<FixedLayout> layouts)
    {
        var streamWriter = new StreamWriter(DefaultFilename);

        fixedLengthWriter.writeToStream(streamWriter, layouts);
    }
}