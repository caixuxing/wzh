using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZH.Domain.Borrow.events
{
    public record SoftDeletedEvent(long Id) : INotification;
}
