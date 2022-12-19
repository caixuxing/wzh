using WZH.Application.Borrow.dto;
using WZH.Application.Borrow.query;
using WZH.Common.Response;

namespace WZH.Application.Borrow
{
    /// <summary>
    ///
    /// </summary>
    public interface IBorrowQueryApp
    {
        /// <summary>
        /// 借阅列表
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        Task<MessageModel<List<BorrowPageListDTO>>>GetPageListQry(BorrowPageListQry qry,int pageIndex);
    }
}