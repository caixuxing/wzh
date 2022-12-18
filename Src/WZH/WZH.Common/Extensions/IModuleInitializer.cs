﻿namespace WZH.Common.Extensions
{
    /// <summary>
    /// 模块注入
    /// </summary>
    public interface IModuleInitializer
    {
        /// <summary>
        /// 初始化注入
        /// </summary>
        /// <param name="services"></param>
        public void Initialize(IServiceCollection services);
    }
}