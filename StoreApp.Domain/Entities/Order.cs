using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Orders")]
    public class Order : BaseAuditableEntity
    {
        [Column("Order_Id")]
        public int OrderId { get; set; }

        [Column("Customer_Id")]
        public int CustomerId { get; set; }

        [Column("Order_Status")]
        public int OrderStatus { get; set; }

        [Column("Order_Date")]
        public DateTime OrderDate{ get; set; }

        [Column("Required_Date")]
        public DateTime RequiredDate { get; set; }

        [Column("Shipped_Date")]
        public DateTime ShippedDate { get; set; }

        [Column("Store_Id")]
        public int StoreId { get; set; }

        [Column("Staff_Id")]
        public int StaffId { get; set; }

    }
}
