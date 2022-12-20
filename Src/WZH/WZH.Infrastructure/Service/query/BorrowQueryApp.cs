using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq.Expressions;
using WZH.Application.Borrow;
using WZH.Application.Borrow.dto;
using WZH.Application.Borrow.query;
using WZH.Common.Assert;
using WZH.Common.Enums;
using WZH.Common.MemoryCache;
using WZH.Common.Response;
using WZH.Domain.Borrow.enums;

namespace WZH.Infrastructure.Service.query
{
    public class BorrowQueryApp : IBorrowQueryApp
    {
        private readonly WzhDbContext _dbContext;
        private readonly IMemoryCacheHelper _cache;
        public BorrowQueryApp(WzhDbContext dbContext, IMemoryCacheHelper cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<MessageModel<PageModel<BorrowPageListDto>>> GetPageListQry(BorrowPageListQry qry, int pageIndex, int pagesize)
        {

            Func<Task<MessageModel<PageModel<BorrowPageListDto>>>> func = async () =>
           {

                //如果查询很复查建议直接采用纯SQL查询
                List<BorrowPageListDto> TestData = _dbContext.ToSlave().Database.SqlQuery<BorrowPageListDto>(@"SELECT cast(Id as varchar(50))as Id,ApplyBorrowName as BorrowName,Status as StatusCode,BorrowDate FROM Borrow
       where IsDel=0
       ORDER BY [CreateTime]
         OFFSET 10 ROWS FETCH NEXT 50 ROWS ONLY ");
               IEnumerable<BorrowPageListDto> getAlls()
               {
                   foreach (var item in TestData)
                   {
                       yield return new BorrowPageListDto()
                       {
                           Id = item.Id.ToString(),
                           BorrowName = item.BorrowName,
                           StatusCode = item.StatusCode,
                           StatusName = item.StatusCode.FetchDescription(),
                           BorrowDate = item.BorrowDate
                       };
                   }
               }
               var datassd = getAlls();




                // EF Core 单表查询
                var data = _dbContext.ToMaster().Set<BorrowEntity>().AsNoTracking().AsQueryable();
               if (!string.IsNullOrWhiteSpace(qry.BorrowName))
               {
                   data = data.Where(x => x.ApplyBorrowName.StartsWith(qry.BorrowName));
               }
               if (qry.StatusCode > 0)
               {
                   data = data.Where(x => x.Status == Enum.Parse<BorrowStatusType>(qry.StatusCode.ToString()));
               }
               var result = await data.Select(x => new { x.Id, x.ApplyBorrowName, x.Status, x.BorrowDate }).OrderByDescending(x => x.Id)
                    .Skip(pageIndex).Take(pagesize)

                    .ToListAsync();
               var count = await data.CountAsync();
               IEnumerable<BorrowPageListDto> getAll()
               {
                   foreach (var item in result)
                   {
                       yield return new BorrowPageListDto()
                       {
                           Id = item.Id.ToString(),
                           BorrowName = item.ApplyBorrowName,
                           StatusCode = item.Status,
                           StatusName = item.Status.FetchDescription(),
                           BorrowDate = item.BorrowDate
                       };
                   }
               }

                //linq查询
                var query = from a in _dbContext.ToMaster().Set<BorrowEntity>()
                           join b in _dbContext.ToMaster().Set<BorrowDetailsEntity>()
                            on a.Id equals b.BorrowId into BorrowDetail
                           from d in BorrowDetail.DefaultIfEmpty()
                           select new { a.Id, a.ApplyBorrowName, d.ArchiveId };
               var linqData = await query.ToListAsync();


                //EF Core多表  Include、Join 必须在Fluent API  表配置中使用主外键关系
                var efCoreMultiTable = await _dbContext.ToMaster().Set<BorrowEntity>().Include(x => x.borrowDetailsEntities).AsNoTracking().ToListAsync();

               var resultt = ApiResponse<PageModel<BorrowPageListDto>>.Success("操作成功！", new PageModel<BorrowPageListDto>
               {
                   data = getAll(),
                   dataCount = count,
                   page = pageIndex,
                   PageSize = pagesize

               });

               return resultt;
           };
            var Key = "Borrow"+qry.ToString().Trim() + $"{pageIndex}/{pagesize}";
            var datakk = await _cache.GetOrCreateAsync(Key, (e) => func());
            return datakk;
        }
    }
}