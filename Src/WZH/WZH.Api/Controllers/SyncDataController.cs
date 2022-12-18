using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WZH.Application.SyncData;
using WZH.Common.Response;

namespace WZH.Api.Controllers
{

    /// <summary>
    /// 同步数据
    /// </summary>
    [Route("api/sync_data")]
    [ApiController]
    public class SyncDataController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISyncDataAppCmd _syncDataAppCmd;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncDataAppCmd"></param>
        public SyncDataController(ISyncDataAppCmd syncDataAppCmd)
        {
            _syncDataAppCmd = syncDataAppCmd;
        }
        
        /// <summary>
        /// 同步科室
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("dept")]
        public async Task<MessageModel<string>> Dept() => await _syncDataAppCmd.Dept();


        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<string>> FindById([Required(ErrorMessage = "主键ID不能为空!")] long id)
        {
            Console.WriteLine(id.ToString());
            await Task.CompletedTask;
            return Ok("");
        }
    }
}
