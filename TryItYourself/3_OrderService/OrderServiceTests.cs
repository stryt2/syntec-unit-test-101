using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace TryItYourself._3_OrderService
{
	class OrderServiceTests
	{
		[Test]
		public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
		{
			// arrange
			var orders = new List<Order>
			{
				new Order{ Type="Book", Price = 100, ProductName = "my book" },
				new Order{ Type="CD", Price = 200, ProductName = "my CD" },
				new Order{ Type="Book", Price = 300, ProductName = "POP book" },
			};

			var httpHandlerStub = new Mock<HttpMessageHandler>();
			httpHandlerStub
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>() )
				.ReturnsAsync( new HttpResponseMessage()
				{
					StatusCode = HttpStatusCode.OK,
				} );
			var httpClient = new HttpClient( httpHandlerStub.Object );

			var bookDaoMock = new Mock<BookDao>();
			bookDaoMock
				.Protected()
				.Setup<HttpClient>( "GetHttpClient" )
				.Returns( httpClient );
			var target = new OrderServiceStub( orders, bookDaoMock.Object );

			// act
			target.SyncBookOrders();

			// assert
			var expectedCallCount = orders
				.Where( o => string.Compare( o.Type, "Book" ) == 0 )
				.Count();
			bookDaoMock.Verify( m => m.Insert( It.IsAny<Order>() ), Times.Exactly( expectedCallCount ) );
		}
	}

	class OrderServiceStub : OrderService
	{
		public OrderServiceStub( List<Order> orders, BookDao bookDao )
		{
			m_orders = orders;
			m_bookDao = bookDao;
		}

		readonly List<Order> m_orders;
		readonly BookDao m_bookDao;

		protected override List<Order> ReadOrders()
		{
			return m_orders;
		}

		protected override BookDao GetBookDao()
		{
			return m_bookDao;
		}
	}
}
