namespace WZH.Infrastructure.DbContext
{
    public class CodeFirstDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CodeFirstDbContext()
        {
        }

        public DbSet<BorrowEntity> Borrow { get; private set; }//不要忘了写set，否则拿到的DbContext的Categories为null

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string SqlType = "MSSQL";

                if (SqlType.ToUpper() == "MSSQL")
                {
                    string connectionString = "Data Source=localhost;Database=wzh_db;User ID=sa;Password=123";
                    optionsBuilder.UseSqlServer(connectionString, b => b.MaxBatchSize(50));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}