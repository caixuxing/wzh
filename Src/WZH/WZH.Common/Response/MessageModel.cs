﻿
namespace WZH.Common.Response
{
   public record MessageModel<T>
    {


        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; } = 200;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = false;


        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T? response { get; set; }
    }
}
