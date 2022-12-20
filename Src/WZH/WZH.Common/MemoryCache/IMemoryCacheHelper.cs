

using Microsoft.Extensions.Caching.Memory;

namespace WZH.Common.MemoryCache
{
   public interface IMemoryCacheHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult">缓存的值的类型</typeparam>
        /// <param name="cacheKey">缓存的key</param>
        /// <param name="valueFactory">提供数据的委托</param>
        /// <param name="expireSeconds">缓存过期秒数的最大值，实际缓存时间是在[expireSeconds,expireSeconds*2)之间，这样可以一定程度上避免大批key集中过期导致的“缓存雪崩”的问题</param>
        /// <returns></returns>
        TResult? GetOrCreate<TResult>(string cacheKey, Func<ICacheEntry, TResult?> valueFactory, int expireSeconds = 60);

        Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<ICacheEntry, Task<TResult?>> valueFactory, int expireSeconds = 60);

        /// <summary>
        /// 删除缓存的值
        /// </summary>
        /// <param name="cacheKey"></param>
        void Remove(string cacheKey);
    }



}
