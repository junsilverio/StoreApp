using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Staffs")]
    public class Staff : BaseAuditableEntity
    {
        [Column("Staff_Id")]
        public int StaffId { get; set; }

        [Column("First_Name")]
        public string FirstName { get; set; }

        [Column("Last_Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Active { get; set; }

        [Column("Store_Id")]
        public int StoreId { get; set; }

        [Column("Manager_Id")]
        public int ManagerId { get; set; }
    }
}
