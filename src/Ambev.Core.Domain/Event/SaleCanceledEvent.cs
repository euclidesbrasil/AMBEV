
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Event
{
    public class SaleCanceledEvent : BaseEvent<Sale>
    {
        public SaleCanceledEvent(Sale data, string message) : base(data, message)
        {
        }
    }
}
