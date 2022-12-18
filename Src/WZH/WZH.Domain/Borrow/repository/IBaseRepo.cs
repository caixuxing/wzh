using System.Collections.Generic;
using System.Threading.Tasks;

namespace WZH.Domain.Borrow.repository
{
    public interface IBaseRepo<T>
    {
        /// <summary>
        /// 根据聚合ID获取聚合实体
        /// </summary>
        /// <param name="agg"></param>
        /// <returns></returns>
        Task<T> FindById(long agg);

        Task<bool> Add(T entity);

        Task<bool> Add(IEnumerable<T> entity);

        Task<bool> Modify(T entity);

        Task<bool> Modify(IEnumerable<T> entity);

        Task<bool> PathModify(T entity, params string[] param);

        Task<bool> Delete(T entity);

        Task<bool> Delete(IEnumerable<T> entity);
    }
}