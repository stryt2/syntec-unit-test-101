using System.Net.Http;
using System.Net.Http.Formatting;

namespace Demo._4_ShipService
{
	public class SevenService
    {
        public void Ship( Order order )
        {
            // seven web service
            var client = new HttpClient();
            var response = client.PostAsync( "http://api.seven.com/Order", order, new JsonMediaTypeFormatter() );
            response.Result.EnsureSuccessStatusCode();
        }
    }

}
