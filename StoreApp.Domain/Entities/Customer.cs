using StoreApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace StoreApp.Domain.Entities
{
    [Table("Customers")]
    public class Customer : BaseAuditableEntity
    {
        [Column("Customer_Id")]
        public int CustomerId { get; set; }

        [Column("First_Name")]
        public string FirstName { get; set; }

        [Column("Last_Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Column("Zip_Code")]
        public string ZipCode { get; set; }

    }
}
