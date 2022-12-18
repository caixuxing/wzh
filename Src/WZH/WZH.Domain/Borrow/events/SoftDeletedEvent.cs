using MediatR;

namespace WZH.Domain.Borrow.events
{
    public record SoftDeletedEvent(long Id) : INotification;
}