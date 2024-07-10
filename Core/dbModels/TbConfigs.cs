using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbConfigs))]
	public class TbConfigs
	{
		[Key]
		public int Id { get; set; }
		public string Group { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string? Desc { get; set; }
	}
}
