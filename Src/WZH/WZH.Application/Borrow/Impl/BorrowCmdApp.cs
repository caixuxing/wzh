using WZH.Common.Response;
using WZH.Domain.Borrow.enums;

namespace WZH.Application.Borrow.Impl
{
    /// <summary>
    ///
    /// </summary>
    public class BorrowCmdApp : IBorrowCmdApp
    {
        /// <summary>
        ///
        /// </summary>
        private readonly IBorrowRepo _borrowRepo;

        /// <summary>
        ///
        /// </summary>
        /// <param name="borrowRepo"></param>
        public BorrowCmdApp(IBorrowRepo borrowRepo)
        {
            this._borrowRepo = borrowRepo;
        }

        public virtual async Task<MessageModel<BorrowEntity>> ApplyBorrow(ApplyBorrowCmd cmd)
        {
            IEnumerable<BorrowEntity> get()
            {
                for (int i = 0; i < 100000; i++)
                {
                    var entity = BorrowEntity.Create(cmd.ApplyName+i.ToString());
                    foreach (var item in cmd.ArchiveIds)
                    {
                        entity.AddborrowDetails(BorrowDetailsEntity.Create(entity.Id, item));
                    }
                    yield return entity;
                }
            }
            await _borrowRepo.Add(get().ToList());
            return ApiResponse<BorrowEntity>.Success("操作成功！", get().FirstOrDefault()!);
        }

        /// <summary>
        /// 审批（局部更新）
        /// </summary>
        /// <param name="status"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MessageModel<string>> Approval(int status, long Id)
        {
            AssertUtils.IsObjNull(Enum.GetName(typeof(BorrowStatusType), status), "非法状态，请核实状态码");
            var result = await _borrowRepo.FindById(Id);
            AssertUtils.IsObjNull(result, "资源不存，审批失败");
            result.ChangeApproval(Enum.Parse<BorrowStatusType>(status.ToString()));
            await _borrowRepo.PathModify(result, "Status");
            return ApiResponse<string>.Success();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual async Task<MessageModel<string>> ModifyApplyBorrow(ApplyBorrowCmd cmd, long Id)
        {
            var data = await _borrowRepo.FindById(Id);
            AssertUtils.IsObjNull(data, "资源不存，无法进行更新操作");
            data.ChangeApplyBorrowName(cmd.ApplyName!);
            await _borrowRepo.Modify(data);
            return ApiResponse<string>.Success();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="applyname"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual async Task<MessageModel<string>> ModifyApplyBorrowName(string applyname, long Id)
        {
            var data = await _borrowRepo.FindById(Id);
            data.ChangeApplyBorrowName(applyname);
            await _borrowRepo.Modify(data);
            return ApiResponse<string>.Success();
        }
    }
}