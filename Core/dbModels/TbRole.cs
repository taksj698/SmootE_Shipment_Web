using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbRole))]
	public class TbRole
	{
		[Key]
		public int Id { get; set; }
		public string RoleName { get; set; }
	}
}
