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
        /// <returns></returns>
        public async Task<MessageModel<List<BorrowPageListDTO>>> GetPageListQry(BorrowPageListQry qry, int pageIndex)
        {
            var data = _dbContext.Borrow.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(qry.BorrowName))
            {
                data = data.Where(x => x.ApplyBorrowName.StartsWith(qry.BorrowName));
            }
            if (qry.StatusCode > 0)
            {
                AssertUtils.IsObjNull(Enum.GetName(typeof(BorrowStatusType), qry.StatusCode), "非法状态，请核实状态码");
                data = data.Where(x => x.Status == Enum.Parse<BorrowStatusType>(qry.StatusCode.ToString()));
            }
            var result = await data.Select(x => new { x.Id, x.ApplyBorrowName, x.Status })
                 .Skip(pageIndex).Take(50)
                 .ToListAsync();

            var count = data.Count();
            List<BorrowPageListDTO> borrowPageListDTOs = new List<BorrowPageListDTO>();
            result.ForEach(item =>
            {
                borrowPageListDTOs.Add(new BorrowPageListDTO()
                {

                    Id = item.Id.ToString(),
                    BorrowName = item.ApplyBorrowName,
                    StatusCode = ((int)item.Status),
                    StatusName = item.Status.FetchDescription()
                });
            });
            return ApiResponse<List<BorrowPageListDTO>>.Success("操作成功！", borrowPageListDTOs);
        }
    }
}