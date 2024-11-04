using SmootE_Shipment_Web.Data.Repository.SQLServer;

namespace SmootE_Shipment_Web.Data.Repository
{
	public class WrapperRepository
	{
		//, StoredProcedureRepository storedProcedureRepository
		public WrapperRepository(SqlServerDbContext dbContext)
		{
			_dbContext = dbContext;
			//_storedProcedureRepository = storedProcedureRepository;
		}


		public SqlServerDbContext _dbContext { get; }
		//public StoredProcedureRepository _storedProcedureRepository { get; }
	}
}
