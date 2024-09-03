namespace GlassLewis_StockExchange.Models;

public static class JwtAuthConstants
{
    public const string Issuer = "Jwt:Issuer";
    public const string Audience = "Jwt:Audience";
    public const string Key = "Jwt:Key";
    public const string Expiry = "Jwt:ExpiryMinutes";
    public const string Authorization = "Authorization";
    public const string Description = "Description";
    public const string BearerName = "Bearer";
}

public static class MessageConstants
{
    public const string DuplicateIsin = "ISIN value must be unique.";
    public const string InvalidIsinFormat = "Invalid ISIN format.";
    public const string RegistrationSuccessful = "User registered successfully.";
    public const string LoginSuccessful = "User logged in successfully.";
}

public static class RegularExpressionConstants
{
    public const string IsinPatternFixedLength = "^[A-Z]{2}[0-9A-Z]{10}$";
    public const string IsinPattern = "^[A-Za-z]{2}$";
}

public static class DbConnectionConstants
{
    public const string ConnectionString = "DefaultConnection";
}
