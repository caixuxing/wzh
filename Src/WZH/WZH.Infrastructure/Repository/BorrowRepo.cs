namespace WZH.Infrastructure.Repository
{
    /// <summary>
    /// 借阅仓储
    /// </summary>
    public class BorrowRepo : BaseRepo<BorrowEntity>, IBorrowRepo
    {
        public BorrowRepo(WzhDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(long ArchiveId, string Status)> FindStatus()
        {
            var data = await _dbContext.Borrow.SingleAsync(x => x.Id == 1602473233960013824L);
            return (data!.Id, data?.Status.GetHashCode().ToString());
        }


        public async override Task<bool> Add(BorrowEntity entity)
        {
            _dbContext.Add(entity);
            _dbContext.AddRange(entity.borrowDetailsEntities);
          return await _dbContext.SaveChangesAsync()>0;
        }

        public async override Task<bool> Add(IEnumerable<BorrowEntity> entity)
        {
            await _dbContext.BulkInsertAsync(entity);
            List<BorrowDetailsEntity> borrowDetailsEntities = new List<BorrowDetailsEntity>();
            foreach (var item in entity)
            {
                borrowDetailsEntities.AddRange(item.borrowDetailsEntities);
            }
            await _dbContext.BulkInsertAsync(borrowDetailsEntities);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}