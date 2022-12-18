namespace WZH.Domain.Base
{
    /// <summary>
    /// 聚合根主键
    /// </summary>
    public interface IEntity
    {
        public long Id { get; }
    }
}