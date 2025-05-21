namespace SportsPro.Models
{
	public class TechnicianViewModel
	{

		public int ID { get; set; }
		public Technician Technician { get; set; } = new Technician();
		public Incident Incident { get; set; } = new Incident();
		public List<Technician> Technicians { get; set; } = new List<Technician>();
		public List<Incident> Incidents { get; set; } = new List<Incident>();
		public List<Customer> Customers { get; set; } = new List<Customer>();
		public List<Product> Products { get; set; } = new List<Product>();



	}
}
