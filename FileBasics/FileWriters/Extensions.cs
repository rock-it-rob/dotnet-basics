using System.Collections.Generic;
using RecordLayouts;

namespace FileWriters;

public static class Extensions
{
    public const string DefaultFilename = @"fixed-file-out.txt";

    public static void writeToFile(this IFixedLengthWriter writer, IEnumerable<FixedLayout> layouts)
    {
        writer.writeToFile(DefaultFilename, layouts);
    }
}