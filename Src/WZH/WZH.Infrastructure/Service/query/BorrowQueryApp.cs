using System.Linq.Expressions;
using WZH.Application.Borrow;
using WZH.Application.Borrow.dto;
using WZH.Application.Borrow.query;
using WZH.Common.Assert;
using WZH.Common.Enums;
using WZH.Common.Response;
using WZH.Domain.Borrow.enums;

namespace WZH.Infrastructure.Service.query
{
    public class BorrowQueryApp : IBorrowQueryApp
    {
        private readonly WzhDbContext _dbContext;
        public BorrowQueryApp(WzhDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var data = _dbContext.Borrow.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(qry.BorrowName))
            {
                data = data.Where(x => x.ApplyBorrowName.StartsWith(qry.BorrowName));
            }
            if (qry.StatusCode > 0)
            {
                data = data.Where(x => x.Status == Enum.Parse<BorrowStatusType>(qry.StatusCode.ToString()));
            }
            var result = await data.Select(x => new { x.Id, x.ApplyBorrowName, x.Status,x.BorrowDate }).OrderByDescending(x => x.Id)
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
                        BorrowName = null,
                        StatusCode = item.Status,
                        StatusName = item.Status.FetchDescription(),
                        BorrowDate=item.BorrowDate
                    };
                }
            }
            return ApiResponse<PageModel<BorrowPageListDto>>.Success("操作成功！", new PageModel<BorrowPageListDto>
            {
                data = getAll(),
                dataCount = count,
                page = pageIndex,
                PageSize = pagesize

            });
        }
    }
}