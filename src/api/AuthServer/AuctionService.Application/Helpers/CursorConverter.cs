namespace AuctionService.Application.Helpers;

public static class CursorConverter
{
    public static DateTimeOffset DecodeAuctionCursor(string encodedCursor)
    {
        var bytes = Convert.FromBase64String(encodedCursor);
        var milliseconds = BitConverter.ToInt64(bytes, startIndex: 0);
        
        return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
    }
    
    public static string EncodeAuctionCursor(DateTimeOffset cursor)
    {
        var bytes = BitConverter.GetBytes(cursor.Millisecond);
        
        return Convert.ToBase64String(bytes);
    }
}