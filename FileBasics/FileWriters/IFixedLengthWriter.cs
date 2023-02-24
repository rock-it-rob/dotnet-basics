namespace FileWriters;

public interface IFixedLengthWriter<T>
    where T : class
{
    public void writeToStream(TextWriter writer, IEnumerable<T> records);
}