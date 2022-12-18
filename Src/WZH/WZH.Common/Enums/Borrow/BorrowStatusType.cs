using System.ComponentModel;

namespace WZH.Common.Enums.Borrow
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