using System.ComponentModel.DataAnnotations;

namespace GlassLewis_StockExchange.Models;

public class Company
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    [Required, MaxLength(10)]   
    public string? Ticker { get; set; }

    [Required, MaxLength(100)]
    public string? Exchange { get; set; }

    [Required, MaxLength(12)]
    [RegularExpression(RegularExpressionConstants.IsinPatternFixedLength, ErrorMessage = MessageConstants.InvalidIsinFormat)]
    public string? Isin { get; set; }

    [Url, MaxLength(500)]
    public string? WebsiteUrl { get; set; }
}
