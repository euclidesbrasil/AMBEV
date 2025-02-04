
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Event
{
    public class SaleModifiedEvent : BaseEvent<Sale>
    {
        public SaleModifiedEvent(Sale sale, string message) : base(sale, message)
        {
        }
    }
}
