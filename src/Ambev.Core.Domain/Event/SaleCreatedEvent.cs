
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Event
{
    public class SaleCreatedEvent : BaseEvent<Sale>
    {
        public SaleCreatedEvent(Sale sale, string message) : base(sale, message)
        {
        }
    }
}
