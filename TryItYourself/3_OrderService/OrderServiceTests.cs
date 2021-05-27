using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

			var bookDaoMock = new Mock<IBookDao>();

			var orderServiceStub = new Mock<OrderService>();
			orderServiceStub
				.Protected()
				.Setup<List<Order>>( "GetOrders" )
				.Returns( orders );
			orderServiceStub
				.Protected()
				.Setup<IBookDao>( "GetBookDao" )
				.Returns( bookDaoMock.Object );

			var target = orderServiceStub.Object;

			// act
			target.SyncBookOrders();

			// assert
			var expectedCallTimes = orders
				.Where( o => string.Compare( o.Type, "Book" ) == 0 )
				.Count();
			bookDaoMock.Verify( b => b.Insert( It.IsAny<Order>() ), Times.Exactly( expectedCallTimes ) );
		}
	}
}