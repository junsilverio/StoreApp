using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Order_Items")]
    public class OrderItem : BaseAuditableEntity
    {
        [Column("Order_Id")]
        public int OrderId { get; set; }

        [Column("Item_Id")]
        public int ItemId { get; set; }

        [Column("Product_Id")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column("List_Price")]
        public decimal ListPrice { get; set; }

        public decimal Discount{ get; set; }
    }
}
