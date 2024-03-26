using Tickets.Microservice.Entities;

namespace Tickets.Microservice.Interfaces.Publishers;

public interface IOutboxPublisher
{
    void PublishOutboxMessage(Outbox outbox);
}
