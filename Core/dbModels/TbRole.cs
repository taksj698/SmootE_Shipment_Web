using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbRole))]
	public class TbRole
	{
		[Key]
		public int Id { get; set; }
		public string RoleName { get; set; }
	}
}
