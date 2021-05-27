using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo._4_ShipService
{

	public class ShipService
	{
		public void ShippingByStore( List<Order> orders )
		{
			// handle seven's orders
			var ordersBySeven = orders.Where( x => x.StoreType != StoreType.Family );
			var sevenService = new SevenService();
			foreach( var order in ordersBySeven ) {
				sevenService.Ship( order );
			}

			// handle family's orders
			var ordersByFamily = orders.Where( x => x.StoreType == StoreType.Family );
			var familyService = new FamilyService();
			foreach( var order in ordersByFamily ) {
				familyService.Ship( order );
			}
		}
	}
}
