using WZH.Domain.Base;

namespace WZH.Domain.Borrow.entity
{
    /// <summary>
    /// 借阅明细
    /// </summary>
    public record BorrowDetailsEntity : BaseEntity
    {
        /// <summary>
        /// 借阅ID
        /// </summary>
        public long BorrowId { get; set; }

        /// <summary>
        /// 文档ID
        /// </summary>
        public long ArchiveId { get; set; }


        public static BorrowDetailsEntity Create(long BorrowId, long ArchiveId)
        {
            BorrowDetailsEntity entity = new BorrowDetailsEntity()
            {
                BorrowId = BorrowId,
                ArchiveId = ArchiveId
            };

            return entity;

        }
    }
}