using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WZH.Application.Borrow;
using WZH.Application.Borrow.cmd;
using WZH.Common.Response;

namespace WZH.Api.Controllers
{
    /// <summary>
    /// 借阅管理
    /// </summary>
    [Route("api/borrow")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "wzh")]
    public class BorrowController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBorrowCmdApp _borrowCmdApp;

        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="borrowCmdApp"></param>
        public BorrowController(IBorrowCmdApp borrowCmdApp, IHttpContextAccessor httpContextAccessor)
        {
            _borrowCmdApp = borrowCmdApp;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 创建申请借阅
        /// </summary>
        /// <param name="cmd">申请借阅Cmd</param>
        /// <returns></returns>
        [HttpPost, Route("add")]
        public async Task<ActionResult> AddBorrow([FromBody] ApplyBorrowCmd cmd)

        {
            var html = MiniProfiler.Current.RenderIncludes(_httpContextAccessor.HttpContext).Value;
            return Ok(await _borrowCmdApp.ApplyBorrow(cmd));
        }

        /// <summary>
        /// 修改申请借阅
        /// </summary>
        /// <param name="cmd">申请借阅Cmd</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpPut, Route("modify/{id}")]
        public async Task<ActionResult> PutBorrow([FromBody] ApplyBorrowCmd cmd, long id) => Ok(await _borrowCmdApp.ModifyApplyBorrow(cmd, id));



        /// <summary>
        /// 获取借阅申请信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        public async Task<ActionResult> FindBorrowById(long id) => Ok(await Task.FromResult(true));

        /// <summary>
        /// 审批借阅
        /// </summary>
        /// <param name="status">审批状态码</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpPatch, Route("approval/{id}")]
        public async Task<ActionResult> PatchApproval([FromForm] int status, [FromRoute] long id) => Ok(await _borrowCmdApp.Approval(status, id));



    }
}
