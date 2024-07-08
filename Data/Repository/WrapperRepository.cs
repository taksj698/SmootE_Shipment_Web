using Document_Control.Data.Repository.SQLServer;

namespace Document_Control.Data.Repository
{
	public class WrapperRepository
	{
		public WrapperRepository(SqlServerDbContext dbContext)
		{
			_dbContext = dbContext;
		}


		public SqlServerDbContext _dbContext { get; }
	}
}
