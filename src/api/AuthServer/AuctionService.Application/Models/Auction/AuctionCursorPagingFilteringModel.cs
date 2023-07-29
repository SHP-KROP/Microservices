using System.Linq.Expressions;
using AuctionService.Application.Helpers;

namespace AuctionService.Application.Models.Auction;

public sealed class AuctionCursorPagingFilteringModel
{
    private const int MaxPageSize = 100;
    private const int MinPageSize = 1;

    private AuctionCursorPagingFilteringModel(int PageSize,
        DateTimeOffset? Cursor,
        Expression<Func<Core.Entities.Auction, bool>> Filter = null)
    {
        this.PageSize = PageSize;
        this.Cursor = Cursor;
        this.Filter = Filter;
    }

    public int PageSize { get; }
    
    public DateTimeOffset? Cursor { get; }
    
    public Expression<Func<Core.Entities.Auction, bool>> Filter { get; }

    public static AuctionCursorPagingFilteringModel Create(int pageSize, string encodedCursor)
    {
        if (pageSize is < MinPageSize or > MaxPageSize)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        var cursor = CursorConverter.DecodeAuctionCursor(encodedCursor);

        return new(pageSize, cursor);
    }

    public void Deconstruct(out int PageSize, 
        out DateTimeOffset? Cursor, 
        out Expression<Func<Core.Entities.Auction, bool>> Filter)
    {
        PageSize = this.PageSize;
        Cursor = this.Cursor;
        Filter = this.Filter;
    }
}