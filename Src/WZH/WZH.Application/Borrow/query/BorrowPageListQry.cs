

namespace WZH.Application.Borrow.query
{
    /// <summary>
    /// 借阅分页列表查询参数
    /// </summary>
   public class BorrowPageListQry
    {
        /// <summary>
        /// 借阅申请名称
        /// </summary>
        public string? BorrowName { get; set; }

        /// <summary>
        /// 借阅状态Code
        /// </summary>
        public int StatusCode { get; set; }
    }
}
