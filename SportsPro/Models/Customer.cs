using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Customer
    {
        public Customer() => Products = new HashSet<Product>();
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "Please Enter a First Name")]
		[StringLength (51, ErrorMessage ="First Name must be between 1 and 51 characters")]
		public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter a Last Name")]
        [StringLength(51, ErrorMessage = "Last Name must be between 1 and 51 characters")]
        public string LastName { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter a City")]
        [StringLength(51, ErrorMessage = "City must be between 1 and 51 characters")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter a State")]
        [StringLength(51, ErrorMessage = "State must be between 1 and 51 characters")]
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter a Postal Code")]
        [StringLength(51, ErrorMessage = "Postal Code must be between 1 and 21 characters")]
        public string PostalCode { get; set; } = string.Empty;
        [StringLength(21, ErrorMessage = "Phone Number must be between 1 and 21 characters")]
        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s-]\\d{3}[-]\\d{4}$", 
            ErrorMessage ="Phone number must be in xxx-xxx-xxx format")]
        public string? Phone { get; set; }
        [StringLength( 51, ErrorMessage ="Email must be between 1 and 51 characters")]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmail", "Validation", AdditionalFields = "FullName")]
        public string? Email { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Please Select a Valid Country")]
        public string CountryID { get; set; } = string.Empty;   // foreign key property
		
		[ValidateNever]
        public Country Country { get; set; } = null!;           // navigation property
		
        [ForeignKey("CustomerID")]
		public ICollection<Product> Products { get; set; }      //skip nav
        public string FullName => FirstName + " " + LastName;   // read-only property

		public string? Slug => FullName?.ToLower().Replace(' ', '-').ToString();
	}
}