using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WZH.Common.Snowflake;

namespace WZH.Domain.Base
{
    public record BaseEntity : IEntity, IDomainEvents
    {
        public long Id { get; init; } = IdWorker.Instance.NextId();

        // [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        private List<INotification> domainEvents = new();

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void AddDomainEventIfAbsent(INotification eventItem)
        {
            if (!domainEvents.Contains(eventItem))
            {
                domainEvents.Add(eventItem);
            }
        }
        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return domainEvents;
        }
    }
}