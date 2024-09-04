using System.ComponentModel.DataAnnotations;

namespace GlassLewis_StockExchange.Models;

public class Company
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(10)]   
    public required string Ticker { get; set; }

    [MaxLength(100)]
    public required string Exchange { get; set; }

    [RegularExpression(RegularExpressionConstants.IsinPatternFixedLength, ErrorMessage = MessageConstants.InvalidIsinFormat)]
    public required string Isin { get; set; }

    [Url, MaxLength(500)]
    public string? WebsiteUrl { get; set; }
}
