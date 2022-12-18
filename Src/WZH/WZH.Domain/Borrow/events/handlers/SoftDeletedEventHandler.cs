using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WZH.Common.Assert;
using WZH.Common.Enums;
using WZH.Domain.Borrow.repository;

namespace WZH.Domain.Borrow.events.handlers
{
 

    public class SoftDeletedEventHandler : INotificationHandler<SoftDeletedEvent>
    {
        private readonly ILogger<SoftDeletedEventHandler> logger;
        /// <summary>
        /// 
        /// </summary>
        private readonly IBorrowRepo _borrowRepo;
        public SoftDeletedEventHandler(ILogger<SoftDeletedEventHandler> logger, IBorrowRepo borrowRepo)
        {
            this.logger = logger;
            _borrowRepo = borrowRepo;
        }

        public async Task Handle(SoftDeletedEvent notification, CancellationToken cancellationToken)
        {
           var data= await this._borrowRepo.FindById(notification.Id);
            data.SoftDelete(notification.Id);
            logger.LogInformation($"已删除");
        }
    }
}
