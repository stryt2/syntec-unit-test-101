using System.Net.Http;
using System.Net.Http.Formatting;

namespace Demo._4_ShipService
{
	public class FamilyService
	{
		public void Ship( Order order )
		{
			// family web service
			var client = new HttpClient();
			var response = client.PostAsync( "http://api.family.com/Order", order, new JsonMediaTypeFormatter() );
			response.Result.EnsureSuccessStatusCode();
		}
	}

}
