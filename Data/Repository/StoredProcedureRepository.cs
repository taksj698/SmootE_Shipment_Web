using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Data.Repository.SQLServer;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;

namespace QuickVisualWebWood.Data.Repository
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
