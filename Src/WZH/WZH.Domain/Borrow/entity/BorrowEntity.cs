using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZH.Common.Enums.Borrow;
using WZH.Common.Snowflake;
using WZH.Domain.Base;
using WZH.Domain.Borrow.events;

namespace WZH.Domain.Borrow.entity
{
    /// <summary>
    /// 借阅聚合
    /// </summary>
   public sealed  record BorrowEntity: AggregateRootEntity, IValidatableObject
    {
        private BorrowEntity() { }
        /// <summary>
        /// 文档Id
        /// </summary>
        public long ArchiveId { get; init; }

        /// <summary>
        /// 申请借阅名称
        /// </summary>
        public string ApplyBorrowName { get; private set; }
        /// <summary>
        /// 借阅Code
        /// </summary>
        public string BorrowUserCode { get; private set; }
        /// <summary>
        /// 借阅部门Code
        /// </summary>
       
        public string BorrowDeptCode { get; private set; }
        /// <summary>
        /// 借阅类型
        /// </summary>
        public string BorrowTpye { get; private set; }
        /// <summary>
        /// 借阅日期
        /// </summary>
        public DateTime BorrowDate { get; init; }
        /// <summary>
        /// 预归还日期
        /// </summary>
        public DateTime ReturnDate { get; private set; }
        /// <summary>
        /// 借阅状态
        /// </summary>
        public BorrowStatusType Status { get; private set; }



        /// <summary>
        /// 创建申请借阅
        /// </summary>
        /// <returns></returns>
        public static BorrowEntity Create(long archiveid, string applyborrowname)
        {

            BorrowEntity entity = new()
            {
                ArchiveId = archiveid,
                BorrowDate = DateTime.Now,
                BorrowDeptCode = "JY" + IdWorker.Instance.NextId(),
                BorrowTpye="type",
                BorrowUserCode="code",
                ApplyBorrowName = applyborrowname,
                ReturnDate = DateTime.Now,
                Status = BorrowStatusType.END,
                CreateUserId = IdWorker.Instance.NextId()
            };
            return entity;
        }

        /// <summary>
        /// 修改申请借阅名称
        /// </summary>
        /// <returns></returns>
        public BorrowEntity ChangeApplyBorrowName(string applyborrowname)
        {
            this.ApplyBorrowName = applyborrowname;
            return this;
        }

        /// <summary>
        /// 修改申请借阅名称
        /// </summary>
        /// <returns></returns>
        public static BorrowEntity ChangeApproval(long id, BorrowStatusType borrowStatus)
        {
            BorrowEntity entity = new()
            {
               Id= id,Status= borrowStatus
            };
            return entity;
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="borrowStatus"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public BorrowEntity ChangeApproval(BorrowStatusType borrowStatus)
        {
            this.Status = borrowStatus;
            this.AddDomainEvent(new SoftDeletedEvent(Id));
            return this;
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <returns></returns>
        public BorrowEntity ChangeQuash()
        {
            this.Status = BorrowStatusType.REVOCATION;
            return this;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
