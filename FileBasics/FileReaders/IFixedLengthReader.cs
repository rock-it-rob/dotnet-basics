namespace FileBasics.FileReaders;

/**
 * <summary>
 * Defines methods for reading objects of type T.
 * </summary>
 */
public interface IFixedLengthReader<T>
    where T : class
{
    public void readAndPrint(TextReader reader);

    public event Action<T>? OnRead;
}