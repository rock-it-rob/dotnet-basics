using RecordLayouts;

namespace FileWriters;

interface IFixedLengthWriter
{
    public void writeToFile(string filename, IEnumerable<FixedLayout> records);
}