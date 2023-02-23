using System;
using FileHelpers;
using System.Text.Json;

namespace RecordLayouts;

[FixedLengthRecord(FixedMode.AllowLessChars)]
[IgnoreFirst]
public record FixedLayout
{
    const string DateFormat = "yyyy-MM-dd";

    [FieldFixedLength(2)]
    [FieldTrim(TrimMode.Right)]
    [FieldConverter(ConverterKind.Int32)]
    public int? Id { get; set; }

    [FieldFixedLength(6)]
    [FieldTrim(TrimMode.Right)]
    public string? Name { get; set; }

    [FieldFixedLength(6)]
    [FieldTrim(TrimMode.Right)]
    [FieldConverter(ConverterKind.Decimal)]
    public decimal? Amount { get; set; }

    [FieldFixedLength(11)]
    [FieldTrim(TrimMode.Right)]
    [FieldConverter(ConverterKind.Date, DateFormat)]
    public DateTime? WhenCreated { get; set; }

    // The ToString method will print as json.
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}