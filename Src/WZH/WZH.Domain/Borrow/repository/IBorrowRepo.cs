using System.Threading.Tasks;
using WZH.Domain.Borrow.entity;

namespace WZH.Domain.Borrow.repository
{
    /// <summary>
    ///
    /// </summary>
    public interface IBorrowRepo : IBaseRepo<BorrowEntity>
    {
        Task<(long ArchiveId, string Status)> FindStatus();
    }
}