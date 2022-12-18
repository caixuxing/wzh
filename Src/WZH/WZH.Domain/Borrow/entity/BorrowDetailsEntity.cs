using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZH.Domain.Base;

namespace WZH.Domain.Borrow.entity
{
    /// <summary>
    /// 借阅明细
    /// </summary>
   public record BorrowDetailsEntity: AggregateRootEntity
    {
        /// <summary>
        /// 借阅ID
        /// </summary>
        public long BorrowId { get; set; }

        public long MyProperty { get; set; }
    }
}
