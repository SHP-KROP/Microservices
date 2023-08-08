namespace AuctionService.Application.Events
{
    public class BidUpdatedEvent
    {
        public string AuctionItemId { get; set; }

        public decimal UpdatedPrice { get; set; }
    }
}
