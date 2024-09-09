using QuickVisualWebWood.Data.Repository.SQLServer;

namespace QuickVisualWebWood.Data.Repository
{
	public class WrapperRepository
	{
		public WrapperRepository(SqlServerDbContext dbContext, StoredProcedureRepository storedProcedureRepository)
		{
			_dbContext = dbContext;
			_storedProcedureRepository = storedProcedureRepository;
		}


		public SqlServerDbContext _dbContext { get; }
		public StoredProcedureRepository _storedProcedureRepository { get; }
	}
}
