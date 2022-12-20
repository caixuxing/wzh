using Microsoft.Extensions.Logging;
using WZH.Application.Borrow.dto;
using WZH.Domain.Base;

namespace WZH.Infrastructure.DbContext
{
    /// <summary>
    /// 无纸化DbContext
    /// </summary>
    public class WzhDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        /*控制台打印SQL 引入下列包
           1.Microsoft.Extensions.Logging
           2.Microsoft.Extensions.Logging.Console
         */
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        private readonly DbConnectionOption _dbConnection;
        private readonly SlaveRoundRobin _slaveRoundRobin;
        private readonly IOptions<DbConnectionOption> _options;

        private readonly IMediator _mediator;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="slaveRoundRobin"></param>
        public WzhDbContext(IOptions<DbConnectionOption> options, SlaveRoundRobin slaveRoundRobin, IMediator mediator)
        {
            _dbConnection = options.Value;
            _slaveRoundRobin = slaveRoundRobin;
            _options = options;
            _mediator = mediator;
        }

        public DbSet<BorrowEntity> Borrow { get; private set; }//不要忘了写set，否则拿到的DbContext的Categories为null
        public DbSet<BorrowDetailsEntity> BorrowDetails { get; private set; }//不要忘了写set，否则拿到的DbContext的Categories为null

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_dbConnection.MasterConnection, b => b.MaxBatchSize(1000));
            }
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        /// <summary>
        /// 主库
        /// </summary>
        public Microsoft.EntityFrameworkCore.DbContext ToMaster()
        {
            //把链接字符串设为读写（主库）
            this.Database.GetDbConnection().ConnectionString = this._dbConnection.MasterConnection;
            return this;
        }

        /// <summary>
        /// 从库
        /// </summary>
        public Microsoft.EntityFrameworkCore.DbContext ToSlave()
        {
            Database.GetDbConnection().ConnectionString = _slaveRoundRobin.GetNext();
            return this;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker是上下文中用来对实体类的变化进行追踪的对象，
            //Entries<IDomainEvents>获得的是所有实现了IDomainEvents接口的追踪实体类
            var domainEntities = this.ChangeTracker.Entries<IDomainEvents>()
                .Where(x => x.Entity.GetDomainEvents().Any());
            var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents())
                .ToList();
            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());
            //在调用父类的SaveChangesAsync方法保存修改之前，
            //我们把所有实体类中注册的领域事件发布出去
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}