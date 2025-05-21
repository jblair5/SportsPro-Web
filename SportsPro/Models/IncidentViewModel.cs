namespace SportsPro.Models
{
    public class IncidentViewModel
    {

        public List<Customer> Customers {  get; set; } = new List<Customer>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Technician> Technicians { get; set; } = new List<Technician>();
        public List<Incident> incidents { get; set; } = new List<Incident>();
        public Incident Incident { get; set; } = new Incident();
        public string Action { get; set; } = string.Empty;
        public string Filter { get; set; } = "All";

    }
}
