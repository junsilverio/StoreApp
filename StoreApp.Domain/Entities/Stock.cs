using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Stocks")]
    public class Stock : BaseAuditableEntity
    {
        [Column("Store_Id")]
        public int BrandId { get; set; }

        [Column("Product_Id")]
        public int ProductId { get; set; }
       
        public int Quantity { get; set; }
    }
}
