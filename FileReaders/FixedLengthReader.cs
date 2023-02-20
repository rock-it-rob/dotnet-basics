using FileHelpers;

class FixedLengthReader
{
    public void readAndPrint(string filename)
    {
        Console.WriteLine($"Reading filename {filename}");

        var engine = new FileHelperAsyncEngine<FixedLayout>();

        using (engine.BeginReadFile(filename))
        {
            foreach (var record in engine)
                Console.WriteLine(record);
        }
    }
}