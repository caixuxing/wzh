using WZH.Domain.SystemManage.entity;

namespace WZH.Infrastructure.EntityConfigs
{
    public class LogsConfig : IEntityTypeConfiguration<LogEntity>
    {
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            builder.ToTable("Logs").HasComment("系统日志表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).ValueGeneratedNever().HasComment("主键ID");
            builder.HasQueryFilter(a => !a.IsDel);
        }
    }
}