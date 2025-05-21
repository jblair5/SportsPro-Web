namespace SportsPro.Models
{
	public class TechSession
	{

		private const string Tech = "Technician";
		private const string Incident = "Incident";
		private const string ID = "TechID";
		private const string Customers = "Customers";
		private const string Products = "Products";
		private const string Incidents = "Incidents";

		private ISession session {  get; set; }
		public TechSession (ISession session) => this.session = session;

		public void SetTechID(int id) => session.SetInt32(ID, id);
		public void SetTech (Technician t) => session.SetObject(Tech, t);
		public void SetIncident(Incident i) => session.SetObject(Incident, i);
		public void SetCustomers(List<Customer> c) => session.SetObject(Customers, c);
		public void SetProducts(List<Product> p) => session.SetObject(Products, p);
		public void SetIncidents(List<Incident> i) => session.SetObject(Incidents, i);

		public int? GetTechID() => session.GetInt32 (ID);
		public Technician GetTechnician() => session.GetObject<Technician>(Tech) ?? new Technician();
		public Incident GetIncident() => session.GetObject<Incident>(Incident) ?? new Incident();
		public List<Customer> GetCustomers() => session.GetObject<List<Customer>>(Customers) ?? new List<Customer>();
		public List<Product> GetProducts() => session.GetObject<List<Product>>(Products) ?? new List<Product>();
		public List<Incident> GetIncidents() => session.GetObject<List<Incident>>(Incidents) ?? new List<Incident>();

		public void RemoveTech()
		{
			session.Remove(Tech);
			session.Remove(ID);
			
		}
	}
}
