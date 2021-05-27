using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;

namespace TryItYourself._3_OrderService
{
	public class BookDao
	{
		public virtual void Insert( Order order )
		{
			// directly depend on some web service
			var client = GetHttpClient();
			var httpMsg = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri( "http://api.syntec.io/Order" ),
				Content = new ObjectContent<Order>( order, new JsonMediaTypeFormatter() ),
			};
			var response = client
				.SendAsync( httpMsg, CancellationToken.None )
				.Result;
			response.EnsureSuccessStatusCode();
		}

		protected virtual HttpClient GetHttpClient()
		{
			return new HttpClient();
		}
	}
}
