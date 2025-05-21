namespace SportsPro.Models
{
	public static class Check
	{

		public static string EmailExists(SportsProContext context, string email, string FullName)
		{

			string msg = string.Empty;
			if (!string.IsNullOrEmpty(email))
			{
				var customer = context.Customers.FirstOrDefault(c =>  c.Email.ToLower() == email.ToLower());
				if (customer != null && customer.FullName != FullName)
					msg = $"Email Address {email} is already in use.";
			}
			return msg ;
		}
	}
}
