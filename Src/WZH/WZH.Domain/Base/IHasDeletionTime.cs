using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZH.Domain.Base
{
    interface IHasDeletionTime
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDel { get; }

        DateTime? DelDateTime { get; }

        long? DelUserId { get;}
        /// <summary>
        /// 软删除
        /// </summary>
        void SoftDelete(long userId);
    }
}
