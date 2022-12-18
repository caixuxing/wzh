namespace WZH.Application.Borrow.cmd
{
    /// <summary>
    /// 申请借阅Cmd
    /// </summary>
    public class ApplyBorrowCmd
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        [Required]
        public long ArchiveId { get; set; }

        /// <summary>
        /// 申请名称
        /// </summary>
        [Required]
        public string? ApplyName { get; set; }
    }

    /// <summary>
    /// 校验
    /// </summary>
    public class ApplyBorrowCmdValidator : AbstractValidator<ApplyBorrowCmd>
    {
        /// <summary>
        ///
        /// </summary>
        public ApplyBorrowCmdValidator()
        {
            RuleFor(e => e.ApplyName).NotNull().NotEmpty().MinimumLength(5).MaximumLength(20);
        }
    }
}