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
    }
}