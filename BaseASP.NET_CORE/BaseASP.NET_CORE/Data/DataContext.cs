using BaseASP.NET_CORE.Entities;
using BaseASP.NET_CORE.Entities.HeThong;
using Microsoft.EntityFrameworkCore;

namespace BaseASP.NET_CORE.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<NhatKy> NhatKys { get; set; }
		public DbSet<NguoiDung> NguoiDungs { get; set; }

		public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}
	}
}