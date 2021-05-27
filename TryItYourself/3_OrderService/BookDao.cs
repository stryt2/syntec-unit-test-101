using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace TryItYourself._3_OrderService
{
	public class BookDao
	{
		public void Insert( Order order )
		{
			// directly depend on some web service
			var client = new HttpClient();
			var response = client.PostAsync( "http://api.syntec.io/Order", order, new JsonMediaTypeFormatter() ).Result;
			response.EnsureSuccessStatusCode();
		}
	}
}
