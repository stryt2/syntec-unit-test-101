using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Demo._4_ShipService
{
	class ShipServiceTests
	{
		[Test]
		public void TestShippingByStore_Seven_1_Order_Family_2_Orders()
		{
			// arrange
			var target = new ShipService();

			var orders = new List<Order>
			{
				new Order{ StoreType = StoreType.Family, Id = 1},
				new Order{ StoreType = StoreType.Seven, Id = 2},
				new Order{ StoreType = StoreType.Family, Id = 3},
			};

			// act
			target.ShippingByStore( orders );

			// assert
			// TODO: check ShipService should invoke SevenService once and FamilyService twice
		}
	}
}
