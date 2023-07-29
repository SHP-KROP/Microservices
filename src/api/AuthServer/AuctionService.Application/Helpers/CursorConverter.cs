using System.Text;

namespace AuctionService.Application.Helpers;

public static class CursorConverter
{
    public static DateTimeOffset? DecodeAuctionCursor(string encodedCursor)
    {
        if (string.IsNullOrWhiteSpace(encodedCursor))
        {
            return null;
        }
        
        var bytes = Convert.FromBase64String(encodedCursor);
        var dateTimeOffsetString = Encoding.UTF8.GetString(bytes);
        
        return DateTimeOffset.Parse(dateTimeOffsetString, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
    
    public static string EncodeAuctionCursor(DateTimeOffset cursor)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(cursor.ToString("o"));
        
        return Convert.ToBase64String(bytes);
    }
}