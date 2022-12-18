using System;

namespace WZH.Domain.Base
{
    internal interface IHasCreationTime
    {
        DateTime CreateTime { get; }

        long CreateUserId { get; }
    }
}