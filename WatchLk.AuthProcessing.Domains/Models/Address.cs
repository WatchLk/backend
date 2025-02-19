

using System.ComponentModel.DataAnnotations;

namespace WatchLk.AuthProcessing.Domains.Models
{
    public class Address
    {
        public int Id { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        [Length(5,5)]
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string Phone { get; set; }
        public required string UserId { get; set; }

        public User? User { get; set; }
    }
}
