using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbStatus))]
	public class TbStatus
	{
		[Key]
		public int Id { get; set; }
		public string StatusName { get; set; }
		public int GroupStatus { get; set; }
		public bool IsDefault { get; set; }
		public bool IsMainFlow { get; set; }
	}
}
