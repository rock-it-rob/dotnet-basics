using System.Collections.Generic;
using RecordLayouts;

namespace FileWriters;

public interface IFixedLengthWriter
{
    public void writeToFile(string filename, IEnumerable<FixedLayout> records);
}