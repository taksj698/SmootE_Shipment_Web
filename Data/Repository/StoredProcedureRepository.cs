using Document_Control.Core.dbModels;
using Document_Control.Data.Repository.SQLServer;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Document_Control.Data.Repository
{

	public class StoredProcedureRepository
	{
		private readonly SqlServerDbContext _sqlServerDbContext;
		public StoredProcedureRepository(SqlServerDbContext sqlServerDbContext)
		{
			_sqlServerDbContext = sqlServerDbContext;
		}

		public string GenarateCode()
		{

			var prResult = _sqlServerDbContext.PRResult
							.FromSqlRaw("EXEC GeneratePR")
							.ToList()
							.FirstOrDefault();

			if (prResult != null)
			{
				return prResult.PRNumber;
			}
			return null;
		}




	}
}
