using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace TryItYourself._3_OrderService
{
	class OrderServiceTests
	{
		[Test]
		public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
		{
			// arrange
			var target = new OrderService();
			var orders = new List<Order>
			{
				new Order{ Type="Book", Price = 100, ProductName = "my book"},
				new Order{ Type="CD", Price = 200, ProductName = "my CD"},
				new Order{ Type="Book", Price = 300, ProductName = "POP book"},
			};

			// act
			target.SyncBookOrders();

			// assert
			// how to assert interaction of target and web service ?
			// hint: 驗證是否有新增正確筆數
		}
	}
}
