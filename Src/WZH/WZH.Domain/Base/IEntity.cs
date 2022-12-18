using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZH.Domain.Base
{
    /// <summary>
    /// 聚合根主键
    /// </summary>
   public interface IEntity
    {
        public long Id { get; }
    }
}
