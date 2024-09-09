using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbPosition))]
	public class TbPosition
	{
		[Key]
		public int Id { get; set; }
		public string? PositionName { get; set; }
	}
}
