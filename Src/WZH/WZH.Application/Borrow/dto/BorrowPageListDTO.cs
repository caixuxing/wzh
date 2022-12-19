

using WZH.Domain.Borrow.enums;

namespace WZH.Application.Borrow.dto
{
    /// <summary>
    /// 借阅分页列表集合DTO
    /// </summary>
    public record BorrowPageListDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 借阅申请名称
        /// </summary>
        public string? BorrowName { get; set; }

        /// <summary>
        /// 状态名
        /// </summary>
        public string? StatusName { get; set; }
        /// <summary>
        /// 状态Code
        /// </summary>
        public BorrowStatusType StatusCode { get; set; }

        /// <summary>
        /// BorrowDate
        /// </summary>
        public DateTime? BorrowDate { get; set; }
    }
}
