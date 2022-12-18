namespace WZH.Common.Enums
{ 
    /// <summary>
    /// HTTP状态码
    /// </summary>
    public enum HttpStatusType
    {
        /// <summary>
        /// 成功
        /// </summary>
        SUCCEED = 200,
        /// <summary>
        /// （无内容）  服务器成功处理了请求，但没有返回任何内容。
        /// </summary>
        NoContent = 204,


        /// <summary>
        /// 错误请求
        /// </summary>
        BADREQUEST = 400,
        /// <summary>
        ///（权限不足） 请求要求身份验证。
        /// </summary>
        PERMISSIONS = 401,
        /// <summary>
        /// 未找到服务器找不到请求。 
        /// </summary>
        NOTFOUND = 404,
        /// <summary>
        /// 请求超时
        /// </summary>
        TIMEOUT = 408,
        /// <summary>
        /// 校验错误
        /// </summary>
        VERIFY = 412,

        /// <summary>
        /// 未通过
        /// </summary>
        FAILED = 418,

        /// <summary>
        /// 服务器内部错误 服务器遇到错误，无法完成请求。
        /// </summary>
        SERVERERROR = 500

    }
}