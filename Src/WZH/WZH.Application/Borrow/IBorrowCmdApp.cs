
using WZH.Common.Response;

namespace WZH.Application.Borrow
{

    /// <summary>
    /// 
    /// </summary>
    public interface IBorrowCmdApp
    {

        /// <summary>
        /// 申请借阅
        /// </summary>
        /// <returns></returns>
        Task<MessageModel<BorrowEntity>> ApplyBorrow(ApplyBorrowCmd cmd);


        /// <summary>
        /// 修改申请借阅
        /// </summary>
        /// <returns></returns>
        Task<MessageModel<string>> ModifyApplyBorrow(ApplyBorrowCmd cmd,long Id);


        /// <summary>
        /// 修改申请借阅名称
        /// </summary>
        /// <param name="applyname"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<MessageModel<string>> ModifyApplyBorrowName(string applyname, long Id);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<MessageModel<string>> Approval(int status, long Id);
    }
}
