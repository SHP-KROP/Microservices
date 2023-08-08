using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.Application.Models.AuctionItem
{
    internal class AuctionItemBidCreateModel
    {
        public Guid AuctionItemId { get; private set; }

        public Guid UserId { get; private set; }

        public decimal Amount { get; private set; }

        public DateTimeOffset Date { get; private set; } = DateTimeOffset.Now;

        public decimal ActualPrice { get; private set; }
    }
}
