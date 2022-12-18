using System;

namespace WZH.Domain.Base
{
    internal interface IHasDeletionTime
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDel { get; }

        DateTime? DelDateTime { get; }

        long? DelUserId { get; }

        /// <summary>
        /// 软删除
        /// </summary>
        void SoftDelete(long userId);
    }
}