using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZH.Domain.Base
{
    interface IHasCreationTime
    {
        DateTime CreateTime { get; }

        long CreateUserId { get; }
    }
}
