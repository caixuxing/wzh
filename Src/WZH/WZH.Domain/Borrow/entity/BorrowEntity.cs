using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WZH.Common.Snowflake;
using WZH.Domain.Base;
using WZH.Domain.Borrow.enums;
using WZH.Domain.Borrow.events;

namespace WZH.Domain.Borrow.entity
{
    /// <summary>
    /// 借阅聚合
    /// </summary>
    public  record BorrowEntity : AggregateRootEntity
    {
        private BorrowEntity() { }
 
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
        /// 借阅明细
        /// </summary>
        public virtual ICollection<BorrowDetailsEntity> borrowDetailsEntities { get; private set; }

        /// <summary>
        /// 创建借阅申请
        /// </summary>
        /// <param name="archiveid"></param>
        /// <param name="applyborrowname"></param>
        /// <param name="borrowDetailsEntities"></param>
        /// <returns></returns>
        public static BorrowEntity Create(string applyborrowname)
        {
            BorrowEntity entity = new()
            {
                BorrowDate = DateTime.Now,
                BorrowDeptCode = "JY" + IdWorker.Instance.NextId(),
                BorrowTpye = "type",
                BorrowUserCode = "code",
                ApplyBorrowName = applyborrowname,
                ReturnDate = DateTime.Now,
                Status = BorrowStatusType.END,
                CreateUserId = IdWorker.Instance.NextId()
            };
            return entity;
        }


        /// <summary>
        /// 增加借阅明细
        /// </summary>
        /// <returns></returns>
        public BorrowEntity AddborrowDetails(BorrowDetailsEntity borrowDetailsEntity)
        {
            if (this.borrowDetailsEntities == null)
            {
                this.borrowDetailsEntities = new List<BorrowDetailsEntity>();
            }
            this.borrowDetailsEntities.Add(borrowDetailsEntity);
            return this;
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
                Id = id,
                Status = borrowStatus
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

    }
}