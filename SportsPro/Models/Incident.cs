﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace SportsPro.Models
{
    public class Incident
    {
		public int IncidentID { get; set; }
		public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateOpened { get; set; } = DateTime.Now;
        public DateTime? DateClosed { get; set; } = null;

		public int CustomerID { get; set; }                   // foreign key property
		
		[ValidateNever]
		public Customer Customer { get; set; } = null!;       // navigation property

		public int ProductID { get; set; }                    // foreign key property
		[ValidateNever]
		public Product Product { get; set; } = null!;         // navigation property

		public int TechnicianID { get; set; }                 // foreign key property 
		[ValidateNever]
		public Technician Technician { get; set; } = null!;   // navigation property

		
	}
}