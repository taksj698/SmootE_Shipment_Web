using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbDivision))]
	public class TbDivision
	{
		[Key]
		public int Id { get; set; }
        public string DivisionName { get; set; }
	}
}
