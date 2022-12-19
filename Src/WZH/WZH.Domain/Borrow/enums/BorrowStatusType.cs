using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZH.Domain.Borrow.enums
{
    public enum BorrowStatusType
    {
        /// <summary>
        /// 通过
        /// </summary>
        [Description("通过")]
        PASSED,

        /// <summary>
        /// 驳回
        /// </summary>
        [Description("驳回")]
        REJECTED,

        /// <summary>
        /// 拒绝(结束)
        /// </summary>
        [Description("拒绝(结束)")]
        END,

        /// <summary>
        ///撤回、废弃
        /// </summary>
        [Description("撤回、废弃")]
        REVOCATION
    }
}
