using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
	[Table(nameof(TB_User))]
	public class TB_User
    {
		[Key]
		public int Id { get; set; }	
		public string? UserCode { get; set; }

        public string? UserName { get; set; }	
		public string? Password { get; set; }
		public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

		public bool? Active { get; set; }
		public DateTime? CreateDate { get; set; }
		public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
    }
}
