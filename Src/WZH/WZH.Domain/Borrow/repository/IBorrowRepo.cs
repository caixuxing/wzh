using System.Threading.Tasks;
using WZH.Domain.Borrow.entity;
using WZH.Domain.Comm;

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