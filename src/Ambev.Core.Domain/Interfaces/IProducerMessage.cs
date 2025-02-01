using Ambev.Core.Domain.Common;
using System.Linq.Expressions;

namespace Ambev.Core.Domain.Interfaces;

public interface IProducerMessage
{
    Task SendMessage<T>(T message, string routingKey);
}