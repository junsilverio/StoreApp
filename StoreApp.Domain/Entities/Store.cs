using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace StoreApp.Domain.Entities
{
    [Table("Stores")]
    public class Store: BaseAuditableEntity
    {
        [Column("Store_Id")]
        public int StoreId { get; set; }

        [Column("Store_Name")]
        public string StoreName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Column("Zip_Code")]
        public string ZipCode { get; set; }

    }
}
