using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryItYourself._2_HappyHoliday
{
	public class Holiday
	{
		/// <summary>
		/// 如果今天是聖誕節前一天(12/24)或是聖誕節(12/25)，回傳String"Merry Xmas"，如果不是，回傳"Today is not Xmas"
		/// </summary>
		public string SayHello()
		{
			var today = DateTime.Today;
			if( today.Month == 12 && ( today.Day == 25 || today.Day == 24 ) ) {
				return "Merry Xmas";
			}

			return "Today is not Xmas";
		}
	}
}
