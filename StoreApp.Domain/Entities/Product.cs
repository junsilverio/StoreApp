using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Products")]
    public class Product : BaseAuditableEntity
    {
        [Column("Product_Id")]
        public int ProductId { get; set; }

        [Column("Product_Name")]
        public string ProductName { get; set; }

        [Column("Brand_Id")]
        public int BrandId { get; set; }

        [Column("Category_Id")]
        public int CategoryId { get; set; }

        [Column("Model_Year")]
        public int ModelYear { get; set; }

        [Column("List_Price")]
        public decimal ListPrice { get; set; }
    }
}
