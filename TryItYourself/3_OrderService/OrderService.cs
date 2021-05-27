using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryItYourself._3_OrderService
{
	/// <summary>
	/// Fix:
	/// 1. Parsing CSV 檔時，直接用到 StreamReader 透過 File I/O 讀取檔案內容。
	/// 2. BookDao 中，透過 HttpClient 與外部 web service 直接相依。
	/// Caution:
	/// 此功能已上線所以不能改任何架構(包含改public)
	/// </summary>
	public class OrderService
	{
		private string _filePath = @"C:\temp\testOrders.csv";

		public void SyncBookOrders()
		{
			var orders = this.GetOrders();

			// only get orders of book
			var ordersOfBook = orders.Where( x => x.Type == "Book" );

			var bookDao = GetBookDao();
			foreach( var order in ordersOfBook ) {
				bookDao.Insert( order );
			}
		}

		private List<Order> GetOrders()
		{
			// parse csv file to get orders
			var result = ReadOrders();

			return result;
		}

		protected virtual List<Order> ReadOrders()
		{
			var result = new List<Order>();

			// directly depend on File I/O
			using( StreamReader sr = new StreamReader( this._filePath, Encoding.UTF8 ) ) {
				int rowCount = 0;

				while( sr.Peek() > -1 ) {
					rowCount++;

					var content = sr.ReadLine();

					// Skip CSV header line
					if( rowCount > 1 ) {
						string[] line = content.Trim().Split( ',' );

						result.Add( this.Mapping( line ) );
					}
				}
			}

			return result;
		}

		protected virtual BookDao GetBookDao()
		{
			return new BookDao();
		}

		private Order Mapping( string[] line )
		{
			var result = new Order
			{
				ProductName = line[ 0 ],
				Type = line[ 1 ],
				Price = Convert.ToInt32( line[ 2 ] ),
				CustomerName = line[ 3 ]
			};

			return result;
		}
	}
}
