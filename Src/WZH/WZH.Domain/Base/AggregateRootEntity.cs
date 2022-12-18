using System;

namespace WZH.Domain.Base
{
    public record AggregateRootEntity : BaseEntity, IHasCreationTime, IHasModificationTime, IHasDeletionTime
    {
        public DateTime CreateTime { get; init; } = DateTime.Now;

        public long CreateUserId { get; init; }

        public long? LastModifyUserId { get; private set; }

        public DateTime? LastModifyTime { get; private set; }

        public bool IsDel { get; private set; } = false;
        public DateTime? DelDateTime { get; private set; }
        public long? DelUserId { get; private set; }

        public void SoftDelete(long userId)
        {
            this.IsDel = true;
            this.DelUserId = userId;
            this.DelDateTime = System.DateTime.Now;
        }
    }
}