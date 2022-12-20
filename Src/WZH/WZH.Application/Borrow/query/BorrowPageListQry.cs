

using WZH.Domain.Borrow.enums;

namespace WZH.Application.Borrow.query
{
    /// <summary>
    /// 借阅分页列表查询参数
    /// </summary>
   public record BorrowPageListQry
    {
        /// <summary>
        /// 借阅申请名称
        /// </summary>
        public string? BorrowName { get; set; }

        /// <summary>
        /// 借阅状态Code
        /// </summary>
        public int StatusCode { get; set; }
    }

    /// <summary>
    /// 校验
    /// </summary>
    public class BorrowPageListQryValidator : AbstractValidator<BorrowPageListQry>
    {
        /// <summary>
        ///
        /// </summary>
        public BorrowPageListQryValidator()
        {
            RuleFor(e => e.StatusCode)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(4)
                .Must(EncryptionPassword).WithMessage("{PropertyName}非法状态，请核实状态码");
        }

        private bool EncryptionPassword(int code)
        {
            if (string.IsNullOrWhiteSpace(Enum.GetName(typeof(BorrowStatusType), code)))
            {
                return false;
            }
            return true;
        }
    }
}
