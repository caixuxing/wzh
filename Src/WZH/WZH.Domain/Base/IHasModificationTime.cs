using System;

namespace WZH.Domain.Base
{
    public interface IHasModificationTime
    {
        long? LastModifyUserId { get; }
        DateTime? LastModifyTime { get; }
    }
}