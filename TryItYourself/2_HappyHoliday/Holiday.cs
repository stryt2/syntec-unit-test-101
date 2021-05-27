using System;

namespace TryItYourself._2_HappyHoliday
{
	public class Holiday
	{
		public Holiday( IDateTimeProvider dateTimeProvider )
		{
			m_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException();
		}

		readonly IDateTimeProvider m_dateTimeProvider;

		/// <summary>
		/// 如果今天是聖誕節前一天(12/24)或是聖誕節(12/25)，回傳String"Merry Xmas"，如果不是，回傳"Today is not Xmas"
		/// </summary>
		public string SayHello()
		{
			var today = m_dateTimeProvider.GetToday();
			if( today.Month == 12 && ( today.Day == 25 || today.Day == 24 ) ) {
				return "Merry Xmas";
			}

			return "Today is not Xmas";
		}
	}

	public interface IDateTimeProvider
	{
		DateTime GetToday();
	}

	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime GetToday()
		{
			return DateTime.Today;
		}
	}
}
