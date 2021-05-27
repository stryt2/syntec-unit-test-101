using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo._2_HouseWork
{
	/// <summary>
	/// 情境：當今天是星期日的時候，就應該是媽媽做家事。如果不是星期日，就是爸爸做家事。
	/// </summary>
	public class HouseWork
	{
		public string WhoShouldWork()
		{
			// TODO: 改寫以便讓此方法的商業邏輯能被驗證
			var today = DateTime.Now;
			if( today.DayOfWeek == DayOfWeek.Sunday ) {
				return "Mom";
			}
			else {
				return "Dad";
			}
		}
	}
}
