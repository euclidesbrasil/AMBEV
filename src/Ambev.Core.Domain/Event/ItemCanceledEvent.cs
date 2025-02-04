
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Event
{
    public class ItemCanceledEvent : BaseEvent<SaleItem>
    {
        public ItemCanceledEvent(SaleItem data, string message) : base(data, message)
        {
        }
    }
}
