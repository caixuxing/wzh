using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZH.Domain.Base
{
   public interface IHasModificationTime
    {
        long? LastModifyUserId { get; }
        DateTime? LastModifyTime { get; }
    }
}
