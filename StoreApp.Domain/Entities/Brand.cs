using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Domain.Entities
{
    [Table("Brands")]
    public class Brand : BaseAuditableEntity
    {
        [Column("Brand_Id")]
        public int BrandId { get; set; }

        [Column("Brand_Name")]
        public string BrandName { get; set; }

    }
}
