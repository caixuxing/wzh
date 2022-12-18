namespace WZH.Infrastructure.EntityConfigs
{
    /// <summary>
    /// 
    /// </summary>
    public class BorrowConfig : IEntityTypeConfiguration<BorrowEntity>
    {
        
        public void Configure(EntityTypeBuilder<BorrowEntity> builder)
        {
            builder.ToTable("Borrow").HasComment("借阅表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).ValueGeneratedNever().HasComment("主键ID");
            builder.Property(e => e.ArchiveId).IsRequired().HasComment("文档ID");
            builder.Property(x => x.BorrowDeptCode).HasMaxLength(50).IsRequired().HasComment("借阅部门");
            builder.Property(x => x.ApplyBorrowName).HasMaxLength(150).IsRequired().HasComment("借阅申请名称");
            builder.Property(x => x.BorrowDate).HasColumnType("datetime").IsRequired().HasComment("借阅日期");
            builder.Property(x=>x.ReturnDate).HasColumnType("datetime").IsRequired().HasComment("预归还日期");

            builder.Property(a => a.RowVersion).HasComment("版本号")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("Timestamp").IsRowVersion();
            builder.HasQueryFilter(a => !a.IsDel);
        }
    }
}
