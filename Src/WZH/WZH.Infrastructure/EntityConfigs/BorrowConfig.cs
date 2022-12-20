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
            builder.Property(x => x.BorrowDeptCode).HasMaxLength(50).IsRequired().HasComment("借阅部门");
            builder.Property(x => x.ApplyBorrowName).HasMaxLength(150).IsRequired().HasComment("借阅申请名称");
            builder.Property(x => x.BorrowDate).HasColumnType("datetime").IsRequired().HasComment("借阅日期");
            builder.Property(x => x.ReturnDate).HasColumnType("datetime").IsRequired().HasComment("预归还日期");
            builder.Property(a => a.RowVersion).HasComment("版本号")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("Timestamp").IsRowVersion();
            builder.HasQueryFilter(a => !a.IsDel);
            builder.Ignore(x => x.borrowDetailsEntities);
            builder.HasMany(x => x.borrowDetailsEntities).WithOne().HasForeignKey(x=>x.BorrowId)   //建立主表与明细表主外键
                .OnDelete(DeleteBehavior.Cascade); //联级删除  删除主表数据  明细直接也跟着删除。

          

       
        }
    }

    public class BorrowDetailsConfig : IEntityTypeConfiguration<BorrowDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<BorrowDetailsEntity> builder)
        {
            builder.ToTable("BorrowDetails").HasComment("借阅明细表");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).ValueGeneratedNever().HasComment("主键Id");
            builder.Property(x => x.BorrowId).IsRequired().HasComment("借阅主键Id");
            builder.Property(x=>x.ArchiveId).IsRequired().HasComment("文档主键Id");
            builder.Property(a => a.RowVersion).HasComment("版本号")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("Timestamp").IsRowVersion();
        }
    }
}