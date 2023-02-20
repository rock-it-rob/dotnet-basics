class FixedLengthReader
{
    internal void readAndPrint(string filename)
    {
        Console.WriteLine($"Reading filename {filename}");

        using var reader = new StreamReader(filename);

        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}