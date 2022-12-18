using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZH.Domain.Borrow.repository;

namespace WZH.Domain.Borrow.service
{
    /// <summary>
    /// 借阅管理领域服务
    /// </summary>
   public class BorrowDomainService
    {
        private readonly IBorrowRepo _borrowRepo;
        public BorrowDomainService(IBorrowRepo borrowRepo)
        {
            _borrowRepo = borrowRepo;
        }

        /// <summary>
        /// 
        /// </summary>
       public async Task<bool> BorrowApply()
        {
          var result= await _borrowRepo.FindStatus();
            if (result.ArchiveId>0)
            {
                throw new Exception("档案已在审批中无法进行撤回！");
            }
            return await Task.FromResult(true);
        }
    }
}
