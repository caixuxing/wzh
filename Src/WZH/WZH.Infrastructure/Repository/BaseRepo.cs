namespace WZH.Infrastructure.Repository
{
    /// <summary>
    /// 基类仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepo<T> where T : class
    {
        protected readonly WzhDbContext _dbContext;

        public BaseRepo(WzhDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 根据聚合ID获取聚合实体
        /// </summary>
        /// <param name="agg"></param>
        /// <returns></returns>
        public virtual async Task<T> FindById(long agg)
        {
            var data = await _dbContext.FindAsync<T>(agg);
            return data;
        }

        public virtual async Task<bool> Add(T entity)
        {
            _dbContext.Add(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Add(IEnumerable<T> entity)
        {
            await _dbContext.BulkInsertAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Modify(T entity)
        {
            _dbContext.Update(entity);
            var entry = this._dbContext.Entry(entity);
            //如果数据没有发生变化
            if (!this._dbContext.ChangeTracker.HasChanges())
            {
                entry.State = EntityState.Unchanged;
                return await Task.FromResult(true);
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Modify(IEnumerable<T> entity)
        {
            _dbContext.UpdateRange(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> PathModify(T entity, params string[] param)
        {
            var entry1 = _dbContext.Entry(entity);
            foreach (var item in param)
            {
                entry1.Property(item.ToString()).IsModified = true;
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(IEnumerable<T> entity)
        {
            _dbContext.RemoveRange(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}