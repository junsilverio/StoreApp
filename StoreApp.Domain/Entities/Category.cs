using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Categories")]
    public class Category : BaseAuditableEntity
    {
        [Column("Category_Id")]
        public int CategoryId { get; set; }

        [Column("Category_Name")]
        public string CategoryName { get; set; }

    }
}
