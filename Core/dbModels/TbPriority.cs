using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbPriority))]
	public class TbPriority
	{
		[Key]
		public int Id { get; set; }
		public string PriorityName { get; set; }
		public int Seq { get; set; }
	}
}
