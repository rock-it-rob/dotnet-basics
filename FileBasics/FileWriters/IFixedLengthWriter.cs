using System.Collections.Generic;
using RecordLayouts;

namespace FileWriters;

public interface IFixedLengthWriter
{
    public void writeToStream(TextWriter writer, IEnumerable<FixedLayout> records);
}