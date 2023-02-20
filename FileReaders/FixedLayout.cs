using FileHelpers;

[FixedLengthRecord]
record FixedLayout
{
    const string DateFormat = "yyyy-MM-dd";

    public int? Id { get; init; }
    public string? Name { get; init; }
    public decimal? Amount { get; init; }
    public DateTime? WhenCreated { get; init; }
}